
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Knjiznica.Core.Models.ViewModels;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services.Commands.Books;
using Knjiznica.Core.Services.Queries.Authors;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Core.Services.Queries.Rents;
using System.Windows.Input;
using Knjiznica.Core.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Knjiznica.Presentation.Controllers
{
    public class BooksController : Controller
    {
        ////private readonly MediatR.IMediator _mediator;
        ////public BooksController(MediatR.IMediator mediator) => _mediator = mediator;

        readonly ICommandHandler<AddBookCommand> _addBook;
        readonly ICommandHandler<EditBookCommand> _editBook;
        readonly ICommandHandler<DeleteBookCommand> _deleteBook;
        readonly IQueryHandler<GetBooksQuery, IQueryable<Book>> _getBooks;
        readonly IQueryHandler<GetBookByIdQuery, Book> _getBookById;
        readonly IQueryHandler<GetAllGenresQuery, IEnumerable<Genre>> _getGenres;
        readonly IQueryHandler<GetRentsWithBookIdQuery, IQueryable<Rent>> _booksRents;
        readonly IQueryHandler<GetAuthorsQuery, IQueryable<Autor>> _getAutors;

        public BooksController(
            ICommandHandler<AddBookCommand> addBook,
            ICommandHandler<EditBookCommand> editBook,
            ICommandHandler<DeleteBookCommand> deleteBook,
            IQueryHandler<GetBooksQuery, IQueryable<Book>> getBooks,
            IQueryHandler<GetBookByIdQuery, Book> getBookById,
            IQueryHandler<GetAllGenresQuery, IEnumerable<Genre>> getGenres,
            IQueryHandler<GetRentsWithBookIdQuery, IQueryable<Rent>> booksRents,
            IQueryHandler<GetAuthorsQuery, IQueryable<Autor>> getAutors)

        {
            _addBook = addBook;
            _editBook = editBook;
            _deleteBook = deleteBook;
            _getBooks = getBooks;
            _getBookById = getBookById;
            _getGenres = getGenres;
            _booksRents = booksRents;
            _getAutors = getAutors; 
        }

        public async Task<IActionResult> Index(string sortOrder, int page)
        {

            ViewData["TitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "titleDesc" : "";
            ViewData["GenreSortParam"] = sortOrder == "genre" ? "genreDesc" : "genre";
            ViewData["AuthorSortParam"] = sortOrder == "author" ? "authorDesc" : "author";
            ViewData["CopiesSortParam"] = sortOrder == "copies" ? "copiesDesc" : "copies";

            var books = await _getBooks.HandleAsync(new GetBooksQuery());

            ViewData["Page"] = page;
            int take = 3;
            int pageNo = (int)Math.Ceiling((decimal)books.Count() / take);
            ViewData["MaxPage"] = pageNo - 1;

            var bookViewModel = (from b in books
                                 .Include(x => x.Genre)
                                 .Include(x => x.Autor)
                                 select new BookViewModel()
                                 {
                                     BookId = b.BookId,
                                     Title = b.Title,
                                     AuthorFullName = b.Autor.FullName,
                                     GenreId = b.GenreId,
                                     GenreName = b.Genre.GenreName,
                                     AutorId = b.AutorId,
                                     BrojPrimjeraka = b.BrojPrimjeraka,
                                     AutorLastName = b.Autor.LastName
                                 });

            switch (sortOrder)
            {
                case "title":
                    bookViewModel = bookViewModel.OrderBy(x => x.Title);
                    break;
                case "titleDesc":
                    bookViewModel = bookViewModel.OrderByDescending(x => x.Title);
                    break;
                case "genre":
                    bookViewModel = bookViewModel.OrderBy(x => x.GenreName);
                    break;
                case "genreDesc":
                    bookViewModel = bookViewModel.OrderByDescending(x => x.GenreName);
                    break;
                case "authorDesc":
                    bookViewModel = bookViewModel.OrderByDescending(x => x.AutorLastName);
                    break;
                case "author":
                    bookViewModel = bookViewModel.OrderBy(x => x.AutorLastName);
                    break;
                case "copiesDesc":
                    bookViewModel = bookViewModel.OrderByDescending(x => x.BrojPrimjeraka);
                    break;
                case "copies":
                    bookViewModel = bookViewModel.OrderBy(x => x.BrojPrimjeraka);
                    break;
            }


            return View(bookViewModel.Skip(page * take).Take(take));
        }
        public async Task<IActionResult> Create()
        {

            var authors =await _getAutors.HandleAsync(new GetAuthorsQuery());
            var genres = await _getGenres.HandleAsync(new GetAllGenresQuery());
            
            

            ViewData["FullName"] = new SelectList(authors, "AutorId", "FullName");
            ViewData["GenreName"] = new SelectList(genres, "GenreId", "GenreName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()] Book book)
        {
            var authors = await _getAutors.HandleAsync(new GetAuthorsQuery());
            var genres = await _getGenres.HandleAsync(new GetAllGenresQuery());
            ViewData["AutorId"] = new SelectList(authors, "AutorId", "AutorId", book.AutorId);
            ViewData["GenreId"] = new SelectList(genres, "GenreId", "GenreId", book.GenreId);

            if (ModelState.IsValid)
            {
                await _addBook.HandleAsync(new AddBookCommand(book));
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> BooksRents(int id)
        {

            var rents = await _booksRents.HandleAsync(new GetRentsWithBookIdQuery(id));

            var rentViewModels = (from kc in rents
                              .Include(x => x.Member)
                              .Include(x => x.Book)
                                  select new RentViewModel()
                                  {
                                      RentId = kc.RentId,
                                      BookId = kc.BookId,
                                      MemberId = kc.RentId,
                                      MemberName = kc.Member.Name,
                                      BookTitle = kc.Book.Title,
                                      DateRented = kc.DateRented,
                                  });

            var Name = await _getBookById.HandleAsync(new GetBookByIdQuery(id));
            ViewData["Name"] = Name.Title;
            return View(rentViewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _getBookById.HandleAsync(new GetBookByIdQuery(id));
            var authors = await _getAutors.HandleAsync(new GetAuthorsQuery());
            var genres = await _getGenres.HandleAsync(new GetAllGenresQuery());

            BookViewModel bookVM = new BookViewModel()
            {
                BookId = book.BookId,
                Title= book.Title,
                AutorId = book.AutorId,
                AuthorFullName = authors.Where(x=>x.AutorId == book.AutorId).Single().FullName,
                GenreId = book.GenreId,
                GenreName = genres.Where(x=>x.GenreId == book.GenreId).Single().GenreName,
                BrojPrimjeraka = book.BrojPrimjeraka,
            };

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind()] Book book)
        {
            if (ModelState.IsValid)
            {
                await _editBook.HandleAsync(new EditBookCommand(book));
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _getBookById.HandleAsync(new GetBookByIdQuery(id));

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var book = await _getBookById.HandleAsync(new GetBookByIdQuery(id));
            await _deleteBook.HandleAsync(new DeleteBookCommand(book));

            return RedirectToAction(nameof(Index));
        }
    }

}


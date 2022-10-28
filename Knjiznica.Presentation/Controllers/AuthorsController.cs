using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Models.ViewModels;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Authors;
using Knjiznica.Core.Services.Commands.Rents;
using Knjiznica.Core.Services.Queries.Authors;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Core.Services.Queries.Members;
using Knjiznica.Core.Services.Queries.Rents;
using Knjiznica.Infrastructure.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Knjiznica.Presentation.Controllers
{
    public class AuthorsController : Controller
    {
        readonly ICommandHandler<EditAuthorCommand> _editAuthor;
        readonly ICommandHandler<AddAuthorCommand> _addAuthor;
        readonly ICommandHandler<DeleteAuthorCommand> _deleteAuthor;
        readonly IQueryHandler<GetAuthorsQuery, IQueryable<Autor>> _getAuthors;
        readonly IQueryHandler<GetAutorByIdQuery, Autor> _getAuthorsById;
        readonly IQueryHandler<GetBooksWithAuthorIdQuery, IQueryable<Book>> _getBooksWithAuthorId;
        public AuthorsController(ICommandHandler<EditAuthorCommand> editAuthor,
            ICommandHandler<AddAuthorCommand> addAuthor,
            ICommandHandler<DeleteAuthorCommand> deleteAuthor,
            IQueryHandler<GetAuthorsQuery, IQueryable<Autor>> getAuthors,
            IQueryHandler<GetAutorByIdQuery, Autor> getAuthorById,
            IQueryHandler<GetBooksWithAuthorIdQuery, IQueryable<Book>> getBooksWithAuthorId
            ) 
        { 
            _editAuthor = editAuthor;
            _addAuthor = addAuthor;
            _deleteAuthor = deleteAuthor;
            _getAuthors = getAuthors;
            _getAuthorsById = getAuthorById;
            _getBooksWithAuthorId = getBooksWithAuthorId;

        }

        // GET: Autors
        public async Task<IActionResult> Index(int page)
        {
            ViewData["Page"] = page;
            int take = 3;
            var count = await _getAuthors.HandleAsync(new GetAuthorsQuery());
            int pageNo = (int)Math.Ceiling((decimal)count.Count() / take);
            ViewData["MaxPage"] = pageNo - 1;
            return View( _getAuthors.HandleAsync(new GetAuthorsQuery()).Result.Skip(page * take).Take(take));
        }

        // GET: Autors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutorId,FirstName,LastName")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _addAuthor.HandleAsync(new AddAuthorCommand(autor));
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        public async Task<IActionResult> AuthorsBooks(int id)
        {

            var books = await _getBooksWithAuthorId.HandleAsync(new GetBooksWithAuthorIdQuery(id));

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

                                 });

            var Name = await _getAuthorsById.HandleAsync(new GetAutorByIdQuery(id));
            ViewData["Name"] = Name.FullName;
            return View(bookViewModel);
        }

        // GET: Autors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var autor = await _getAuthorsById.HandleAsync(new GetAutorByIdQuery(id));
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind()] Autor autor)
        {
            if (id != autor.AutorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _editAuthor.HandleAsync(new EditAuthorCommand(autor));
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var autor = await _getAuthorsById.HandleAsync(new GetAutorByIdQuery(id));

            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var autor = await _getAuthorsById.HandleAsync(new GetAutorByIdQuery(id));
            await _deleteAuthor.HandleAsync(new DeleteAuthorCommand(autor));

            return RedirectToAction(nameof(Index));
        }  
    }
}

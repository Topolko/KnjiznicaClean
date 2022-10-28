using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Models.ViewModels;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Core.Services.Queries.Genres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Knjiznica.Presentation.Controllers
{
    public class GenresController : Controller
    {
        ////readonly MediatR.IMediator _mediator;
        readonly ICommandHandler<AddGenreCommand> _addGenre;
        readonly ICommandHandler<DeleteGenreCommand> _deleteGenre;
        readonly ICommandHandler<EditGenreCommand> _editGenre;
        readonly IQueryHandler<GetAllGenresQuery, IEnumerable<Genre>> _getGenres;
        readonly IQueryHandler<GetGenreByIdQuery, Genre> _getGenreById;
        readonly IQueryHandler<GetBooksWithGenreIdQuery, IQueryable<Book>> _getBooksWithGenreId;

        public GenresController(
             IQueryHandler<GetAllGenresQuery, IEnumerable<Genre>> getGenresQuery,
             IQueryHandler<GetGenreByIdQuery, Genre> getGenreByIdQuery,
             IQueryHandler<GetBooksWithGenreIdQuery, IQueryable<Book>> getBooksWithGenreId,
             ICommandHandler<AddGenreCommand> addGenreCommand,
             ICommandHandler<EditGenreCommand> editGenreCommand,
             ICommandHandler<DeleteGenreCommand> deleteGenreCommand
            )
        {
            ////_mediator = mediator;
            _addGenre = addGenreCommand;
            _editGenre = editGenreCommand;
            _deleteGenre = deleteGenreCommand;
            _getGenres = getGenresQuery;
            _getGenreById = getGenreByIdQuery;
            _getBooksWithGenreId = getBooksWithGenreId;
        }

        public async Task<IActionResult> Index(int page)
        {
            var genres = await _getGenres.HandleAsync(new GetAllGenresQuery());

            ViewData["Page"] = page;
            int take = 2;
            int pageNo = (int)Math.Ceiling((decimal)genres.Count() / take);
            ViewData["MaxPage"] = pageNo - 1;

            return View(genres.Skip(page * take).Take(take));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _addGenre.HandleAsync(new AddGenreCommand(genre));
                ////await _mediator.Send(new AddGenreCommand(genre));
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        public async Task<IActionResult> GenresBooks(int id)
        {

            var genresBooks = await _getBooksWithGenreId.HandleAsync(new GetBooksWithGenreIdQuery(id));

            var bookViewModel = (from b in genresBooks
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

            var Name = await _getGenreById.HandleAsync(new GetGenreByIdQuery(id));
            ViewData["Name"] = Name.GenreName;
            return View(bookViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var genre = await _getGenreById.HandleAsync(new GetGenreByIdQuery(id));

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind()] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _editGenre.HandleAsync(new EditGenreCommand(genre));
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _getGenreById.HandleAsync(new GetGenreByIdQuery(id));

            return View(genre);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(int id)
        {
            var genre = await _getGenreById.HandleAsync(new GetGenreByIdQuery(id));
            await _deleteGenre.HandleAsync(new DeleteGenreCommand(genre));

            return RedirectToAction(nameof(Index));
        }

    }
}

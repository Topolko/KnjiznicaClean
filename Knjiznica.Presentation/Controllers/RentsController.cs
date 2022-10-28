using Knjiznica.Core.Models;
using Knjiznica.Core.Models.EditModels;
using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Models.ViewModels;
using Knjiznica.Core.Services.Commands.Books;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Rents;
using Knjiznica.Core.Services.Queries.Authors;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Core.Services.Queries.Members;
using Knjiznica.Core.Services.Queries.Rents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Mvc.Core;
using X.PagedList;
using Knjiznica.Core.Services.Queries.Genres;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Reflection.Metadata.BlobBuilder;

namespace Knjiznica.Presentation.Controllers
{
    public class RentsController : Controller
    {
        ////private readonly MediatR.IMediator _mediator;
        readonly ICommandHandler<DeleteRentCommand> _deleteRent;
        readonly ICommandHandler<AddRentCommand> _addRent;
        readonly ICommandHandler<EditRentCommand> _editRent;
        readonly IQueryHandler<GetRentsQuery, IQueryable<Rent>> _getRents;
        readonly IQueryHandler<GetRentByIdQuery, Rent> _getRentById;
        readonly IQueryHandler<GetRentsWithBookIdQuery, IQueryable<Rent>> _getRentsWithBookId;
        readonly IQueryHandler<GetRentsWithMemberIdQuery, IQueryable<Rent>> _getRentsWithMemberId;
        readonly IQueryHandler<GetMembersQuery, IQueryable<Member>> _getMembers;
        readonly IQueryHandler<GetBooksQuery, IQueryable<Book>> _getBooks;
        public RentsController(ICommandHandler<DeleteRentCommand> deleteRent,
            ICommandHandler<AddRentCommand> addRent,
            ICommandHandler<EditRentCommand> editRent,
            IQueryHandler<GetRentsQuery, IQueryable<Rent>> getRents,
            IQueryHandler<GetRentByIdQuery, Rent> getRentById,
            IQueryHandler<GetRentsWithBookIdQuery, IQueryable<Rent>> getRentsWithBookId,
            IQueryHandler<GetRentsWithMemberIdQuery, IQueryable<Rent>> getRentsWithMemberId,
            IQueryHandler<GetMembersQuery, IQueryable<Member>> getMembers,
            IQueryHandler<GetBooksQuery, IQueryable<Book>> getBooks)
        {
            _deleteRent = deleteRent;
            _addRent = addRent;
            _editRent = editRent;
            _getRents = getRents;
            _getRentById = getRentById;
            _getRentsWithBookId = getRentsWithBookId;
            _getRentsWithMemberId = getRentsWithMemberId;
            _getMembers = getMembers;
            _getBooks = getBooks;
        }


        // GET: Rents
        public async Task<IActionResult> Index(string sortOrder, string searchTitle, string searchMember, int page)
        {
            ViewData["IsLate"] = String.IsNullOrEmpty(sortOrder) ? "true" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dateDesc" : "date";
            ViewData["MemberFilter"] = searchMember;
            ViewData["TitleFilter"] = searchTitle;

            var rents = await _getRents.HandleAsync(new GetRentsQuery());

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
            if (!String.IsNullOrEmpty(searchTitle))
            {
                rentViewModels = rentViewModels.Where(x => x.BookTitle.Contains(searchTitle));
            }
            if (!String.IsNullOrEmpty(searchMember))
            {
                rentViewModels = rentViewModels.Where(x => x.MemberName.Contains(searchMember));
            }

            ViewData["Page"] = page;
            int take = 3;
            int pageNo = (int)Math.Ceiling((decimal)rents.Count() / take);
            ViewData["MaxPage"] = pageNo - 1;

            switch (sortOrder)
            {
                case "date":
                    rentViewModels = rentViewModels.OrderBy(x => x.DateRented);
                    break;
                case "dateDesc":
                    rentViewModels = rentViewModels.OrderByDescending(x => x.DateRented);
                    break;
                case "true":
                    rentViewModels = rentViewModels.Where(x => x.DateRented.AddDays(Constants.MAX_RENT_DAYS) < DateTime.Today);
                    break;
            }

            return View(rentViewModels.ToList().Skip(page * take).Take(take));
        }

        public async Task<IActionResult> Create()
        {
            var books = await _getBooks.HandleAsync(new GetBooksQuery());
            var members = await _getMembers.HandleAsync(new GetMembersQuery());


            ViewData["book"] = new SelectList(books, "BookId", "Title");
            ViewData["member"] = new SelectList(members, "MemberId", "Name");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()] Rent rent)
        {
            var books = await _getBooks.HandleAsync(new GetBooksQuery());
            var members = await _getMembers.HandleAsync(new GetMembersQuery());
            ViewData["BookId"] = new SelectList(books, "BookId", "BookId", rent.BookId);
            ViewData["MemberId"] = new SelectList(members, "MemberId", "MemberId", rent.RentId);
            if (ModelState.IsValid)
            {
                await _addRent.HandleAsync(new AddRentCommand(rent));
                return RedirectToAction(nameof(Index));
            }
            return View(rent);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rent = await _getRentById.HandleAsync(new GetRentByIdQuery(id));
            var members = await _getMembers.HandleAsync(new GetMembersQuery());
            var books = await _getBooks.HandleAsync(new GetBooksQuery());

            RentEM rentEM = new RentEM()
            {
                MemberName = members.Where(x=>x.MemberId == rent.MemberId).Single().Name,
                BookTitle = books.Where(x=>x.BookId == rent.BookId).Single().Title,
                BookId = rent.BookId,
                MemberId = rent.MemberId,
                RentId = rent.RentId,
                DateRented = rent.DateRented,
                IsLate = rent.IsLate,
                Members = members.ToDictionary(x => x.MemberId, x => x.Name),
                Books = books.ToDictionary(x => x.BookId, x => x.Title)

            };

            return View(rentEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind()] Rent rent)
        {
            if (ModelState.IsValid)
            {
                await _editRent.HandleAsync(new EditRentCommand(rent));
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var rent = await _getRentById.HandleAsync(new GetRentByIdQuery(id));
            var members = await _getMembers.HandleAsync(new GetMembersQuery());
            var books = await _getBooks.HandleAsync(new GetBooksQuery());

            RentViewModel rentVM = new RentViewModel()
            {
                RentId = rent.RentId,
                BookId = rent.BookId,
                MemberId = rent.RentId,
                MemberName = members.Where(x => x.MemberId == rent.MemberId).Single().Name,
                BookTitle = books.Where(x => x.BookId == rent.BookId).Single().Title,
                DateRented = rent.DateRented,

            };

            return View(rentVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var rent = await _getRentById.HandleAsync(new GetRentByIdQuery(id));

            
            await _deleteRent.HandleAsync(new DeleteRentCommand(rent));

            return RedirectToAction(nameof(Index));
        }

    }
}

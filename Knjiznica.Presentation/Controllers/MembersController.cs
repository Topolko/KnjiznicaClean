using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Models.ViewModels;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Members;
using Knjiznica.Core.Services.Commands.Rents;
using Knjiznica.Core.Services.Queries.Members;
using Knjiznica.Core.Services.Queries.Rents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;
using static System.Reflection.Metadata.BlobBuilder;


namespace Knjiznica.Presentation.Controllers
{
    public class MembersController : Controller
    {
        readonly ICommandHandler<EditMemberCommand> _editMember;
        readonly ICommandHandler<AddMemberCommand> _addMember;
        readonly ICommandHandler<DeleteMemberCommand> _deleteMember;
        readonly IQueryHandler<GetMembersQuery, IQueryable<Member>> _getMembers;
        readonly IQueryHandler<GetMemberByIdQuery, Member> _getMemberById;
        readonly IQueryHandler<GetRentsWithMemberIdQuery, IQueryable<Rent>> _getResntsWithMemberId;
        public MembersController(
            ICommandHandler<EditMemberCommand> editMember,
            ICommandHandler<AddMemberCommand> addMember,
            ICommandHandler<DeleteMemberCommand> deleteMember,
            IQueryHandler<GetMembersQuery, IQueryable<Member>> getMembers,
            IQueryHandler<GetMemberByIdQuery, Member> getMembersById,
            IQueryHandler<GetRentsWithMemberIdQuery, IQueryable<Rent>> getResntsWithMemberId)
        {
            _editMember = editMember;
            _addMember = addMember;
            _deleteMember = deleteMember;
            _getMemberById = getMembersById;
            _getMembers = getMembers;
            _getResntsWithMemberId = getResntsWithMemberId;
        }
        public async Task<IActionResult> Index( int page)
        {
            var members = await _getMembers.HandleAsync(new GetMembersQuery());
            ViewData["Page"] = page;
            int take = 3;
            int pageNo = (int)Math.Ceiling((decimal)members.Count() / take);
            ViewData["MaxPage"] = pageNo - 1;

            return View(members.Skip(page * take).Take(take));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()] Member member)
        {
            if (ModelState.IsValid)
            {
                await _addMember.HandleAsync(new AddMemberCommand(member));
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        public async Task<IActionResult> MembersRents(int id)
        {
            
            var membersRents = await _getResntsWithMemberId.HandleAsync(new GetRentsWithMemberIdQuery(id));

            var rentViewModels = (from kc in membersRents
                                  .Include(x=>x.Member)
                                  .Include(x=>x.Book)
                                  select new RentViewModel()
                                  {
                                      RentId = kc.RentId,
                                      BookId = kc.BookId,
                                      MemberId = kc.RentId,
                                      MemberName = kc.Member.Name,
                                      BookTitle = kc.Book.Title,
                                      DateRented = kc.DateRented,
                                  });
            var Name = await _getMemberById.HandleAsync(new GetMemberByIdQuery(id));
            ViewData["Name"] = Name.Name;
            return View(rentViewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var member = await _getMemberById.HandleAsync(new GetMemberByIdQuery(id));

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind()] Member member)
        {
            if (ModelState.IsValid)
            {
                await _editMember.HandleAsync(new EditMemberCommand(member));
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _getMemberById.HandleAsync(new GetMemberByIdQuery(id));

            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var member = await _getMemberById.HandleAsync(new GetMemberByIdQuery(id));
            await _deleteMember.HandleAsync(new DeleteMemberCommand(member));

            return RedirectToAction(nameof(Index));
        }




    }
}

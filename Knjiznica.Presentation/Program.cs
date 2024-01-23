using Knjiznica.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Knjiznica.Core.Services.Queries.Members;
using Knjiznica.Core.Models.Models;
using Knjiznica.Infrastructure.Handlers.Authors;
using Knjiznica.Infrastructure.Handleres.Genres;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Presentation;
using Knjiznica.Core.Services.Commands.Books;
using Knjiznica.Infrastructure.Handleres.Books;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Infrastructure.Handlers.Books;
using Knjiznica.Infrastructure.Handleres.Rents;
using Knjiznica.Core.Services.Queries.Rents;
using Knjiznica.Infrastructure.Handlers.Rents;
using Knjiznica.Core.Services.Commands.Rents;
using Knjiznica.Core.Services.Queries.Authors;
using Knjiznica.Core.Services.Commands.Authors;
using Knjiznica.Infrastructure.Handleres.Members;
using Knjiznica.Core.Services.Commands.Members;
using Knjiznica.Infrastructure.Handlers.Currency;
using Knjiznica.Core.Services.Queries.Currency;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
////builder.Services.AddMediatR(typeof(Program));
////builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

#region Genres
builder.Services.AddQueryHandler<GetAllGenresQuery, IEnumerable<Genre>, GetAllGenresHandler>();
builder.Services.AddQueryHandler<GetGenreByIdQuery, Genre, GetGenreByIdHandler>();

builder.Services.AddCommandHandler<AddGenreCommand, AddGenreCommandHandler>();
builder.Services.AddCommandHandler<EditGenreCommand, EditGenreHandler>();
builder.Services.AddCommandHandler<DeleteGenreCommand, DeleteGenreHandler>();
#endregion

#region Books
builder.Services.AddCommandHandler<AddBookCommand, AddBookHandler>();
builder.Services.AddCommandHandler<EditBookCommand, EditBookHandler>();
builder.Services.AddCommandHandler<DeleteBookCommand, DeleteBookHandler>();

builder.Services.AddQueryHandler<GetBookByIdQuery, Book, GetBookByIdHandler>();
builder.Services.AddQueryHandler<GetBooksWithAuthorIdQuery, IQueryable<Book>, GetBooksWithAuthorIdHandler>();
builder.Services.AddQueryHandler<GetBooksWithGenreIdQuery, IQueryable<Book>, GetBooksWithGenreIdHandler>();
builder.Services.AddQueryHandler<GetBooksQuery, IQueryable<Book>, GetBooksHandler > ();
#endregion

#region Rents
builder.Services.AddQueryHandler<GetRentByIdQuery, Rent, GetRentByIdHandler >();
builder.Services.AddQueryHandler<GetRentsQuery, IQueryable<Rent>, GetRentsHandler>();
builder.Services.AddQueryHandler<GetRentsWithBookIdQuery, IQueryable<Rent>, GetRentsWithBookIdHandler>();
builder.Services.AddQueryHandler<GetRentsWithMemberIdQuery, IQueryable<Rent>, GetRentsWithMemberIdHandler>();

builder.Services.AddCommandHandler<AddRentCommand, AddRentHandler>();
builder.Services.AddCommandHandler<EditRentCommand, EditRentHandler>();
builder.Services.AddCommandHandler<DeleteRentCommand, DeleteRentHandler>();
#endregion

#region Authors

builder.Services.AddQueryHandler<GetAutorByIdQuery, Autor, GetAuthorByIdHandler>();
builder.Services.AddQueryHandler<GetAuthorsQuery, IQueryable<Autor>, GetAuthorsHandler>();

builder.Services.AddCommandHandler<AddAuthorCommand, AddAuthorHandler>();
builder.Services.AddCommandHandler<EditAuthorCommand, EditAuthorHandler>();
builder.Services.AddCommandHandler<DeleteAuthorCommand, DeleteAuthorHandler>();

#endregion

#region Members
builder.Services.AddQueryHandler<GetMembersQuery, IQueryable<Member>, GetMembersHandler>();
builder.Services.AddQueryHandler<GetMemberByIdQuery, Member, GetMemberByIdHandler>();

builder.Services.AddCommandHandler<AddMemberCommand, AddMemberHandler>();
builder.Services.AddCommandHandler<EditMemberCommand, EditMemberHandler>();
builder.Services.AddCommandHandler<DeleteMemberCommand, DeleteMemberHandler>();
#endregion

#region Currency
builder.Services.AddQueryHandler<GetAllRatesQuery, List<ValutaModel>, GetAllRatesHandler>();
builder.Services.AddQueryHandler<GetRatesByDateQuery, List<ValutaModel>, GetRatesByDateHandler>();
builder.Services.AddQueryHandler<ConvertQuery, decimal, ConvertHandler>();
builder.Services.AddQueryHandler<GetRatesForDateQuery, List<ValutaModel>, GetRatesForDateHandler>();

#endregion

builder.Services.AddDbContext<KnjiznicaContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

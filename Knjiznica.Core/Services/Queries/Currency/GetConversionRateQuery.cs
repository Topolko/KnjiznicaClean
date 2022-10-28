using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Queries.Currency
{
    //    public record GetConversionRateQuery(string valuta) : IRequest<List<ValutaModel>>;

    public class GetConversionRateQuery: IRequest<List<ValutaModel>>
    {
        public readonly string Valuta;
        public DateTime Date;


    }



}

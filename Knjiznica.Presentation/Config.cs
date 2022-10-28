using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Infrastructure.Handleres.Genres;

namespace Knjiznica.Presentation
{
    public static class Config
    {
        public static IServiceCollection AddCommandHandler<TCommand, TCommandHandler>(
            this IServiceCollection services
        )
            where TCommandHandler : class, ICommandHandler<TCommand>
        {
            return services.AddTransient<TCommandHandler>()
                .AddTransient<ICommandHandler<TCommand>>(sp => sp.GetRequiredService<TCommandHandler>());
        }

        public static IServiceCollection AddQueryHandler<TQuery, TQueryResult, TQueryHandler>(
            this IServiceCollection services
        )
            where TQueryHandler : class, IQueryHandler<TQuery, TQueryResult>
        {
            return services.AddTransient<TQueryHandler>()
                .AddTransient<IQueryHandler<TQuery, TQueryResult>>(sp => sp.GetRequiredService<TQueryHandler>());
        }

        public static IServiceCollection AddMultipleQueryHandler<TQuery1 , TQuery2, TQueryResult, TQueryHandler>(
            this IServiceCollection services
        )
            where TQueryHandler : class, IMultipleQueryHandler<TQuery1, TQuery2, TQueryResult>
        {
            return services.AddTransient<TQueryHandler>()
                .AddTransient<IMultipleQueryHandler<TQuery1, TQuery2, TQueryResult>>(sp => sp.GetRequiredService<TQueryHandler>());
        }
    }

}

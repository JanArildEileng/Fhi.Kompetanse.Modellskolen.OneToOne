using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Context;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Fhi.Kompetanse.Modellskolen.OneToOne.NUnitIntegrasjonstest
{
    internal class TestDataFactory
    {
        static public async Task AddToContext(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<KompetanseContext>();
                context.Countries.Add(CreateCountry());

                await context.SaveChangesAsync();
            }
        }

        static private Country CreateCountry()
        {

            return new Country()
            {
                Name = "Country" + Random.Shared.Next()
            };
        }
    }
}

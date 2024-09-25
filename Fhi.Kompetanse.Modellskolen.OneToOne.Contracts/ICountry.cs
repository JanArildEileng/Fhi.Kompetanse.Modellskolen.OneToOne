using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Model;
using Refit;

namespace Fhi.Kompetanse.Modellskolen.OneToOne.Contracts;

public interface ICountry
{
    [Get("/api/Country")]
    Task<List<GetCountryDto>> GetCountry();

    [Post("/api/Country")]
    Task<GetCountryDto> PostCountry(PostCountryDto countryDto);

    [Delete("/api/Country/{id}")]
    Task DeleteCountry(int id);
}

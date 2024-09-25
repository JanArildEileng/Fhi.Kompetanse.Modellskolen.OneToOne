namespace Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Entities;

public class Country
{
    public int CountryId { get; set; }
    required public string Name { get; set; }

    public King? King { get; set; }
}

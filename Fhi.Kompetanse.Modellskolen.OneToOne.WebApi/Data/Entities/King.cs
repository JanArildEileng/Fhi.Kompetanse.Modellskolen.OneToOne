namespace Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Entities;

public class King
{
    public int KingId { get; set; }
    required public string Name { get; set; }

    public int CountryId { get; set; }
    public Country? Country { get; set; }
}

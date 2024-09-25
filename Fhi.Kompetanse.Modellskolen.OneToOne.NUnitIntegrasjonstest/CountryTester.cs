using Fhi.Kompetanse.Modellskolen.OneToOne.Contracts;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Model;

namespace Fhi.Kompetanse.Modellskolen.OneToOne.NUnitIntegrasjonstest;


[TestFixture]
public class CountryTester:Testbase
{

    [SetUp]
    public async Task Setup()
    {
        //delete Spain..if exists
        int CountryId = (await countryClient.GetCountry()).Where(e => e.CountryName.Equals(SPAIN)).Select(e => e.CountryId).FirstOrDefault();
        if (CountryId > 0)
        {
            await countryClient.DeleteCountry(CountryId);
        }
        //add Norway and Harald
        GetCountryDto getCountryDto = await countryClient.PostCountry(new PostCountryDto(NORWAY, HARALD));
    }

    [TestCase]
    public async Task GetCountry_Test()
    {
        List<GetCountryDto> countryDtos = await countryClient.GetCountry();
        Assert.IsNotNull(countryDtos);
        Assert.IsNotEmpty(countryDtos);
    }

    [TestCase("Spain", "Carlos")]
    public async Task PostCountry_Test(string countryName, string kingName)
    {
        PostCountryDto postCountryDto = new PostCountryDto(countryName, kingName);
        GetCountryDto getCountryDto = await countryClient.PostCountry(postCountryDto);
        Assert.IsNotNull(getCountryDto);
        Assert.That(getCountryDto.CountryName, Is.EqualTo(countryName));
        Assert.That(getCountryDto.KingnameName, Is.EqualTo(kingName));
        Assert.Pass();
    }

    [TestCase]
    public async Task DeleteCountry_Test()
    {
        int CountryId = (await countryClient.GetCountry()).Where(e => e.CountryName == NORWAY).Select(e => e.CountryId).FirstOrDefault();

        Console.WriteLine($"DeleteCountry {CountryId}");

        if (CountryId > 0)
            await countryClient.DeleteCountry(CountryId);

        //assert Norway and Harald all gone
        var norway = (await countryClient.GetCountry()).Where(e => e.CountryName.Equals(NORWAY)).SingleOrDefault();
        Assert.IsNull(norway);
    }


    [TearDown]
    public void TearDown()
    {
    }

}
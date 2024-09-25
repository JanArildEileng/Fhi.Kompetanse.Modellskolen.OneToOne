using Fhi.Kompetanse.Modellskolen.OneToOne.Contracts;

namespace Fhi.Kompetanse.Modellskolen.OneToOne.NUnitIntegrasjonstest;

[TestFixture]
public class KingTester:Testbase
{
  
    [SetUp]
    public async Task Setup()
    {
        //make sure Charles is king in England..
        PostKingDto postKingDto = new PostKingDto(England, CHARLES);
        GetKingDto getKingDto = await kingclient.PostKing(postKingDto);
    }

    [TestCase]
    public async Task GetKings_Test()
    {
        List<GetKingDto> getKingDto = await kingclient.GetKings();
        Assert.IsNotNull(getKingDto);
        Assert.IsNotEmpty(getKingDto);
    }

    [TestCase("Spain", "Carlos")]
    [TestCase("Spain", "Ferdinan")]
    public async Task PostKing_Test(string countryName, string kingName)
    {
        PostKingDto postKingDto = new PostKingDto(countryName, kingName);
        GetKingDto getKingDto = await kingclient.PostKing(postKingDto);
        Assert.IsNotNull(getKingDto);
        Assert.That(getKingDto.CountryName, Is.EqualTo(countryName));
        Assert.That(getKingDto.KingnameName, Is.EqualTo(kingName));
        Assert.Pass();
    }

    [TestCase]
    public async Task DeleteKing_Test()
    {
        int KingId = (await kingclient.GetKings()).Where(e => e.KingnameName == CHARLES).Select(e => e.KingId).FirstOrDefault();

        if (KingId > 0)
            await kingclient.DeleteKing(KingId);
    }


    [TearDown]
    public void TearDown()
    {
    }

}
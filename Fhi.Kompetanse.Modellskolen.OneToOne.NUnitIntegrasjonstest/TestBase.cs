using Fhi.Kompetanse.Modellskolen.OneToOne.Contracts;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Refit;

namespace Fhi.Kompetanse.Modellskolen.OneToOne.NUnitIntegrasjonstest;


public class Testbase
{
    protected const string CHARLES = "Charles";
    protected const string England = "England";
    protected const string SPAIN = "Spain";
    protected const string NORWAY = "Norway";
    protected const string HARALD = "Harald";

    protected IKing kingclient;
    protected ICountry countryClient;

    internal WebApplicationFactory<Program> factory { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        factory = new WebApplicationFactory<Program>()
          .WithWebHostBuilder(builder =>
          {
              builder.ConfigureTestServices(services =>
              {
              });

              builder.UseEnvironment("Development");
          });


        countryClient = RestService.For<ICountry>(factory.CreateClient());
        kingclient = RestService.For<IKing>(factory.CreateClient());
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        factory.Dispose();
    }

}
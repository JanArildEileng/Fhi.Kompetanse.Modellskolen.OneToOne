using Fhi.Kompetanse.Modellskolen.OneToOne.Contracts;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Context;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Runtime.CompilerServices;
using Testcontainers.MsSql;

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

    const string databaseName = "Modellskolen";
    private MsSqlContainer MsSqlContainer { get; init; } = new MsSqlBuilder().Build();
    private string ConnectionString => MsSqlContainer.GetConnectionString().Replace("Database=master;", $"Database={databaseName};");




    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await MsSqlContainer.StartAsync();


        factory = new WebApplicationFactory<Program>()
          .WithWebHostBuilder(builder =>
          {
              builder.ConfigureTestServices(services =>
              {

                  var dbContextDescriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                   typeof(DbContextOptions<KompetanseContext>));

                  services.Remove(dbContextDescriptor!);

                  services.AddDbContext<KompetanseContext>(options => options.UseSqlServer(ConnectionString));
              });

              builder.UseEnvironment("Development");
          });



        countryClient = RestService.For<ICountry>(factory.CreateClient());
        kingclient = RestService.For<IKing>(factory.CreateClient());
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await factory.DisposeAsync();
        await MsSqlContainer.DisposeAsync();
    }

}


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<KompetanseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KompetanseContext") ?? throw new InvalidOperationException("Connection string 'KompetanseContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KompetanseContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    //Test data..
    List<Country> countries = new List<Country>
     {
         new Country { Name = "England" },
         new Country { Name = "Germany" },
         new Country { Name = "Norway" },
         new Country { Name = "Sweden" },
     };

    context.AddRange(countries);
    context.SaveChanges();

    List<King> kings = new List<King>
     {
         new King() { Name = "Charles" ,Country=context.Countries.Where(e=>e.Name.Equals("England")).Single()  },
         new King() { Name = "Harald" ,Country=context.Countries.Where(e=>e.Name.Equals("Norway")).Single()  },
         new King() { Name = "Carl" ,Country=context.Countries.Where(e=>e.Name.Equals("Sweden")).Single()  },
     };

    context.AddRange(kings);
    context.SaveChanges();
}

app.Run();
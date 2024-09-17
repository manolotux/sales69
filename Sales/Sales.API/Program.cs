using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sales.API._Helpers;
using Sales.API.Data;
using Sales.API.Servicios;
using Sales.Shared.Entidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(x =>
		x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("local"), new MariaDbServerVersion("11.4.2-MariaDB")));

builder.Services.AddCors(o =>
{
    o.AddPolicy("default", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


//Inyectar servicios
builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IUserHelper, UserHelper>();

var app = builder.Build();

SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("default");

app.MapControllers();

app.Run();
return;

void SeedData(WebApplication appService)
{
	var scopeFactory = appService.Services.GetService<IServiceScopeFactory>();

	using var scope = scopeFactory!.CreateScope();

	var service = scope.ServiceProvider.GetService<SeedDb>();
	service!.SeedAsync().Wait();

}

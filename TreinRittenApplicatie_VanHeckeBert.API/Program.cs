using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TreinRittenApplicatie_VanHeckeBert.API.Models;
using TreinRittenApplicatie_VanHeckeBert.Domain.Data;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Repository;
using TreinRittenApplicatie_VanHeckeBert.Repository.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.Service;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.API.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TrainRide API",
        Version = "version 1",
        Description = "An API to perform Train Ride operations",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "BVH",
            Email = "bert.vanhecke@student.vives.be",
            Url = new Uri("https://vives.be"),
        },
        License = new OpenApiLicense
        {
            Name = "TrainRide API LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<TrainRideDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// add Automapper
builder.Services.AddAutoMapper(typeof(Program));
//DI

builder.Services.AddTransient<IDAO<Station>, StationDAO>();
builder.Services.AddTransient<IService<Station>, StationService>();

builder.Services.AddTransient<IRideDAO, RideDAO>();
builder.Services.AddTransient<IRideService, RideService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var swaggerOptions = new TreinRittenApplicatie_VanHeckeBert.API.Options.SwaggerOptions();
builder.Configuration.GetSection(nameof(TreinRittenApplicatie_VanHeckeBert.API.Options.SwaggerOptions)).Bind(swaggerOptions);
app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
});
app.UseSwagger();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

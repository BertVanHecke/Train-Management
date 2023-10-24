using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TreinRittenApplicatie_VanHeckeBert.Data;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Repository.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.Service;
using TreinRittenApplicatie_VanHeckeBert.Repository;
using TreinRittenApplicatie_VanHeckeBert.Domain.Data;
using TreinRittenApplicatie_VanHeckeBert.Areas.Identity.Data;
using TreinRittenApplicatie_VanHeckeBert.Util.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Trainride context connect
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<TrainRideDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddTransient<IService<Station>, StationService>();
builder.Services.AddTransient<IDAO<Station>, StationDAO>();

builder.Services.AddTransient<IService<Train>, TrainService>();
builder.Services.AddTransient<IDAO<Train>, TrainDAO>();

builder.Services.AddTransient<IRideService, RideService>();
builder.Services.AddTransient<IRideDAO, RideDAO>();

builder.Services.AddTransient<IService<Ticket>, TicketService>();
builder.Services.AddTransient<IDAO<Ticket>, TicketDAO>();

builder.Services.AddTransient<IBookingTicketService, BookingTicketService>();
builder.Services.AddTransient<IBookingTicketDAO, BookingTicketDAO>();

builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<IBookingDAO, BookingDAO>();

builder.Services.AddTransient<IService<Seat>, SeatService>();
builder.Services.AddTransient<IDAO<Seat>, SeatDAO>();

builder.Services.AddAutoMapper(typeof(Program));

//session
builder.Services.AddSession(options =>
{

    options.Cookie.Name = "be.SPEEDTRO.Session";

    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//add session
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

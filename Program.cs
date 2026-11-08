using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Paun_George_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Paun_George_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Paun_George_Lab2Context' not found.");

builder.Services.AddDbContext<Paun_George_Lab2Context>(options =>
    options.UseSqlServer(connectionString));

// Adaugam conexiunea si serviciul pentru LibraryIdentityContext
var identityConnectionString = builder.Configuration.GetConnectionString("LibraryIdentityContextConnection") ?? connectionString;

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(identityConnectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryIdentityContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // Adaugat obligatoriu pentru login/identity
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
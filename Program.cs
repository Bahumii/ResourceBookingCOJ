using Microsoft.EntityFrameworkCore;
using ResourceBookingCOJ.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // Applies any pending migrations

    if (!db.Resources.Any())
    {
        db.Resources.AddRange(new[]
        {
            new Resource { Name = "Boardroom A", Location = "2nd Floor", Capacity = 10, Description = "With projector", IsAvailable = true },
            new Resource { Name = "Company Car 1", Location = "Parking Bay 3", Capacity = 4, Description = "White sedan", IsAvailable = true }
        });

        db.SaveChanges();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


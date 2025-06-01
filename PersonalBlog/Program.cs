using Microsoft.EntityFrameworkCore;
using PersonalBlog.Controllers;
using PersonalBlog.Data;
using PersonalBlog.Filters;
using PersonalBlog.Services;
using PersonalBlog.Services.Accounts;
using PersonalBlog.Services.Articles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PersonalBlogContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("PersonalBlogDb")));
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddSingleton<IAccountsService, AccountsService>();
builder.Services.AddScoped<EnsureLoggedIn>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
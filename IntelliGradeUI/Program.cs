using IntelliGradeUI.Services;
using Microsoft.Extensions.FileProviders;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDirectoryBrowser();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path="/";
        context.Response.Redirect("/");
        await next();
    }
});
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStaticFiles("/wwwroot/images");

app.UseDirectoryBrowser("/wwwroot/images");

app.UseStaticFiles("/wwwroot/assets");

app.UseDirectoryBrowser("/wwwroot/assets");

app.UseStaticFiles("/wwwroot/vendor");

app.UseDirectoryBrowser("/wwwroot/vendor");

app.UseRouting();



app.UseAuthorization();

//default direct to base page

app.MapGet("/", () => Results.Redirect("/Base"));


app.MapRazorPages();


app.Run();

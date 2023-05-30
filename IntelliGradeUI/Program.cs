using Microsoft.Extensions.FileProviders;

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

app.UseRouting();



app.UseAuthorization();

app.MapRazorPages();

app.Run();

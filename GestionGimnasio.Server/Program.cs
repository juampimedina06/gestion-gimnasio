var builder = WebApplication.CreateBuilder(args);

#region Servicios

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


#endregion

var app = builder.Build();

#region MiddleWare
if (!app.Environment.IsDevelopment())
    {
       app.UseExceptionHandler("/Error");
       app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
#endregion

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using OnlineShopCMS.Data;
using OnlineShopCMS.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductContext")));
builder.Services.AddDefaultIdentity<OnlineShopUser>(options => {
    options.Password.RequiredLength = 4;             //密碼長度
    options.Password.RequireLowercase = false;       //包含小寫英文
    options.Password.RequireUppercase = false;       //包含大寫英文
    options.Password.RequireNonAlphanumeric = false; //包含符號
    options.Password.RequireDigit = false;           //包含數字
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<OnlineShopContext>()
    .AddEntityFrameworkStores<OnlineShopContext>();
builder.Services.AddDbContext<OnlineShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineShopContext")));
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.Run();

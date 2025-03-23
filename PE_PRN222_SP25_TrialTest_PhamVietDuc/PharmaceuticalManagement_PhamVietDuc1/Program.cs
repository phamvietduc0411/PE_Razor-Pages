 using Microsoft.EntityFrameworkCore;
using Repository.Repo;
using Service.Services;
using System.Configuration;
using System;
using Repository.Models;

namespace PharmaceuticalManagement_PhamVietDuc1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //DI
            builder.Services.AddScoped<Sp25PharmaceuticalDbContext>();
            builder.Services.AddScoped<AccountRepository>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<ManufactureRepo>();
            builder.Services.AddScoped<ManufactureService>();
            builder.Services.AddScoped<MedicineRepository>();
            builder.Services.AddScoped<MedicineService>();


            builder.Services.AddRazorPages();


            //SESSION

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session hết hạn sau 30 phút
                options.Cookie.HttpOnly = true; // Bảo mật session bằng HttpOnly cookie
                options.Cookie.IsEssential = true; // Bắt buộc sử dụng session
            });

            // Thêm HttpContextAccessor để dùng session trong các lớp Service
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseSession(); // Bật Session

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Login/Login");
                return Task.CompletedTask;
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}

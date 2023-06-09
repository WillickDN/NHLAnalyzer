using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NHLAnalyzer.Data;
using NHLAnalyzer.Data.Seeding;
using NHLAnalyzer.Management.Services;
using NHLAnalyzer.Management.Services.Interfaces;
using NHLAnalyzer.Web.Areas.Identity;

namespace NHLAnalyzer.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Set up the DB Connection
            var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var connectionStringBuilder = new SqlConnectionStringBuilder(defaultConnectionString)
            {
                // Use User Secrets for this information.
                // See: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows#string-replacement-with-secrets
                DataSource = builder.Configuration["DbDataSource"],
                UserID = builder.Configuration["DbUserId"],
                Password = builder.Configuration["DbPassword"]
            };
            var connectionString = connectionStringBuilder.ConnectionString;

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Inject Services
            builder.Services.AddTransient<IPlayerRankingService, PlayerRankingService>();
            builder.Services.AddTransient<IPlayerSeasonService, PlayerSeasonService>();
            builder.Services.AddTransient<ISeasonService, SeasonService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Initialize the database
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                ctx.Database.Migrate();

                SeedData.Initialize(Directory.GetCurrentDirectory(), ctx);
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
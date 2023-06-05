using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Auth0.ManagementApi;
using Example.Auth0.AuthenticationApi.AccessTokenManagement;
using Example.Auth0.AuthenticationApi.Services;
using Pitcher.Data;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;

namespace Pitcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TeamContext>();
                    //DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        })
                            .AddCookie()
                            .AddOpenIdConnect("Auth0", options =>
                            {
                                // Set the authority to your Auth0 domain
                                options.Authority = $"https://{hostContext.Configuration["Auth0:Domain"]}";
                                // Configure the Auth0 Client ID and Client Secret
                                options.ClientId = hostContext.Configuration["Auth0:ClientId"];
                                options.ClientSecret = hostContext.Configuration["Auth0:ClientSecret"];

                                // Set response type to code
                                options.ResponseType = "code";

                                // Configure the scope
                                options.Scope.Clear();
                                options.Scope.Add("openid");
                                options.Scope.Add("profile");
                                options.Scope.Add("email");
                                options.Scope.Add("openid email profile");
                                // Set the correct name claim type
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    NameClaimType = "Name",
                                    RoleClaimType = "https://schemas.Aussie_Tenant.com"
                                };

                                // Set the callback path, so Auth0 will call back to http://localhost:5001/callback
                                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                                options.CallbackPath = new PathString("/callback");

                                // Configure the Claims Issuer to be Auth0
                                options.ClaimsIssuer = "Auth0";

                                // Saves tokens to the AuthenticationProperties
                                options.SaveTokens = true;

                                options.Events = new OpenIdConnectEvents
                                {
                                    // handle the logout redirection
                                    OnRedirectToIdentityProviderForSignOut = (context) =>
                                    {
                                        var logoutUri =
                                            $"https://{hostContext.Configuration["Auth0:Domain"]}/v2/logout?client_id={hostContext.Configuration["Auth0:ClientId"]}";

                                        var postLogoutUri = context.Properties.RedirectUri;
                                        if (!string.IsNullOrEmpty(postLogoutUri))
                                        {
                                            if (postLogoutUri.StartsWith("/"))
                                            {
                                                // transform to absolute
                                                var request = context.Request;
                                                postLogoutUri =
                                                    request.Scheme + "://" + request.Host + request.PathBase +
                                                    postLogoutUri;
                                            }

                                            logoutUri +=
                                                $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
                                        }

                                        context.Response.Redirect(logoutUri);
                                        context.HandleResponse();

                                        return Task.CompletedTask;
                                    }
                                };
                            });

                        DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
                        services.AddDbContext<TeamContext>(options =>
                            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

                        // Add the whole configuration object here.
                        services.AddSingleton<IConfiguration>(hostContext.Configuration);

                        // Add the Auth0 HttpClientManagementConnection.
                        services.AddSingleton<IManagementConnection, HttpClientManagementConnection>();
                        services.AddRazorPages();
                        services.AddControllersWithViews()
                            .AddNewtonsoftJson(options =>
                                options.SerializerSettings.ContractResolver =
                                    new CamelCasePropertyNamesContractResolver());
                        services.AddAccessTokenManagement(hostContext.Configuration);
                        services.AddTransient<IUserService, UserService>();
                    })
                    .Configure(app =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
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
                        app.UseCookiePolicy();
                        app.UseAuthentication();
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                        });
                    });
                });
    }
}

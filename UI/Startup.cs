using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Services.ScopeLibrary.ScopeHandler;
using Services.ScopeLibrary.ScopeRequirement;
using Services.Tools;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using UI.Repositorios;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Set variables
            string domain = Configuration["Auth0:Domain"];
            string clientId = Configuration["Auth0:ClientId"];
            string clientSecret = Configuration["Auth0:ClientSecret"];
            string audience = Configuration["Auth0:Audience"];
            string hostUri = Configuration["Host:Uri"];


            services.AddLocalization();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSyncfusionBlazor();
            services.AddSignalR(e => {
                e.MaximumReceiveMessageSize = 65536;
            });
            //services.AddSignalR(e => { e.MaximumReceiveMessageSize = 65536; }).AddAzureSignalR();


            var httpClientHanndler = new HttpClientHandler();
            httpClientHanndler.ServerCertificateCustomValidationCallback =
                (message, cert, chai, errors) => true;

            services.AddSingleton(new HttpClient(httpClientHanndler)
            {
                BaseAddress = new Uri(hostUri)
            });

            #region Auth0 implementation
            /*-------------------------------*/
            /* Código para implementar Auth0 */
            /*-------------------------------*/
            /*** Comento esta parte del código de políticas de uso de cookie por la selección manual de idioma ***/
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            // Add authentication services
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
                options.Authority = $"https://{domain}";

                // Configure the Auth0 Client ID and Client Secret
                options.ClientId = clientId;
                options.ClientSecret = clientSecret;

                // Set response type to code
                options.ResponseType = "code";

                // Configure the scope
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");


                // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                options.CallbackPath = new PathString("/callback");

                // Configure the Claims Issuer to be Auth0
                options.ClaimsIssuer = "Auth0";

                // Saves tokens to the AuthenticationProperties
                options.SaveTokens = true;

                // Authorization //
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = $"https://schemas.{domain}/roles"
                };

                options.Events = new OpenIdConnectEvents
                {
                    //Add audience parameter
                    OnRedirectToIdentityProvider = context =>
                    {
                        context.ProtocolMessage.SetParameter("audience", audience);
                        return Task.FromResult(0);
                    },

                    //Add event when token were validated
                    OnTokenValidated = context =>
                    {
                        //set security token
                        string secJwt = context.SecurityToken.RawData;
                        //set access token
                        string accessJwt = context.TokenEndpointResponse.AccessToken;
                        //read access token
                        var handler = new JwtSecurityTokenHandler();
                        var jwt = handler.ReadJwtToken(accessJwt);
                        //get permissions from access token
                        List<Claim> permissions = jwt.Claims.Where(x => x.Type == "permissions").ToList();
                        //add permission to user claims (in this case add new identity)
                        context.Principal.AddIdentity(new ClaimsIdentity(permissions));

                        //this is a test to get response from api
                        //in this part yo have to save token on a global var and call api on page component
                        //
                        //var client3 = new RestClient("https://localhost:44339/WeatherForecast");
                        //var request3 = new RestRequest(Method.GET);
                        //request3.AddHeader("authorization", $"Bearer {accessJwt}");
                        //IRestResponse response3 = client3.Execute(request3);
                        //
                        //

                        return Task.FromResult(0);
                    },

                    // handle the logout redirection
                    OnRedirectToIdentityProviderForSignOut = (context) =>
                    {
                        var logoutUri = $"https://{domain}/v2/logout?client_id={clientId}";

                        var postLogoutUri = context.Properties.RedirectUri;
                        if (!string.IsNullOrEmpty(postLogoutUri))
                        {
                            if (postLogoutUri.StartsWith("/"))
                            {
                                // transform to absolute
                                var request = context.Request;
                                postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                            }
                            logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                        }

                        context.Response.Redirect(logoutUri);
                        context.HandleResponse();

                        return Task.CompletedTask;
                    }
                };
            });

            /*------------------*/
            /* AddAuthorization */
            /*------------------*/
            // Add policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.Requirements.Add(new HasScopeRequirement("read:log", $"https://{domain}/")));
                options.AddPolicy("User", policy => policy.Requirements.Add(new HasScopeRequirement("read:patient", $"https://{domain}/")));
            });
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddHttpContextAccessor();
            #endregion

            services.AddHttpClient();
            services.AddScoped<IRepositorio, Repositorio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Licencia de Syncfusion
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTA1NDM0QDMxMzkyZTMyMmUzMExBZGsrdExCU1BhZVpvcDZBOUVKaXRXVG1JR0JhbjhtdHVuVU80bEtHOWc9");


            #region Cultura
            /* Cultura */
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                SupportedCultures = culturasHelper.CulturasSoportadas,
                SupportedUICultures = culturasHelper.CulturasSoportadas,
                DefaultRequestCulture = new RequestCulture("en-US") //Cultura por defecto
            });
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

        }
    }
}

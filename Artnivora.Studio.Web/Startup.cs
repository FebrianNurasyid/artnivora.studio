using Evisi.Framework.Configuration;
using FluentMigrator.Runner;
using Artnivora.Studio.Portal.Business.Services;
using Artnivora.Studio.Portal.Business.Services.Helpers;
using Artnivora.Studio.Portal.Data.Interfaces;
using Artnivora.Studio.Portal.Data.Services;
using Artnivora.Studio.Portal.Data.Services.Helpers;
using Artnivora.Studio.Portal.Data.Services.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http;

namespace Artnivora.Studio.Portal.Web
{
    public class Startup
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string databaseConnectionString =
                ConfigurationManager.Instance.GetConnectionString(
                    DatabaseConfigurationHelper.databaseConnectionString
                );

            services.AddControllersWithViews();

            // Configure IIS Services:
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "App/build";
            });

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(databaseConnectionString)
            );

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Hervormde vrouwenbond", Version = "v1" });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(databaseConnectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(DatabaseMigration).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            var builder = new ContainerBuilder();

            // Business layer servives
            builder.RegisterType<UserService>().InstancePerDependency();
            builder.RegisterType<UserProfileService>().InstancePerDependency();
            builder.RegisterType<UserAuthService>().InstancePerDependency();
            builder.RegisterType<UserRoleService>().InstancePerDependency();
            builder.RegisterType<MessageBoxService>().InstancePerDependency();
            builder.RegisterType<ParticipantProfileService>().InstancePerDependency();
            builder.RegisterType<VolunteerProfileService>().InstancePerDependency();
            builder.RegisterType<EmailService>().InstancePerDependency();
            builder.RegisterType<ProductionServices>().InstancePerDependency();

            // Data layer services
            builder.RegisterType<UserDataService>()
                .As<IUserDataService>()
                .InstancePerDependency();
            builder.RegisterType<UserProfileDataService>()
            .As<IUserProfileDataService>()
            .InstancePerDependency();

            builder.RegisterType<UserRoleDataService>()
                .As<IUserRoleDataService>()
                .InstancePerDependency();

            builder.RegisterType<ParticipantProfileDataService>()
            .As<IParticipantProfileDataService>()
            .InstancePerDependency();

            builder.RegisterType<VolunteerProfileDataService>()
            .As<IVolunteerProfileDataService>()
            .InstancePerDependency();

            builder.RegisterType<VolunteerFunctionDataService>()
            .As<IVolunteerFunctionDataService>()
            .InstancePerDependency();

            builder.RegisterType<MessageBoxDataService>()
            .As<IMessageBoxDataService>()
            .InstancePerDependency();

            builder.RegisterType<ProductionDataService>()
            .As<IProductionDataService>()
            .InstancePerDependency();

            builder.Populate(services);            

            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hervormde vrouwenbond V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "App";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            migrationRunner.MigrateUp();
            Logger.Info("Database migration done, server is running!");

        }
    }
}

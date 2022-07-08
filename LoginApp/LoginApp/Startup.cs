using LoginApp.Infraestructura;
using LoginApp.Infraestructura.Repositorio;
using LoginApp.Servicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.FileProviders;

namespace LoginApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
            services.AddDbContext<ContextoLogin>(options => options.UseSqlServer(Configuration.GetConnectionString("LoginConectionString").Replace("[DataDirectory]",path)));
            services.AddControllers();
            services.AddScoped<IServicioToken, ServicioToken>();
            services.AddScoped<IRepositorioToken, RepositorioToken>();
            services.AddHostedService<ServicioAutomatico>();
            //Carga la página estática de React

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Web/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Web/build/static")),
                RequestPath = "/static"
            });

            //Inicia el Cliente
            app.UseSpa(spa =>
              {
                  spa.Options.SourcePath = "Web";
                  if (env.IsDevelopment())
                  {
                      spa.UseReactDevelopmentServer(npmScript: "start");
                  }
              }   
            );
        }
    }
}

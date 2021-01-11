using CadContato.Domain.Handlers;
using CadContato.Domain.Infra.Contexts;
using CadContato.Domain.Infra.Repositories;
using CadContato.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CadContato.WebApi
{
    public class Startup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Banco de Dados
            //services.AddDbContext<DataContext>(opt => opt.UseSqlite(Environment.GetEnvironmentVariable("ConnStr")));
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("db"));

            //Repositorios
            services.AddTransient<IContatoRepository, ContatoRepository>();

            //Handlers
            services.AddTransient<ContatoHandler, ContatoHandler>();

            //Jwt Authentication
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.Authority = Environment.GetEnvironmentVariable("JwtAuthority");
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer"),
            //        ValidateAudience = true,
            //        ValidAudience = Environment.GetEnvironmentVariable("JwtValidAudience"),
            //        ValidateLifetime = true
            //    };
            //});

            //services.AddNewtonsotJson(x => x.)

            //Controllers
            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Insert(0, new Util.CustomJsonConverter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

            //Jwt Auth
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

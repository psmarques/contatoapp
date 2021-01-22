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
            //services.AddDbContext<DataContext>(opt => opt.UseSqlite(Config.GetConnectionString("SQLite")));
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("db"));

            //Repositorios
            services.AddTransient<IContatoRepository, ContatoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            //Handlers
            services.AddTransient<ContatoHandler, ContatoHandler>();

            //Google Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt => jwt.UseGoogle(Environment.GetEnvironmentVariable("CadContato_GoogleClientId")));

            //Custom Auth
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.Authority = Environment.GetEnvironmentVariable("CadContato:JwtAuthority");
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = Environment.GetEnvironmentVariable("CadContato:JwtIssuer"),
            //        ValidateAudience = true,
            //        ValidAudience = Environment.GetEnvironmentVariable("CadContato:JwtValidAudience"),
            //        ValidateLifetime = true
            //    };
            //});

            //Controllers
            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Insert(0, new Util.CustomJsonConverter());
                opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
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

            //TODO: Configurar o Cors
            app.UseCors(x => x.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

            //Jwt Auth
            app.UseAuthentication();
            app.UseAuthorization();

            //Mapear Controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

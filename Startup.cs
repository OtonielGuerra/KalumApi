using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//Agregamos esta librería
using Microsoft.EntityFrameworkCore;
using KalumNotas.KalumBDContext;
using AutoMapper;
using KalumNotas.Entities;
using KalumNotas.Models;

namespace KalumNotas
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
            services.AddAutoMapper(configuration => {

                configuration.CreateMap<Alumno, AlumnoDTO>()
                    .ConstructUsing(Alumno => new AlumnoDTO{FullName = $"{Alumno.Nombres} {Alumno.Apellidos}"});

                configuration.CreateMap<AsignacionAlumno, AsignacionAlumnoDTO>();

                configuration.CreateMap<DetalleNota, DetalleNotaDTO>();

                configuration.CreateMap<Seminario, SeminarioDTO>();

                configuration.CreateMap<DetalleActividad, DetalleActividadDTO>();
                
            },
            typeof(Startup));
            services.AddDbContext<KalumNotasBDContext>(options => 
                options.UseSqlServer(Configuration
                    .GetConnectionString("DefaultConnectionString"))
            );
            services.AddControllers().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

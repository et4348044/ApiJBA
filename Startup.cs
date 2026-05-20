using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ApiJBA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Registrar ApplicationDbContext con Pooling para maximizar el rendimiento en CPU de pocos recursos
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configurar los controladores ignorando los ciclos de referencia en JSON
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Generador de Swagger
            services.AddSwaggerGen();

            // Configuración de AutoMapper buscando los perfiles del proyecto
            services.AddAutoMapper(typeof(Startup));

            // Configurar CORS para permitir solicitudes desde cualquier origen
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configurar el pipeline de solicitudes HTTP
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

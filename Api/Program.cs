
using Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Negocio;
using System.Reflection;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Api Prueba .Net",
                    Version = "v1",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);

            });

            builder.Services.AddDbContext<ProductDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConection")));
            builder.Services.AddAutoMapper(typeof(ProductoProfile));

            builder.Services.AddSingleton<IServicioDescuento,ServicioDescuento>();
            builder.Services.AddSingleton<IServicioEstado, ServicioEstado>();
            builder.Services.AddScoped<IProductoNegocio, ProductoNegocio>();

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<RequestTime>();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.MapControllers();

            app.Run();
        }
    }
}

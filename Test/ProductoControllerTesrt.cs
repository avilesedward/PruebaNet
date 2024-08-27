using Microsoft.AspNetCore.Mvc.Testing;
using Api;
using System.Net.Http.Json;
using System.Net;
using Modelos;
using DTO;

namespace Test
{
    public class ProductoControllerTesrt
    {
        [Fact]
        public async Task Controllador_Funcionando()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetStringAsync("/api/Producto/Health");

            Assert.Equal("OK", response);
        }


        [Fact]
        public async Task Crear_Nuevo_Status400BadRequest_Por_Nombre()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.PostAsJsonAsync("/api/Producto", new ProductoDto
            {
                ProductId = 0,
                Stock = 10,
                Description = "Descripcion producto",
                Price = 100,
                StatusName = "Active",
                Discount = 5,
                FinalPrice = 95

            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Crear_Nuevo_Status400BadRequest_Por_Precio()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.PostAsJsonAsync("/api/Producto", new ProductoDto
            {
                ProductId = 0,
                Name = "Producto",
                Stock = 10,
                Description = "Descripcion producto",
                Price = 0,
                StatusName = "Active",
                Discount = 5,
                FinalPrice = 95

            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Crear_Nuevo_Status201Created()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.PostAsJsonAsync("/api/Producto", new ProductoDto
            {
                ProductId = 0,
                Name = "Producto " + DateTime.Now.ToString("hhmmss"),
                Stock = 10,
                Description = "Descripcion producto",
                Price = 100,
                StatusName = "Active",
                Discount = 5,
                FinalPrice = 95

            });

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

    }
}
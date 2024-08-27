using AutoMapper;
using DTO;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoProfile: Profile
    {
        public ProductoProfile() {
            CreateMap<Producto, ProductoDto>();
            CreateMap<ProductoDto, Producto>();
        }    
    }
}

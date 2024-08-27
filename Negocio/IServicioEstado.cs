using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public interface IServicioEstado
    {
        public Task<IDictionary<int,string>> GetEstados();
    }
}

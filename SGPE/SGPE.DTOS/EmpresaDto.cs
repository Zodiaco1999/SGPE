using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPE.DTOS
{
    public class EmpresaDto
    {
        public long IdEmpresa { get; set; }
        public string Nit { get; set; } = null!;
        public string NombreEmpresa { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
    }
}

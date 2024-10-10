using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPE.DTOS
{
    public class RolDto
    {
        public long IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
        public string? DescripcionRol { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPE.DTOS
{
    public class UsuarioLoginDto
    {
        public string CedulaUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
    }
}

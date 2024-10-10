using AutoMapper;
using SGPE.DTOSM;

namespace SGPE.WebApi.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Producto
            CreateMap<Producto, ProductoDto>()
                .ForMember(dest => dest.CategoriaProducto,
                    opt => opt.MapFrom(src => src.IdCategoriaProductoNavigation.NombreCategoriaProducto))
                .ForMember(dest => dest.NombreEmpresa,
                    opt => opt.MapFrom(src => src.IdEmpresaNavigation.NombreEmpresa))
                .ForMember(dest => dest.EstadoProducto,
                    opt => opt.MapFrom(src => src.IdEstadoProductoNavigation.DescripcionEstadoProducto));

            CreateMap<ProductoEditDto, Producto>().ReverseMap();

            //Usuario
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.NombreEmpresa,
                    opt => opt.MapFrom(src => src.IdEmpresaNavigation!.NombreEmpresa));

            CreateMap<Usuario, UsuarioEditDto>()
                .ForMember(dest => dest.NombreEmpresa,
                    opt => opt.MapFrom(src => src.IdEmpresaNavigation!.NombreEmpresa));

            CreateMap<UsuarioEditDto, Usuario>();
            CreateMap<Usuario, UsuarioLoginDto>();

            //UsuarioPerfil
            CreateMap<UsuarioPerfilDto, UsuarioPerfil>();
            CreateMap<UsuarioPerfil, UsuarioPerfilDto>()
                .ForMember(dest => dest.NombrePerfil,
                    opt => opt.MapFrom(src => src.IdPerfilNavigation.NombrePerfil));

            //Pedido
            CreateMap<Pedido, PedidoDto>()
                .ForMember(dest => dest.FechaSolicita,
                    opt => opt.MapFrom(src => src.CreaFecha))
                .ForMember(dest => dest.EstadoPedido,
                    opt => opt.MapFrom(src => src.IdEstadoPedidoNavigation.DescripcionEstadoPedido))
                .ForMember(dest => dest.CantidadProductos,
                    opt => opt.MapFrom(src => src.DetallePedidos.Count))
                .ForMember(dest => dest.Usuario,
                    opt => opt.MapFrom(src => $"{src.IdUsuarioSolicitaNavigation.Nombres} {src.IdUsuarioSolicitaNavigation.Apellidos}"))
                .ForMember(dest => dest.CedulaUsuario,
                    opt => opt.MapFrom(src => src.IdUsuarioSolicitaNavigation.CedulaUsuario));

            CreateMap<PedidoEditDto, Pedido>();

            CreateMap<Producto, ProductoPedidoDto>()
                .ForMember(dest => dest.Cantidad, p => p.AllowNull());

            //Pedido Detalle
            CreateMap<DetallePedido, DetallePedidoDto>()
                .ForMember(dest => dest.CodigoErp,
                    opt => opt.MapFrom(src => src.IdProductoNavigation.CodigoErp))
                .ForMember(dest => dest.DescripcionProducto, 
                    opt => opt.MapFrom(src => src.IdProductoNavigation.DescripcionProducto))
                .ForMember(dest => dest.Precio,
                    opt => opt.MapFrom(src => src.IdProductoNavigation.Precio));

            CreateMap<DetallePedidoDto, DetallePedido>();
            CreateMap<DetallePedidoEditDto, Producto>();

            //CategoriaProducto
            // Estoy trayendo el nombre de la empresa por medio de la relacion
            CreateMap<CategoriaProducto, CategoriaProductoDto>()
                .ForMember(dest => dest.NombreEmpresa,
                    opt => opt.MapFrom(src => src.IdEmpresaNavigation.NombreEmpresa));

            CreateMap<CategoriaProductoDto, CategoriaProducto>();
            //Empresa
            CreateMap<EmpresaDto, Empresa>().ReverseMap();
            //Modulo
            CreateMap<ModuloDto, Modulo>().ReverseMap();
            //Menu
            CreateMap<MenuDto, Menu>()
                .ReverseMap()
                .ForMember(dest => dest.Visible,
                    opt => opt.MapFrom(src => src.Orden > 0));
            //Perfil
            CreateMap<Perfil, PerfilDto>();
            CreateMap<PerfilEditDto, Perfil>();
            //PerfilMenu
            CreateMap<PerfilMenuEditDto, PerfilMenu>();
            CreateMap<PerfilMenu, PerfilMenuDto>()
                .ForMember(dest => dest.MenuConsulta,
                    opt => opt.MapFrom(src => src.IdM.Consulta))
                .ForMember(dest => dest.MenuInserta,
                    opt => opt.MapFrom(src => src.IdM.Inserta))
                .ForMember(dest => dest.MenuActualiza,
                    opt => opt.MapFrom(src => src.IdM.Actualiza))
                .ForMember(dest => dest.MenuElimina,
                   opt => opt.MapFrom(src => src.IdM.Elimina))
                .ForMember(dest => dest.MenuActiva,
                    opt => opt.MapFrom(src => src.IdM.Activa))
                .ForMember(dest => dest.MenuEjecuta,
                    opt => opt.MapFrom(src => src.IdM.Ejecuta))
                .ForMember(dest => dest.NombreMenu,
                    opt => opt.MapFrom(src => src.IdM.NombreMenu))
                .ForMember(dest => dest.DescMenu,
                    opt => opt.MapFrom(src => src.IdM.DescMenu))
                .ForMember(dest => dest.NombreModulo,
                    opt => opt.MapFrom(src => src.IdModuloNavigation.NombreModulo));
            CreateMap<Menu, PerfilMenuDto>()
                .ForMember(dest => dest.Consulta, p => p.MapFrom(p => false))
                .ForMember(dest => dest.Inserta, p => p.MapFrom(p => false))
                .ForMember(dest => dest.Actualiza, p => p.MapFrom(p => false))
                .ForMember(dest => dest.Elimina, p => p.MapFrom(p => false))
                .ForMember(dest => dest.Activa, p => p.MapFrom(p => false))
                .ForMember(dest => dest.Ejecuta, p => p.MapFrom(p => false))
                .ForMember(dest => dest.MenuConsulta,
                    opt => opt.MapFrom(src => src.Consulta))
                .ForMember(dest => dest.MenuInserta,
                    opt => opt.MapFrom(src => src.Inserta))
                .ForMember(dest => dest.MenuActualiza,
                    opt => opt.MapFrom(src => src.Actualiza))
                .ForMember(dest => dest.MenuElimina,
                   opt => opt.MapFrom(src => src.Elimina))
                .ForMember(dest => dest.MenuActiva,
                    opt => opt.MapFrom(src => src.Activa))
                .ForMember(dest => dest.MenuEjecuta,
                    opt => opt.MapFrom(src => src.Ejecuta))
                .ForMember(dest => dest.NombreModulo,
                    opt => opt.MapFrom(src => src.IdModuloNavigation.NombreModulo));
        }
    }
}

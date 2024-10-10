using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SGPE.WebApi.DataAccess;

namespace SGPE.WebApi.Context;

public partial class SGPEContext : DbContext
{
    public SGPEContext()
    {
    }

    public SGPEContext(DbContextOptions<SGPEContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArchivoPedidoDescargado> ArchivoPedidoDescargados { get; set; }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<EstadoProducto> EstadoProductos { get; set; }

    public virtual DbSet<LogAccionUsuario> LogAccionUsuarios { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<PerfilMenu> PerfilMenus { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioPerfil> UsuarioPerfils { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<ArchivoPedidoDescargado>(entity =>
        {
            entity.HasKey(e => e.IdArchivoPedidoDescargado);

            entity.ToTable("ArchivoPedidoDescargado");

            entity.Property(e => e.IdArchivoPedidoDescargado).HasColumnName("idArchivoPedidoDescargado");
            entity.Property(e => e.CantidadPedidosDescargados).HasColumnName("cantidadPedidosDescargados");
            entity.Property(e => e.ContenidoArchivo).HasColumnName("contenidoArchivo");
            entity.Property(e => e.FechaDescarga)
                .HasColumnType("datetime")
                .HasColumnName("fechaDescarga");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaProducto);

            entity.ToTable("CategoriaProducto");

            entity.Property(e => e.IdCategoriaProducto).HasColumnName("idCategoriaProducto");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("activo");
            entity.Property(e => e.CodigoCategoriaProducto)
                .HasMaxLength(100)
                .HasColumnName("codigoCategoriaProducto");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.NombreCategoriaProducto)
                .HasMaxLength(200)
                .HasColumnName("nombreCategoriaProducto");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.CategoriaProductos)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoriaProducto_Empresa");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedido).HasName("PK_DetallePedido_1");

            entity.ToTable("DetallePedido", tb => tb.HasTrigger("trdelDetallePedido"));

            entity.Property(e => e.IdDetallePedido)
                .ValueGeneratedNever()
                .HasColumnName("idDetallePedido");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CreaFecha)
                .HasColumnType("datetime")
                .HasColumnName("creaFecha");
            entity.Property(e => e.CreaMaquina)
                .HasMaxLength(50)
                .HasColumnName("creaMaquina");
            entity.Property(e => e.CreaUsuario)
                .HasMaxLength(50)
                .HasColumnName("creaUsuario");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.ModificaFecha)
                .HasColumnType("datetime")
                .HasColumnName("modificaFecha");
            entity.Property(e => e.ModificaMaquina)
                .HasMaxLength(50)
                .HasColumnName("modificaMaquina");
            entity.Property(e => e.ModificaUsuario)
                .HasMaxLength(50)
                .HasColumnName("modificaUsuario");
            entity.Property(e => e.SubTotal)
                .HasComment("Este es valor aproximado, ya que el valor lo calcula el SIC.")
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("subTotal");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_Pedido");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_Producto");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa);

            entity.ToTable("Empresa");

            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nit)
                .HasMaxLength(100)
                .HasColumnName("nit");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(200)
                .HasColumnName("nombreEmpresa");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido).HasName("PK_Table_1_1");

            entity.ToTable("EstadoPedido");

            entity.Property(e => e.IdEstadoPedido).HasColumnName("idEstadoPedido");
            entity.Property(e => e.DescripcionEstadoPedido)
                .HasMaxLength(2000)
                .HasColumnName("descripcionEstadoPedido");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<EstadoProducto>(entity =>
        {
            entity.HasKey(e => e.IdEstadoProducto);

            entity.ToTable("EstadoProducto");

            entity.Property(e => e.IdEstadoProducto).HasColumnName("idEstadoProducto");
            entity.Property(e => e.DescripcionEstadoProducto)
                .HasMaxLength(200)
                .HasColumnName("descripcionEstadoProducto");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<LogAccionUsuario>(entity =>
        {
            entity.HasKey(e => e.IdLogAccionUsuario);

            entity.ToTable("LogAccionUsuario");

            entity.Property(e => e.IdLogAccionUsuario).HasColumnName("idLogAccionUsuario");
            entity.Property(e => e.Accion)
                .HasMaxLength(100)
                .HasComment("CREAR_PEDIDO (Cuando el usuario confirma el pedido) \r\nDESCARGAR_PEDIDOS (Cuando el rol_administrador descarga pedidos)\r\n")
                .HasColumnName("accion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRolUsuario).HasColumnName("idRolUsuario");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => new { e.IdMenu, e.IdModulo });

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdModulo).HasColumnName("idModulo");
            entity.Property(e => e.Activa).HasColumnName("activa");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Actualiza).HasColumnName("actualiza");
            entity.Property(e => e.Consulta).HasColumnName("consulta");
            entity.Property(e => e.CreaFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creaFecha");
            entity.Property(e => e.CreaMaquina)
                .HasMaxLength(50)
                .HasDefaultValueSql("(host_name())")
                .HasColumnName("creaMaquina");
            entity.Property(e => e.CreaUsuario)
                .HasMaxLength(50)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("creaUsuario");
            entity.Property(e => e.DescMenu)
                .HasMaxLength(250)
                .HasColumnName("descMenu");
            entity.Property(e => e.Ejecuta).HasColumnName("ejecuta");
            entity.Property(e => e.Elimina).HasColumnName("elimina");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.EtiquetaMenu)
                .HasMaxLength(50)
                .HasColumnName("etiquetaMenu");
            entity.Property(e => e.Inserta).HasColumnName("inserta");
            entity.Property(e => e.ModificaFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modificaFecha");
            entity.Property(e => e.ModificaMaquina)
                .HasMaxLength(50)
                .HasDefaultValueSql("(host_name())")
                .HasColumnName("modificaMaquina");
            entity.Property(e => e.ModificaUsuario)
                .HasMaxLength(50)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("modificaUsuario");
            entity.Property(e => e.NombreMenu)
                .HasMaxLength(50)
                .HasColumnName("nombreMenu");
            entity.Property(e => e.Orden).HasColumnName("orden");
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .HasColumnName("url");

            entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdModulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menu_Modulo");
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo).HasName("PK__Modulo__D9F15315AF36E41C");

            entity.ToTable("Modulo");

            entity.Property(e => e.IdModulo)
                .ValueGeneratedNever()
                .HasColumnName("idModulo");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("activo");
            entity.Property(e => e.CreaFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creaFecha");
            entity.Property(e => e.CreaMaquina)
                .HasMaxLength(150)
                .HasDefaultValueSql("(host_name())")
                .HasColumnName("creaMaquina");
            entity.Property(e => e.CreaUsuario)
                .HasMaxLength(150)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("creaUsuario");
            entity.Property(e => e.DescModulo)
                .HasMaxLength(50)
                .HasColumnName("descModulo");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.IconoNombre)
                .HasMaxLength(50)
                .HasColumnName("iconoNombre");
            entity.Property(e => e.IconoPrefijo)
                .HasMaxLength(50)
                .HasColumnName("iconoPrefijo");
            entity.Property(e => e.ModificaFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modificaFecha");
            entity.Property(e => e.ModificaMaquina)
                .HasMaxLength(150)
                .HasDefaultValueSql("(host_name())")
                .HasColumnName("modificaMaquina");
            entity.Property(e => e.ModificaUsuario)
                .HasMaxLength(150)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("modificaUsuario");
            entity.Property(e => e.NombreModulo)
                .HasMaxLength(50)
                .HasColumnName("nombreModulo");
            entity.Property(e => e.Orden).HasColumnName("orden");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido);

            entity.ToTable("Pedido");

            entity.Property(e => e.IdPedido)
                .ValueGeneratedNever()
                .HasColumnName("idPedido");
            entity.Property(e => e.CreaFecha)
                .HasColumnType("datetime")
                .HasColumnName("creaFecha");
            entity.Property(e => e.CreaMaquina)
                .HasMaxLength(50)
                .HasColumnName("creaMaquina");
            entity.Property(e => e.CreaUsuario)
                .HasMaxLength(50)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("creaUsuario");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.FechaPedidoDescarga)
                .HasColumnType("datetime")
                .HasColumnName("fechaPedidoDescarga");
            entity.Property(e => e.IdEstadoPedido).HasColumnName("idEstadoPedido");
            entity.Property(e => e.IdUsuarioDescarga).HasColumnName("idUsuarioDescarga");
            entity.Property(e => e.IdUsuarioSolicita).HasColumnName("idUsuarioSolicita");
            entity.Property(e => e.ModificaFecha)
                .HasColumnType("datetime")
                .HasColumnName("modificaFecha");
            entity.Property(e => e.ModificaMaquina)
                .HasMaxLength(50)
                .HasColumnName("modificaMaquina");
            entity.Property(e => e.ModificaUsuario)
                .HasMaxLength(50)
                .HasColumnName("modificaUsuario");
            entity.Property(e => e.ValorTotal)
                .HasComment("Este valor total es con IVA incluido, aun asi es valor aproximado, quien liquida es el SIC")
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("valorTotal");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdEstadoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_EstadoPedido");

            entity.HasOne(d => d.IdUsuarioDescargaNavigation).WithMany(p => p.PedidoIdUsuarioDescargaNavigations)
                .HasForeignKey(d => d.IdUsuarioDescarga)
                .HasConstraintName("FK_Pedido_Usuario1");

            entity.HasOne(d => d.IdUsuarioSolicitaNavigation).WithMany(p => p.PedidoIdUsuarioSolicitaNavigations)
                .HasForeignKey(d => d.IdUsuarioSolicita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Usuario");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.IdPerfil);

            entity.ToTable("Perfil");

            entity.Property(e => e.IdPerfil)
                .ValueGeneratedNever()
                .HasColumnName("idPerfil");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("activo");
            entity.Property(e => e.CreaFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creaFecha");
            entity.Property(e => e.CreaMaquina)
                .HasMaxLength(50)
                .HasDefaultValueSql("(host_name())")
                .HasColumnName("creaMaquina");
            entity.Property(e => e.CreaUsuario)
                .HasMaxLength(50)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("creaUsuario");
            entity.Property(e => e.DescPerfil)
                .HasMaxLength(250)
                .HasColumnName("descPerfil");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.ModificaFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modificaFecha");
            entity.Property(e => e.ModificaMaquina)
                .HasMaxLength(50)
                .HasDefaultValueSql("(host_name())")
                .HasColumnName("modificaMaquina");
            entity.Property(e => e.ModificaUsuario)
                .HasMaxLength(50)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("modificaUsuario");
            entity.Property(e => e.NombrePerfil)
                .HasMaxLength(50)
                .HasColumnName("nombrePerfil");
        });

        modelBuilder.Entity<PerfilMenu>(entity =>
        {
            entity.HasKey(e => new { e.IdPerfil, e.IdMenu }).HasName("PK_PerfilMenu_1");

            entity.ToTable("PerfilMenu");

            entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Activa).HasColumnName("activa");
            entity.Property(e => e.Actualiza).HasColumnName("actualiza");
            entity.Property(e => e.Consulta).HasColumnName("consulta");
            entity.Property(e => e.Ejecuta).HasColumnName("ejecuta");
            entity.Property(e => e.Elimina).HasColumnName("elimina");
            entity.Property(e => e.IdModulo).HasColumnName("idModulo");
            entity.Property(e => e.Inserta).HasColumnName("inserta");

            entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.PerfilMenus)
                .HasForeignKey(d => d.IdModulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilMenu_Modulo");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.PerfilMenus)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilMenu_Perfil");

            entity.HasOne(d => d.IdM).WithMany(p => p.PerfilMenus)
                .HasForeignKey(d => new { d.IdMenu, d.IdModulo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilMenu_Menu");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.CodigoErp).HasColumnName("codigoErp");
            entity.Property(e => e.CreaFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creaFecha");
            entity.Property(e => e.CreaMaquina)
                .HasMaxLength(50)
                .HasDefaultValueSql("(host_name())")
                .HasColumnName("creaMaquina");
            entity.Property(e => e.CreaUsuario)
                .HasMaxLength(50)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("creaUsuario");
            entity.Property(e => e.DescripcionProducto)
                .HasMaxLength(500)
                .HasColumnName("descripcionProducto");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.IdCategoriaProducto).HasColumnName("idCategoriaProducto");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdEstadoProducto)
                .HasDefaultValueSql("((1))")
                .HasColumnName("idEstadoProducto");
            entity.Property(e => e.ImagenBase64).HasColumnName("imagenBase64");
            entity.Property(e => e.ModificaFecha)
                .HasColumnType("datetime")
                .HasColumnName("modificaFecha");
            entity.Property(e => e.ModificaMaquina)
                .HasMaxLength(50)
                .HasColumnName("modificaMaquina");
            entity.Property(e => e.ModificaUsuario)
                .HasMaxLength(50)
                .HasColumnName("modificaUsuario");
            entity.Property(e => e.OrdenVisualizacion).HasColumnName("ordenVisualizacion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdCategoriaProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoriaProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_CategoriaProducto");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Empresa");

            entity.HasOne(d => d.IdEstadoProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEstadoProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_EstadoProducto");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("idUsuario");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(300)
                .HasColumnName("apellidos");
            entity.Property(e => e.CedulaUsuario)
                .HasMaxLength(100)
                .HasColumnName("cedulaUsuario");
            entity.Property(e => e.Correo)
                .HasMaxLength(300)
                .HasColumnName("correo");
            entity.Property(e => e.CreaFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creaFecha");
            entity.Property(e => e.CreaMaquina)
                .HasMaxLength(50)
                .HasDefaultValueSql("(host_name())")
                .HasColumnName("creaMaquina");
            entity.Property(e => e.CreaUsuario)
                .HasMaxLength(50)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("creaUsuario");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.FechaExpiracionToken).HasColumnType("datetime");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.ModificaFecha)
                .HasColumnType("datetime")
                .HasColumnName("modificaFecha");
            entity.Property(e => e.ModificaMaquina)
                .HasMaxLength(50)
                .HasColumnName("modificaMaquina");
            entity.Property(e => e.ModificaUsuario)
                .HasMaxLength(50)
                .HasColumnName("modificaUsuario");
            entity.Property(e => e.Nombres)
                .HasMaxLength(300)
                .HasColumnName("nombres");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(300)
                .HasColumnName("passwordHash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(300)
                .HasColumnName("passwordSalt");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Usuario_Empresa");
        });

        modelBuilder.Entity<UsuarioPerfil>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdPerfil });

            entity.ToTable("UsuarioPerfil");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");
            entity.Property(e => e.FechaInicia)
                .HasColumnType("date")
                .HasColumnName("fechaInicia");
            entity.Property(e => e.FechaTermina)
                .HasColumnType("date")
                .HasColumnName("fechaTermina");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.UsuarioPerfils)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioPerfil_Perfil");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioPerfils)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioPerfil_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

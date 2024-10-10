using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGPE.Comun.ContextAccesor;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Extensions;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.EstadoPedidoService;
using SGPE.WebApi.Services.PedidoService.Especificacion;

namespace SGPE.WebApi.Services.PedidoService
{
    public class PedidoService : IPedidoService
    {
        private readonly SGPEContext _db;
        public readonly IMapper _mapper;
        public readonly IEstadoPedidoService _estadoPedidoService;
        private readonly IContextAccessor _contextAccessor;

        public PedidoService(SGPEContext db, IMapper mapper, IEstadoPedidoService estadoPedidoService, IContextAccessor contextAccessor)
        {
            _db = db;
            _mapper = mapper;
            _estadoPedidoService = estadoPedidoService;
            _contextAccessor = contextAccessor;
        }

        public async Task<PagedResult<PedidoDto>> GetPedidos(GetEntityQuery query)
        {
            var especificacion = new PedidoEspecificacion(query.SearchText ?? "");

            return await _db.Pedidos
                .Include(p => p.IdEstadoPedidoNavigation)
                .Include(p => p.IdUsuarioSolicitaNavigation)
                .Where(especificacion.Criteria)
                .OrderBy($"{query.SortProperty} {query.SortDir}")
                .ProjectTo<PedidoDto>(_mapper.ConfigurationProvider)
                .GetPagedResultAsync(query.PageSize, query.CurrentPage);
        }

        public async Task<PagedResult<PedidoDto>> GetPedidosUsuario(GetEntityQuery query)
        {
            return await _db.Pedidos
                .Include(p => p.IdEstadoPedidoNavigation)
                .Where(p => p.IdUsuarioSolicita == Guid.Parse(_contextAccessor.UserId))
                .OrderByDescending(p => p.CreaFecha)
                .ProjectTo<PedidoDto>(_mapper.ConfigurationProvider)
                .GetPagedResultAsync(query.PageSize, query.CurrentPage);
        }

        public async Task<PedidoDto> GetPedido(Guid idPedido)
        {
            return await _db.Pedidos
                .Where(p => p.IdPedido == idPedido)
                .ProjectTo<PedidoDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Pedido), idPedido);
        }

        public async Task<string> CreatePedido(List<DetallePedidoEditDto> detalles)
        {
            var idsProductos = detalles.Select(p => p.IdProducto);
            var productos = _db.Productos.Where(p => idsProductos.Contains(p.IdProducto)).ToList();
            var detallesPedido = new List<DetallePedido>();

            foreach (var p in productos)
            {
                var productoDetalle = detalles.FirstOrDefault(d => d.IdProducto == p.IdProducto);
                if (productoDetalle != null)
                {
                    var detalle = new DetallePedido
                    {
                        IdDetallePedido = Guid.NewGuid(),
                        IdProducto = p.IdProducto,
                        Cantidad = productoDetalle.Cantidad,
                        SubTotal = p.Precio * productoDetalle.Cantidad,
                        CreaUsuario = _contextAccessor.UserName,
                        CreaMaquina = _contextAccessor.ClientIP,
                        CreaFecha = DateTime.Now
                    };
                    detallesPedido.Add(detalle);
                }
            }

            var pedido = new Pedido
            {
                IdPedido = Guid.NewGuid(),
                IdUsuarioSolicita = Guid.Parse(_contextAccessor.UserId),
                IdEstadoPedido = 1,
                ValorTotal = detallesPedido.Sum(p => p.SubTotal),
                CreaUsuario = _contextAccessor.UserName,
                CreaMaquina = _contextAccessor.ClientIP,
                CreaFecha = DateTime.Now,
                DetallePedidos = detallesPedido
            };

            _db.Pedidos.Add(pedido);
            await _db.SaveChangesAsync();
            return "¡Pedido creado!";
        }

        public async Task<Pedido> GetPedidoById(Guid idPedido)
        {
            return await _db.Pedidos.FindAsync(idPedido) ??
                throw new NotFoundException(nameof(Pedido), idPedido);
        }

        public async Task<PedidoEditDto> UpdatePedido(PedidoEditDto pedido)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ChangeStatusPedido(Guid idPedido, long idEstadoPedido)
        {
            var pedido = await GetPedidoById(idPedido);
            var estado = await _estadoPedidoService.GetEstadoPedidoById(idEstadoPedido);

            pedido.IdEstadoPedido = idEstadoPedido;
            _db.Pedidos.Update(pedido);
            await _db.SaveChangesAsync();

            return $"Pedido {pedido.IdPedido} actualizado a {estado.DescripcionEstadoPedido}";
        }

        public async Task<IEnumerable<ProductoPedidoDto>> GetProductosPedido(long idEmpresa)
        {
            return await _db.Productos
                .Where(p => p.IdEmpresa == idEmpresa && p.IdEstadoProducto == 1)
                .OrderBy(p => p.OrdenVisualizacion)
                .ProjectTo<ProductoPedidoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}

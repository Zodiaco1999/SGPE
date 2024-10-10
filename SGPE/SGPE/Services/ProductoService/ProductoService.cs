using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGPE.Comun.ContextAccesor;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Extensions;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.EstadoProductoService;
using SGPE.WebApi.Services.ProductoService.Especificacion;

namespace SGPE.WebApi.Services.ProductoService;

public class ProductoService : IProductoService
{
    private readonly SGPEContext _db;
    private readonly IMapper _mapper;
    private readonly IEstadoProductoService _estadoProductoService;
    private readonly IContextAccessor _contextAccessor;

    public ProductoService(SGPEContext db, IMapper mapper, IEstadoProductoService estadoProductoService, IContextAccessor contextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _estadoProductoService = estadoProductoService;
        _contextAccessor = contextAccessor;
    }

    public async Task<PagedResult<ProductoDto>> GetProductos(GetEntityQuery query)
    {
        var especificacion = new ProductoEspecificacion(query.SearchText ?? "");

        return await _db.Productos
            .AsNoTracking()
            .Where(especificacion.Criteria)
            .Include(p => p.IdCategoriaProductoNavigation)
            .Include(p => p.IdEmpresaNavigation)
            .Include(p => p.IdEstadoProductoNavigation)
            .OrderBy($"{query.SortProperty} {query.SortDir}")
            .ProjectTo<ProductoDto>(_mapper.ConfigurationProvider)
            .GetPagedResultAsync(query.PageSize, query.CurrentPage);
    }

    public async Task<string> CreateProducto(ProductoEditDto productoDto)
    {
        if (await ExistsProductoByCodigoErp(productoDto.CodigoErp))
            throw new ValidationException("Codigo Erp ya registrado!");

        var newProducto = _mapper.Map<Producto>(productoDto);
        newProducto.CreaMaquina = _contextAccessor.ClientIP;
        newProducto.CreaUsuario = _contextAccessor.UserName;
        newProducto.CreaFecha = DateTime.Now;

        _db.Productos.Add(newProducto);
        await _db.SaveChangesAsync();

        return "¡Producto creado!";
    }

    public async Task<string> UpdateProducto(ProductoEditDto productoDto)
    {
        var producto = await GetProductoById(productoDto.IdProducto);

        producto.IdEmpresa = productoDto.IdEmpresa;
        producto.IdCategoriaProducto = productoDto.IdCategoriaProducto;
        producto.CodigoErp = productoDto.CodigoErp;
        producto.DescripcionProducto = productoDto.DescripcionProducto;
        producto.Precio = productoDto.Precio;
        producto.ImagenBase64 = productoDto.ImagenBase64;
        producto.OrdenVisualizacion = productoDto.OrdenVisualizacion;
        producto.ModificaMaquina = _contextAccessor.ClientIP;
        producto.ModificaUsuario = _contextAccessor.UserName ?? "N/A";
        producto.ModificaFecha = DateTime.Now;

        await _db.SaveChangesAsync();

        return "¡Producto actualizado!";
    }

    public async Task<string> ChangeStatusProducto(long idProducto, long idEstadoProducto)
    {
        var producto = await GetProductoById(idProducto);
        var estado = await _estadoProductoService.GetEstadoProductoById(idEstadoProducto);

        producto.IdEstadoProducto = idEstadoProducto;

        await _db.SaveChangesAsync();

        return $"El producto con ID: {producto.CodigoErp} quedo en estado: {estado.DescripcionEstadoProducto}";
    }

    public async Task<ProductoEditDto> GetProducto(long idProducto)
    {
        var producto = await GetProductoById(idProducto);
        var productoDto = _mapper.Map<ProductoEditDto>(producto);

        return productoDto;
    }

    public async Task<bool> ExistsProductoByCodigoErp(int codigoErp)
    {
        return await _db.Productos.AnyAsync(p => p.CodigoErp == codigoErp);
    }

    public async Task<Producto> GetProductoById(long idProducto)
    {
        var producto = await _db.Productos.FindAsync(idProducto)
            ?? throw new NotFoundException(nameof(Producto), idProducto);

        return producto;
    }
}

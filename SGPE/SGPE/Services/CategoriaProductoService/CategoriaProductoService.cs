using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Extensions;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.CategoriaProductoService.Especificacion;

namespace SGPE.WebApi.Services.CategoriaProductoService
{
    public class CategoriaProductoService : ICategoriaProductoService
    {
        private readonly SGPEContext _db;
        private readonly IMapper _mapper;

        public CategoriaProductoService(SGPEContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PagedResult<CategoriaProductoDto>> GetCategorias(GetEntityQuery query)
        {
            var especificacion = new CategoriaProductoEspecificacion(query.SearchText ?? "");

            return await _db.CategoriaProductos
                .AsNoTracking()
                .Where(especificacion.Criteria)
                .OrderBy($"{query.SortProperty} {query.SortDir}")
                .Include(p => p.IdEmpresaNavigation)
                .ProjectTo<CategoriaProductoDto>(_mapper.ConfigurationProvider)
                .GetPagedResultAsync(query.PageSize, query.CurrentPage);
        }

        public async Task<IEnumerable<CategoriaProductoDto>> GetAllCategorias()
        {
            return await _db.CategoriaProductos
                .Include(c => c.IdEmpresaNavigation)
                .ProjectTo<CategoriaProductoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CategoriaProductoDto> GetCategoria(long idCategoria)
        {
            var categoria = await GetCategoriaById(idCategoria);
            return _mapper.Map<CategoriaProductoDto>(categoria);
        }

        public async Task<IEnumerable<CategoriaProductoDto>> GetCategoriasByIdEmpresa(long idEmpresa)
        {
            return await _db.CategoriaProductos
                .Where(c => c.IdEmpresa == idEmpresa)
                .ProjectTo<CategoriaProductoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<string> CreateCategoria(CategoriaProductoDto categoriaProductoDto)
        {
            if (await ExisteCodigoCategoria(categoriaProductoDto.CodigoCategoriaProducto))
                throw new ValidationException("El codigo de categoria ya esta registrado");

            var newCategoria = _mapper.Map<CategoriaProducto>(categoriaProductoDto);

            _db.CategoriaProductos.Add(newCategoria);
            await _db.SaveChangesAsync();

            return $"¡Categoria {newCategoria.NombreCategoriaProducto} creada correctamente!";
        }

        public async Task<string> UpdateCategoria(CategoriaProductoDto categoriaProductoDto)
        {
            var categoria = await GetCategoriaById(categoriaProductoDto.IdCategoriaProducto);

            if (categoria.CodigoCategoriaProducto != categoriaProductoDto.CodigoCategoriaProducto && await ExisteCodigoCategoria(categoriaProductoDto.CodigoCategoriaProducto))
                throw new ValidationException("El codigo de categoria ya esta registrado");

            categoria.IdEmpresa = categoriaProductoDto.IdEmpresa;
            categoria.CodigoCategoriaProducto = categoriaProductoDto.CodigoCategoriaProducto;
            categoria.NombreCategoriaProducto = categoriaProductoDto.NombreCategoriaProducto;

            await _db.SaveChangesAsync();

            return $"¡Categoria {categoria.NombreCategoriaProducto} actualizada correctamente!";
        }

        public async Task<string> ChangeStatusCategoria(long idCategoria)
        {
            var categoria = await GetCategoriaById(idCategoria);
            categoria.Activo = !categoria.Activo;
            await _db.SaveChangesAsync();

            return $"Categoria {categoria.NombreCategoriaProducto} {(categoria.Activo ? "activada" : "inactivada")} correctamente";
        }

        public async Task<CategoriaProducto> GetCategoriaById(long idCategoria)
        {
            return await _db.CategoriaProductos.FindAsync(idCategoria)
                ?? throw new NotFoundException(nameof(CategoriaProducto), idCategoria);
        }

        public async Task<bool> ExisteCodigoCategoria(string codigoCategoria)
        {
            return await _db.CategoriaProductos.AnyAsync(c => c.CodigoCategoriaProducto == codigoCategoria);
        }
    }
}

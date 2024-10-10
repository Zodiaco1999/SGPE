using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace SGPE.WebApi.Services.DetallePedidoService
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly SGPEContext _db;
        private readonly IMapper _mapper;

        public DetallePedidoService(SGPEContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetallePedidoDto>> GetDetallePedido(Guid idPedido)
        {
            return await _db.DetallePedidos
                .Where(d => d.IdPedido == idPedido)
                .ProjectTo<DetallePedidoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}

import { DetallePedido } from './detalle-pedido';
export interface PedidoEdit {
    idPedido: number
    idUsuarioSolicita: number
    idEstadoPedido: number
    valorTotal: number
    detallePedido: DetallePedido[]
}

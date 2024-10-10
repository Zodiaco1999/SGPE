export interface Pedido {
  idPedido: number;
  idUsuarioSolicita: number;
  valorTotal: number;
  fechaSolicita: Date;
  estadoPedido: string;
  usuario: string;
  cedulaUsuario: string;
  cantidadProductos: number;
}

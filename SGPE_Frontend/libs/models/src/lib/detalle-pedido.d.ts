export interface DetallePedido {
  idDetallePedido: number;
  idPedido: number;
  idProducto: number;
  codigoErp: number;
  descripcionProducto: string;
  cantidad: number;
  precio: number;
  subTotal: number;
}

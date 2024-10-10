export interface ProductoPedido {
  idProducto: number;
  idCategoriaProducto: number;
  idEmpresa: number;
  idEstadoProducto: number;
  codigoErp: number;
  descripcionProducto: string;
  precio: number;
  imagenBase64?: string;
  cantidad: number;
}

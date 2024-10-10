export interface Producto {
    idProducto: number
    idCategoriaProducto: number
    idEmpresa: number
    idEstadoProducto: number
    idUsuarioModifica: number
    codigoErp: number
    descripcionProducto?: string
    precio: number
    imagenBase64?: string
    ordenVisualizacion: number
    fechaCreacion: Date
    fechaModificacion?: Date
    categoriaProducto?: string
    nombreEmpresa?: number
    estadoProducto?: number
}

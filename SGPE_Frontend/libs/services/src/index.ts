export * from './lib/services.module';
export { ProductoService } from './lib/producto.service'
export { CategoriaProductoService } from './lib/categoria-producto.service'
export { EmpresaService } from './lib/empresa.service'
export { UsuarioService } from './lib/usuario.service'
export { RolService } from './lib/rol.service'
export { AuthService } from './lib/auth.service'
export { ModuloService } from './lib/modulo.service'
export { MenuService } from './lib/menu.service'
export { PerfilService } from './lib/perfil.service'
export { PedidoService } from './lib/pedido.service'
export { PedidoUsuarioService } from './lib/pedido-usuario.service'
export { DetallePedidoService } from './lib/detalle-pedido.service'

export const environment = {
  production: false,
  API_URL: 'https://localhost:7199/api'
}

// export interface ServiceResponse<T> {
//   data: T,
//   message: string,
//   success: boolean
// }

export class CustomErrorResponse {
  id = 0;
  detail = '';
  isWarning = false;
  statusCode = 0;
}


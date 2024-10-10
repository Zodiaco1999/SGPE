import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
export { Producto } from './producto'
export { ProductoEdit } from './producto-edit'
export { Empresa } from './empresa'
export { CategoriaProducto } from './categoria-producto'
export { Usuario } from './usuario'
export { UsuarioEdit } from './usuario-edit'
export { Rol } from './rol'
export { EstadoProducto } from './estado-producto'
export { UsuarioLogin } from './usuario-login'
export { LoginResult } from './login-result'
export { Modulo } from './modulo'
export { Menu } from './menu'
export { Perfil } from './perfil'
export { PerfilMenu } from './perfil-menu'
export { UsuarioPerfil } from './usuario-perfil'
export { ModuloUsuario } from './modulo-usuario'
export { MenuUsuario } from './menu-usuario'
export { Pedido } from './pedido'
export { ProductoPedido } from './producto-pedido'
export { DetallePedido } from './detalle-pedido'
export { CambioContrasena } from './cambio-contrasena'

@NgModule({
  imports: [CommonModule],
})
export class ModelsModule {}

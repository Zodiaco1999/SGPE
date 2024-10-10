import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { authRoutes } from './lib.routes';
import { AuthLayoutComponent } from './components/auth-layout/auth-layout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IniciarSesionComponent } from './pages/iniciar-sesion/iniciar-sesion.component';
import { RecuperarContrasenaComponent } from './pages/recuperar-contrasena/recuperar-contrasena.component';
export { TokenInterceptor } from './interceptors/token.interceptor'
export { authGuard } from './guards/auth.guard'
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(authRoutes),
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    AuthLayoutComponent,
    IniciarSesionComponent,
    RecuperarContrasenaComponent,
  ],
  exports: [AuthLayoutComponent]
})
export class AuthModule {}

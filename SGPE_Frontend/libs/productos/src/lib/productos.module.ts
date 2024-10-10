import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { productosRoutes } from './lib.routes';
import { ProductosComponent } from './pages/productos/productos.component';
import { ProductoEditComponent } from './pages/producto-edit/producto-edit.component';
import { GeneralModule } from '@sgpe-ws/general';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    CommonModule,
    GeneralModule,
    RouterModule.forChild(productosRoutes)],
  declarations: [ProductosComponent, ProductoEditComponent],
})
export class ProductosModule {}

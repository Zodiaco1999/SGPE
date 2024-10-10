import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { categoriaproductosRoutes } from './lib.routes';
import { CategoriaProductosComponent } from './pages/categoria-productos/categoria-productos.component';
import { CategoriaEditComponent } from './pages/categoria-edit/categoria-edit.component';
import { GeneralModule } from '@sgpe-ws/general';

@NgModule({
  imports: [
    CommonModule,
    GeneralModule,
    RouterModule.forChild(categoriaproductosRoutes)],
  declarations: [CategoriaProductosComponent, CategoriaEditComponent],
})
export class CategoriaProductosModule {}

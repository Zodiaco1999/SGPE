import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GeneralModule } from '@sgpe-ws/general';
import { modulosRoutes } from './lib.routes';
import { ModulosComponent } from './pages/modulos/modulos.component';
import { ModuloEditComponent } from './pages/modulo-edit/modulo-edit.component';
import { ModuloViewComponent } from './pages/modulo-view/modulo-view.component';
import { MenuEditComponent } from './components/menu-edit/menu-edit.component';

@NgModule({
  imports: [
    CommonModule,
    GeneralModule,
    RouterModule.forChild(modulosRoutes),
  ],
  declarations: [
    ModulosComponent,
    ModuloEditComponent,
    ModuloViewComponent,
    MenuEditComponent,
  ],
})
export class ModulosModule {}

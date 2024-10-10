import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { empresasRoutes } from './lib.routes';
import { EmpresasComponent } from './pages/empresas/empresas.component';
import { EmpresaEditComponent } from './pages/empresa-edit/empresa-edit.component';
import { GeneralModule } from '@sgpe-ws/general';

@NgModule({
  imports: [
    CommonModule,
    GeneralModule,
    RouterModule.forChild(empresasRoutes)],
  declarations: [EmpresasComponent, EmpresaEditComponent],
})
export class EmpresasModule {}

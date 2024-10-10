import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { perfilesRoutes } from './lib.routes';
import { GeneralModule } from '@sgpe-ws/general';
import { PerfilesComponent } from './pages/perfiles/perfiles.component';
import { PerfilEditComponent } from './pages/perfil-edit/perfil-edit.component';
import { PerfilViewComponent } from './pages/perfil-view/perfil-view.component';

@NgModule({
  imports: [CommonModule, GeneralModule, RouterModule.forChild(perfilesRoutes)],
  declarations: [PerfilesComponent, PerfilEditComponent, PerfilViewComponent],
})
export class PerfilesModule {}

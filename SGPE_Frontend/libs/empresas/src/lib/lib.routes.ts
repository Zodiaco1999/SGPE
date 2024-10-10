import { Route } from '@angular/router';
import { EmpresasComponent } from './pages/empresas/empresas.component';
import { EmpresaEditComponent } from './pages/empresa-edit/empresa-edit.component';

export const empresasRoutes: Route[] = [
    { path:'', pathMatch:'full', component: EmpresasComponent, title: 'Empresas' },
    { path:'crear', pathMatch:'full', component: EmpresaEditComponent, title: 'Crear Empresa' },
    { path:'editar/:id', pathMatch:'full', component: EmpresaEditComponent, title: 'Editar Empresa' }
];

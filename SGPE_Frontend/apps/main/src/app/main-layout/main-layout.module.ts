import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routes } from './main-layout.routes';
import { RouterModule } from '@angular/router';
import { LayoutUiModule } from '@sgpe-ws/layout-ui';
import { MainLayoutComponent } from './main-layout.component';
import { IndexComponent } from '../pages/index/index.component';

@NgModule({
  declarations: [MainLayoutComponent, IndexComponent],
  imports: [
    CommonModule,
    LayoutUiModule,
    RouterModule,
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
  bootstrap: [MainLayoutComponent],
})
export class MainLayoutModule {}

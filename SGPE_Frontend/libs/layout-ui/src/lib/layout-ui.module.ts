import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutComponent } from './layout/layout.component';
import { FooterComponent } from './footer/footer.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RightSidebarComponent } from './right-sidebar/right-sidebar.component';
import { TopMenuComponent } from './top-menu/top-menu.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgbModule
  ],
  declarations: [
    LayoutComponent,
    FooterComponent,
    NavMenuComponent,
    RightSidebarComponent,
    TopMenuComponent,
  ],
  exports: [LayoutComponent]
})
export class LayoutUiModule {}

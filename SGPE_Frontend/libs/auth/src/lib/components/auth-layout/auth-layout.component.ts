import { Component } from '@angular/core';

@Component({
  selector: 'sgpe-ws-auth-layout',
  template: `
    <div class="auth-page">
      <div class="container-fluid p-0">
        <div class="row g-0">
          <div class="col-xxl-3 col-lg-4 col-md-5">
            <div class="auth-full-page-content d-flex p-sm-5 p-4">
              <div class="w-100">
                <div class="d-flex flex-column h-100">
                  <div class="mb-4 mb-md-5 text-center">
                    <a class="d-block auth-logo poniter">
                      <img src="assets/images/logo-sm.svg" alt="" height="28" />
                      <span class="logo-txt">Pedidos empleados</span>
                    </a>
                  </div>
                  <div class="auth-content my-auto">
                    <!-- Content -->
                    <router-outlet />
                  </div>
                  <div class="mt-4 mt-md-5 text-center">
                    <p class="mb-0">© {{ date.toLocaleDateString() }}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-xxl-9 col-lg-8 col-md-7">
            <div class="auth-bg pt-md-5 p-4 d-flex">
              <div class="bg-overlay bg-primary"></div>
              <ul class="bg-bubbles">
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styleUrls: ['./auth-layout.component.scss'],
})
export class AuthLayoutComponent {
  date = new Date();
}

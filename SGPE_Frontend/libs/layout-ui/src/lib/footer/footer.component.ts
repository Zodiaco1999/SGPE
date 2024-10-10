import { Component } from '@angular/core';

@Component({
  selector: 'sgpe-ws-footer',
  template: `
    <footer class="footer">
      <div class="container-fluid">
        <div class="row">
          <div class="col-sm-6">
            {{ date }} © Agora CSC.
          </div>
          <div class="col-sm-6">
            <div class="text-sm-end d-none d-sm-block">
              Develop by
              <a href="https://github.com/Zodiaco1999" class="text-decoration-underline">Agora TIC</a>
            </div>
          </div>
        </div>
      </div>
    </footer>
  `
})
export class FooterComponent {
  date = new Date().getFullYear();
}

import { AfterViewInit, Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '@sgpe-ws/services';
import { UsuarioLogin } from '@sgpe-ws/models';
import { SweetAlertService } from '@sgpe-ws/general';
import { Router } from '@angular/router';

@Component({
  selector: 'sgpe-ws-iniciar-sesion',
  templateUrl: './iniciar-sesion.component.html',
  styleUrls: ['./iniciar-sesion.component.scss'],
})
export class IniciarSesionComponent implements AfterViewInit {
  loginForm: FormGroup = new FormGroup({
    cedulaUsuario: new FormControl('', Validators.required),
    contrasena: new FormControl('', Validators.required),
  });

  constructor(
    private authService: AuthService,
    private router: Router,
    private alert: SweetAlertService) {}

  ngAfterViewInit() {
    document.getElementById('pass')?.remove();

    const script = document.createElement('script');
    script.id = 'pass';
    script.src = '../assets/js/pages/pass-addon.init.js';
    document.body.appendChild(script);
  }

  login() {
    if (this.loginForm.valid) {
      const usuarioLogin: UsuarioLogin = this.loginForm.value;
      this.authService.login(usuarioLogin).subscribe({
        next: (success) => {
          if (success) {
            this.router.navigate(['/']);
          }
        },
        error: (err) => {
          console.log(err);
          this.alert.msgNormalError('Autenticación erronea', err.message);
        },
      });
    }
  }
}

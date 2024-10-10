import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomFormBuilderService, SweetAlertService } from '@sgpe-ws/general';
import { AuthService } from '@sgpe-ws/services';

@Component({
  selector: 'sgpe-ws-cambiar-correo',
  templateUrl: './cambiar-correo.component.html',
  styleUrls: ['./cambiar-correo.component.scss'],
})
export class CambiarCorreoComponent implements OnInit {
  haveEmail = false;
  action = '';
  email = '';

  constructor(
    private authService: AuthService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private router: Router,
    public formBuilder: CustomFormBuilderService
  ) {}

  ngOnInit() {
    this.builForm();

    const emailParam = this.route.snapshot.paramMap.get('email');
    const token = this.route.snapshot.paramMap.get('token');

    if (emailParam && token) {
      this.authService.changeEmail(emailParam, token).subscribe({
        next: (res) => {
          this.alert.msgNormal('success', '', res.message);
          if (!this.authService.getUserEmail()) {
            this.authService.setUserEmail(emailParam);
            this.router.navigateByUrl('/usuarios/cambiarcontrasena');
          }

          this.getEmailAndInitForm();
        },
        error: (e) => this.alert.msgNormalError('Error en el cambio de correo', e.message)
      });
    } else {
      this.getEmailAndInitForm();
    }
  }

  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'correo',
        name: 'Correo electrónico',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            isEmail: true,
          },
        ],
      },
    ];

    this.formBuilder.initFormGroup();
  }

  sendEmailChangeEmail() {
    const correo: string = this.formBuilder.formGroup.value.correo;

    this.authService.sendEmailChangeEmail(correo).subscribe({
      next: (res) => this.alert.msgNormal('info', '', res.message),
      error: (e) =>
        this.alert.msgNormalError('Error en el cambio de correo', e.message),
    });
  }

  getEmailAndInitForm() {
    this.email = this.authService.getUserEmail();
    if (this.email) {
      this.formBuilder.formGroup.reset({ correo: this.email });
      this.action = 'Cambiar correo';
    } else {
      this.action = 'Establecer correo';
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomFormBuilderService, SweetAlertService } from '@sgpe-ws/general';
import { AuthService } from '@sgpe-ws/services';

@Component({
  selector: 'sgpe-ws-recuperar-contrasena',
  templateUrl: './recuperar-contrasena.component.html',
  styleUrls: ['./recuperar-contrasena.component.scss'],
})
export class RecuperarContrasenaComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private router: Router,
    public formBuilder: CustomFormBuilderService
  ) {}

  ngOnInit() {
    this.builForm();
  }

  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'cedula',
        name: 'Cedula',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6
          },
        ],
      },
    ];

    this.formBuilder.initFormGroup();
  }

  sendEmailResetPassword() {
    this.authService
      .sendEmailResetPassword(this.formBuilder.formGroup.value.cedula)
      .subscribe({
        next: (res) => {
          this.alert.msgNormal('success', '', res.message);
        },
        error: (e) => this.alert.msgNormalError('Error en el envio de correo', e.message),
      });
  }
}

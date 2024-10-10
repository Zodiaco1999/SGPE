import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomFormBuilderService, SweetAlertService, ServiceResponse } from '@sgpe-ws/general';
import { CambioContrasena } from '@sgpe-ws/models';
import { AuthService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-cambiar-contrasena',
  templateUrl: './cambiar-contrasena.component.html',
  styleUrls: ['./cambiar-contrasena.component.scss'],
})
export class CambiarContrasenaComponent implements OnInit {
  email = '';
  token = '';
  isReset = false;
  itemsForm: ItemForm[] = [];

  constructor(
    public formBuilder: CustomFormBuilderService,
    private authService: AuthService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  async ngOnInit() {
    this.email = this.route.snapshot.paramMap.get('email') ?? '';
    this.token = this.route.snapshot.paramMap.get('token') ?? '';

    if (this.email && this.token) {
      this.isReset = true;
      this.authService.verifyToken(this.email, this.token).subscribe({
        error: (e) => {
          this.alert.msgNormalError('Enlace de restablecer contraseña invalido', e.message);
          this.router.navigateByUrl('/auth/recuperarcontrasena');
        }
      });
    }
    else if (!this.authService.getUserEmail()) {
      await this.alert .msgComfirm('', `${this.authService.getUserName()} para poder cambiar la contraseña debe establecer un correo electrónico`, 'warning', false);
      this.router.navigateByUrl('/usuarios/cambiarcorreo');
    }

    this.builForm();
  }

  changePassword() {
    const contrasenaForm: CambioContrasena = this.formBuilder.formGroup.value;
    if (contrasenaForm.contrasenaNueva != contrasenaForm.contrasenaConfirmar) {
      this.alert.msgNormalError('', 'La contraseña nueva y la confirmación no coinciden');
    } else {
      if (this.isReset) {
        contrasenaForm.correo = this.email;
        this.handleCommand(this.authService.ResetPassword(contrasenaForm, this.token));
      }
      else
      this.handleCommand(this.authService.changePassword(contrasenaForm));
    }
  }

  handleCommand(command: Observable<ServiceResponse<object>>) {
    command.subscribe({
      next: (res) => {
        this.alert.msgSimpleSuccess(res.message, 'center', 10000);
        this.router.navigateByUrl('/auth');
      },
      error: (e) => {
        this.alert.msgNormalError('Error cambio de contraseña', e.message)
        if (this.isReset)
          this.router.navigateByUrl('/auth/recuperarcontrasena');
      }
    });
  }

  builForm() {
    this.itemsForm = [
      {
        label: 'Contraseña Actual',
        placeholder: 'Ingrese la contraseña actual',
        inputType: 'password',
        classButton: 'mdi mdi-eye-outline',
        controlName: 'contrasenaActual',
        visible: !this.isReset
      },
      {
        label: 'Contraseña Nueva',
        placeholder: 'Ingrese la contraseña nueva',
        inputType: 'password',
        classButton: 'mdi mdi-eye-outline',
        controlName: 'contrasenaNueva'
      },
      {
        label: 'Confirmar Contraseña',
        placeholder: 'Confirma la nueva contraseña',
        inputType: 'password',
        classButton: 'mdi mdi-eye-outline',
        controlName: 'contrasenaConfirmar'
      }
    ];

    this.formBuilder.formControls = [
      {
        id: 'contrasenaActual',
        name: 'contraseña actual',
        formState: '',
        validatorOrOpts: [
          {
            required: !this.isReset,
            minLength: 6,
          },
        ],
      },
      {
        id: 'contrasenaNueva',
        name: 'contraseña nueva',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6,
          },
        ],
      },
      {
        id: 'contrasenaConfirmar',
        name: 'confirmacion de contraseña',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6,
          },
        ],
      },
    ];

    this.formBuilder.initFormGroup();
  }

  changeInput(item: ItemForm) {
    const isPassword = item.inputType === 'password';
    item.inputType = isPassword ? 'text' : 'password';
    item.classButton = isPassword
      ? 'mdi mdi-eye-off-outline'
      : 'mdi mdi-eye-outline';
  }
}

type ItemForm = {
  label: string;
  placeholder: string;
  inputType: string;
  classButton: string;
  controlName: string;
  visible?: boolean;
}

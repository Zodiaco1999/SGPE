import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomFormBuilderService, SweetAlertService, ServiceResponse } from '@sgpe-ws/general';
import { Empresa } from '@sgpe-ws/models';
import { EmpresaService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-empresa-edit',
  templateUrl: './empresa-edit.component.html',
  styleUrls: ['./empresa-edit.component.scss'],
})
export class EmpresaEditComponent implements OnInit {
  idEmpresa = 0;

  constructor(
    private empresaService: EmpresaService,
    private router: Router,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    public formBuilder: CustomFormBuilderService
  ) {}

  ngOnInit() {
    this.builForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.idEmpresa = +id;
      this.empresaService.getEmpresa(this.idEmpresa)
        .subscribe({
          next: (respose) => {
            this.formBuilder.formGroup.reset(respose.data)
          },
          error: (e) => this.alert.msgNormalError('Obtener empresa', e.message)
        });
    }
  }

  saveEmpresa() {
    const empresa: Empresa = this.formBuilder.formGroup.value;
    if (!this.idEmpresa) {
      this.handleCommand(this.empresaService.createEmpresa(empresa));
    } else {
      empresa.idEmpresa = this.idEmpresa;
      this.handleCommand(this.empresaService.updateEmpresa(empresa));
    }
  }

  handleCommand(command: Observable<ServiceResponse<object>>) {
    command.subscribe({
      next: (res) => {
        this.router.navigateByUrl('/empresas');
        this.alert.msgSimpleSuccess(res.message);
      },
      error: (e) => this.alert.msgNormalError(`No se pudo ${this.idEmpresa ? 'editar' : 'crear'} la empresa`, e.message)
    })
  }

  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'nit',
        name: 'Nit',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 7,
            pattern: '(^[0-9]+$)',
            validationKey: 'pattern',
            validationMessage: 'Solo debe contar con numeros'
          }
        ]
      },
      {
        id: 'nombreEmpresa',
        name: 'Nombre Empresa',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6
          }
        ]
      }
    ];

    this.formBuilder.initFormGroup();
  }

}

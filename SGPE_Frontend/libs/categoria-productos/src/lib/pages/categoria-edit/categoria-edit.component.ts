import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomFormBuilderService, SweetAlertService, ServiceResponse } from '@sgpe-ws/general';
import { CategoriaProducto, Empresa } from '@sgpe-ws/models';
import { CategoriaProductoService, EmpresaService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-categoria-edit',
  templateUrl: './categoria-edit.component.html',
  styleUrls: ['./categoria-edit.component.scss'],
})
export class CategoriaEditComponent implements OnInit {
  idCategoriaProducto = 0;
  empresas: Empresa[] = [];

  constructor (
    private categoriaService: CategoriaProductoService,
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
      this.idCategoriaProducto = +id;
      this.categoriaService.getCategoria(this.idCategoriaProducto).subscribe({
        next: (res) => this.formBuilder.formGroup.reset(res.data),
        error: (err) => this.alert.msgNormalError('Error consultado la categoria', err.message)
      })
    }
    this.empresaService.getAllEmpresas().subscribe({
      next: (res) => this.empresas = res.data,
      error: (err) => this.alert.msgNormalError('Error consulta de empresas', err.message)
    })
  }

  saveCategoria() {
    const categoria: CategoriaProducto = this.formBuilder.formGroup.value;

    if (this.idCategoriaProducto > 0) {
      categoria.idCategoriaProducto = this.idCategoriaProducto;
      this.handleCommand(this.categoriaService.updateCategoria(categoria));
    } else {
      this.handleCommand(this.categoriaService.createCategoria(categoria));
    }
  }

  handleCommand(command: Observable<ServiceResponse<object>>) {
    command.subscribe({
      next: (res) => {
        this.alert.msgSimpleSuccess(res.message);
        this.router.navigateByUrl('/categoria-productos');
      },
      error: (e) => this.alert.msgNormalError('Error en la operación de categoria', e.message)
    })
  }

  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'idEmpresa',
        name: 'Empresa',
        formState: 0,
        validatorOrOpts: [
          {
            min: 1,
            validationKey: 'min',
            validationMessage: 'La Empresa es requerida'
          }
        ]
      },
      {
        id: 'codigoCategoriaProducto',
        name: 'Codigo Categoria',
        formState: '',
        validatorOrOpts: [
          {
            required: true
          }
        ]
      },
      {
        id: 'nombreCategoriaProducto',
        name: 'Nombre Categoria',
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

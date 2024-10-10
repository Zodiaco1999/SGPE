import { Component, OnInit } from '@angular/core';
import { CategoriaProducto, Empresa, ProductoEdit } from '@sgpe-ws/models';
import { CategoriaProductoService, EmpresaService, ProductoService } from '@sgpe-ws/services';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomFormBuilderService, SweetAlertService } from '@sgpe-ws/general'

@Component({
  selector: 'sgpe-ws-producto-edit',
  templateUrl: './producto-edit.component.html',
  styleUrls: ['./producto-edit.component.scss'],
})

export class ProductoEditComponent implements OnInit {
  empresas: Empresa[] = [];
  categorias: CategoriaProducto[] = [];
  idProducto = 0;
  isEdit = false;
  image: string | undefined = '';

  constructor (
    private productoService: ProductoService,
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
      this.isEdit = true;
      this.idProducto = +id;
      this.productoService.getProducto(this.idProducto)
        .subscribe({
          next: (respose) => {
            this.formBuilder.formGroup.reset(respose.data)
            this.image = respose.data.imagenBase64;
          },
          error: (e) => this.alert.msgNormalError('Obtener producto', e.message)
        });
    }

    this.empresaService.getAllEmpresas()
      .subscribe((response) => { if (response.success) this.empresas = response.data });

    this.formBuilder.formGroup.get('idEmpresa')?.valueChanges.subscribe(v => {
      this.categoriaService.getCategoriasByIdEmpresa(v).subscribe(res => this.categorias = res.data)
    });
  }

  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'idCategoriaProducto',
        name: 'Categora Producto',
        formState: 0,
        validatorOrOpts: [
          {
            min: 1,
            validationKey: 'min',
            validationMessage: 'La Categoria es requerida'
          }
        ]
      },
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
        id: 'codigoErp',
        name: 'Codigo Erp',
        formState: 0,
        validatorOrOpts: [
          {
            required: true
          }
        ]
      },
      {
        id: 'descripcionProducto',
        name: 'Descripcion',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6
          }
        ]
      },
      {
        id: 'precio',
        name: 'Precio',
        formState: 0,
        validatorOrOpts: [
          {
            required: true,
            min: 1,
            max: 1000000
          }
        ]
      },
      {
        id: 'imagenBase64',
        name: 'Imagen',
        formState: ''
      },
      {
        id: 'ordenVisualizacion',
        name: 'Orden de visualizacion',
        formState: 0,
        validatorOrOpts: [
          {
            required: true,
            min: 1
          }
        ]
      }
    ];

    this.formBuilder.initFormGroup();
  }

  saveProducto() {
    if (this.formBuilder.formGroup.valid) {
      const producto: ProductoEdit = this.formBuilder.formGroup.value;
      if (!this.isEdit) {
        this.productoService.createProducto(producto)
          .subscribe({
            next: () => {
              this.router.navigateByUrl('/productos');
              this.alert.msgSimpleSuccess('Se creo el producto')
            },
            error: (e) => this.alert.msgNormalError('Crear producto', e.message)
          });
      } else {
        producto.idProducto = this.idProducto;
        this.productoService.updateProducto(producto)
          .subscribe({
            next: () => {
              this.router.navigateByUrl('/productos');
              this.alert.msgSimpleSuccess('Se actualizo el producto')
            },
            error: (e) => this.alert.msgNormalError('Editar producto', e.message)
          });
      }
    }
  }

  fileProgress(event: Event) {
    const target = event.target as HTMLInputElement;
    const fileInput = (target.files as FileList)[0];
    const reader = new FileReader();

    reader.onloadend = () => {
      const arr = fileInput.name.split('.');
      switch (arr[arr.length - 1]) {
        case 'png':
        case 'jpg':
        case 'jpeg': {
          this.image = reader.result?.toString();
          this.formBuilder.formGroup.get('imagenBase64')?.setValue(this.image);
          break;
        }
        default: {
          this.alert.msgNormalError(
            'Error al cargar imagen',
            'La extensión debe ser png, jpg o jpeg'
          );
          break;
        }
      }
    };
    if (fileInput) {
      reader.readAsDataURL(fileInput);
    }
  }
}

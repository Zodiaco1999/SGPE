import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SweetAlertService } from '@sgpe-ws/general';
import { CategoriaProducto, Empresa, ProductoPedido } from '@sgpe-ws/models';
import { CategoriaProductoService, EmpresaService, PedidoService } from '@sgpe-ws/services';
import { PedidoPreviewComponent } from '../../components/pedido-preview/pedido-preview.component';

@Component({
  selector: 'sgpe-ws-pedido-edit',
  templateUrl: './pedido-edit.component.html',
  styleUrls: ['./pedido-edit.component.scss'],
})
export class PedidoEditComponent implements OnInit {
  productos: ProductoPedido[] = [];
  productosTemp: ProductoPedido[] = [];
  empresas: Empresa[] = []
  categorias: CategoriaProducto[] = [];
  categoriasTemp: CategoriaProducto[] = [];
  idEmpresa = new FormControl(0);
  idCategoria  = new FormControl(0);
  searchEnable = false;
  cantidad = 0;
  valor = 0;

  constructor(
    private pedidoService: PedidoService,
    private categoriaService: CategoriaProductoService,
    private empresaService: EmpresaService,
    private alert: SweetAlertService,
    private router: Router,
    private matDialog: MatDialog
    ) {}

  ngOnInit() {
    this.empresaService.getEmpresasWithCategories().subscribe(res => this.empresas = res.data)
    this.categoriaService.getAllCategorias().subscribe(res => this.categorias = res.data);
    this.idEmpresa.valueChanges.subscribe(v => {
      this.idCategoria.setValue(0);
      if (v && v > 0) {
        this.categoriasTemp = this.categorias.filter(c => c.idEmpresa == v);
        this.searchEnable = true;
      }
      else {
        this.categoriasTemp = [];
        this.searchEnable = false;
      }
    })

  }

  async searchProducts() {
    if (!this.productos.length) {
      this.getProducts();
    }
    else if (this.productos.length && this.productos[0].idEmpresa == this.idEmpresa.value) {
      this.getProductsTemp();
    }
    else {
      const changeEmpresa = await this.alert.msgComfirm('Cambio de empresa', 'Si cambia de empresa perdera el pedido actual', 'warning');
      if (changeEmpresa) {
        this.getProducts();
        this.cantidad = 0;
        this.valor = 0;
      }
    }
  }

  getProducts() {
    this.pedidoService.getProductosPedido(this.idEmpresa.value ?? 0).subscribe({
      next: (res) => {
        this.productos = res.data
        this.getProductsTemp();
      },
      error: (e) => this.alert.msgNormalError('Error en consulta', e.message)
    });
  }

  getProductsTemp() {
    const idCategoria = this.idCategoria.value ?? 0;
    this.productosTemp = idCategoria > 0 ? this.productos.filter(p => p.idCategoriaProducto == idCategoria) : this.productos;
  }

  changeQuatity() {
    this.cantidad = this.productos.filter(p => p.cantidad > 0).length;
  }

  changeValue() {
    const productosAgregados = this.productos.filter(p => p.cantidad > 0);
    this.valor = 0;
    if (productosAgregados.length) {
      this.valor = productosAgregados.reduce((suma, producto) => suma += producto.precio * producto.cantidad, 0)
    }
  }

  async refresh() {
    if (this.cantidad > 0 && await this.alert.msgComfirm('Reiniciar', '¿Seguro que desea reiniciar el pedido?')) {
      this.getProducts();
      this.cantidad = 0;
      this.valor = 0;
    }
  }

  accept() {
    const productosAgregados = this.productos.filter(p => p.cantidad > 0);
    const modalRef = this.matDialog.open(PedidoPreviewComponent,{
      disableClose: true,
      data: productosAgregados
    });

    modalRef
    .afterClosed()
    .subscribe({
      next: (response) => {
        if (response !== '' && response) {
          this.pedidoService.createPedido(productosAgregados).subscribe({
            next: (res) => {
              this.alert.msgSimpleSuccess(res.message);
              this.router.navigateByUrl('/pedidos/usuario');
            },
            error: (e) => this.alert.msgNormalError("No se pudo crear el pedido", e.message)
          });
        }
      },
      error: (err) => console.log(err)
    });
  }

  async cancel() {
    if (await this.alert.msgComfirm('Cancelar', '¿Seguro que desea descartar este pedido?'))
      this.router.navigateByUrl('/pedidos/usuario');
  }
}

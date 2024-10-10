import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DetallePedido } from '@sgpe-ws/models';

@Component({
  selector: 'sgpe-ws-pedido-preview',
  templateUrl: './pedido-preview.component.html',
  styleUrls: ['./pedido-preview.component.scss'],
})
export class PedidoPreviewComponent implements OnInit {
  valorAprox = 0;

  constructor (
    @Inject(MAT_DIALOG_DATA) public detalles: DetallePedido[],
    private dialogRef: MatDialogRef<PedidoPreviewComponent>
    ) {
  }

  ngOnInit() {
    this.detalles.forEach(p => p.subTotal = p.cantidad * p.precio);
    this.valorAprox = this.detalles.reduce((suma, producto) => suma += producto.subTotal, 0)
  }

  savePedido() {
    this.dialogRef.close(true);
  }
}

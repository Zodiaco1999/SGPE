import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '..'
import { DetallePedido } from '@sgpe-ws/models';
import { ServiceResponse } from '@sgpe-ws/general';
@Injectable({
  providedIn: 'root'
})
export class DetallePedidoService {
  private apiUrl = `${environment.API_URL}/detallepedido`

  constructor(private http: HttpClient) {
  }

  getDetallePedido(idPedido: string) {
    return this.http.get<ServiceResponse<DetallePedido[]>>(`${this.apiUrl}/getdetallepedido/${idPedido}`);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '..'
import { Pedido } from '@sgpe-ws/models';
import { DataTableServiceBase, SearchResult, ServiceResponse } from '@sgpe-ws/general';

@Injectable({
  providedIn: 'root'
})
export class PedidoUsuarioService extends DataTableServiceBase<Pedido> {
  private apiUrl = `${environment.API_URL}/pedido`

  constructor(private http: HttpClient) {
    super();
  }

  public getSearch() {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } =
    this._searchState;

    let params = new HttpParams()
      .set('currentPage', page)
      .set('pageSize', pageSize)

    if (searchTerm) {
      params = params.append('searchText', searchTerm);
    }

    if (sortColumn !== undefined) {
      params
        .set('sortProperty', sortColumn)
        .set('sortDir', sortDirection);
    }

    return this.http.get<ServiceResponse<SearchResult<Pedido>>>(`${this.apiUrl}/getpedidosusuario`, { params });
  }
}

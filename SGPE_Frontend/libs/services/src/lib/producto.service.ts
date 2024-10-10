import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '..'
import { Producto, ProductoEdit } from '@sgpe-ws/models';
import { DataTableServiceBase, SearchResult, ServiceResponse } from '@sgpe-ws/general';

@Injectable({
  providedIn: 'root'
})
export class ProductoService extends DataTableServiceBase<Producto> {
  private apiUrl = `${environment.API_URL}/producto`

  constructor(
    private http: HttpClient) {
    super()
  }

  public getSearch() {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._searchState;

    let params = new HttpParams()
      .set('currentPage', page)
      .set('pageSize', pageSize)

    if (searchTerm) {
      params = params.append('searchText', searchTerm);
    }

    if (sortColumn !== undefined) {
      params = params
        .append('sortProperty', sortColumn)
        .append('sortDir', sortDirection);
    }

    return this.http.get<ServiceResponse<SearchResult<Producto>>>(`${this.apiUrl}/getproductos`, { params })
  }

  createProducto(producto: ProductoEdit) {
    return this.http.post<ServiceResponse<string>>(`${this.apiUrl}/createproducto`, producto);
  }

  updateProducto(producto: ProductoEdit) {
    return this.http.put<ServiceResponse<ProductoEdit>>(`${this.apiUrl}/updateproducto`, producto);
  }

  getProducto(idProducto: number) {
    return this.http.get<ServiceResponse<ProductoEdit>>(`${this.apiUrl}/getproducto/${idProducto}`);
  }

  ChangeStatusProducto(idProducto: number, idEstadoProducto: number) {
    return this.http.get<ServiceResponse<string>>(`${this.apiUrl}/changestatusproducto/${idProducto}/${idEstadoProducto}`);
  }
}

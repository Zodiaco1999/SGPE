import { Injectable } from '@angular/core';
import { environment } from '..';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Modulo } from '@sgpe-ws/models';
import { DataTableServiceBase, SearchResult } from '@sgpe-ws/general';
import { ServiceResponse } from '@sgpe-ws/general';
@Injectable({
  providedIn: 'root'
})
export class ModuloService extends DataTableServiceBase<Modulo> {
  private apiUrl = `${environment.API_URL}/modulo`

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
      params
        .set('sortProperty', sortColumn)
        .set('sortDir', sortDirection);
    }

    return this.http.get<ServiceResponse<SearchResult<Modulo>>>(`${this.apiUrl}/getModulos`, { params });
  }

  createModulo(modulo: Modulo) {
    return this.http.post<ServiceResponse<object>>(`${this.apiUrl}/createmodulo`, modulo);
  }

  updateModulo(modulo: Modulo) {
    return this.http.put<ServiceResponse<object>>(`${this.apiUrl}/updatemodulo`, modulo);
  }

  getModulo(id: string) {
    return this.http.get<ServiceResponse<Modulo>>(`${this.apiUrl}/getmodulo/${id}`);
  }

  changeStatusModulo(id: string) {
    return this.http.get<ServiceResponse<object>>(`${this.apiUrl}/changestatusmodulo/${id}`);
  }
}

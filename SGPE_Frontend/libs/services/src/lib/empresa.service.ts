import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '..'
import { Empresa } from '@sgpe-ws/models';
import { DataTableServiceBase, SearchResult, ServiceResponse } from '@sgpe-ws/general';

@Injectable({
  providedIn: 'root'
})
export class EmpresaService extends DataTableServiceBase<Empresa> {
  private apiUrl = `${environment.API_URL}/empresa`

  constructor(private http: HttpClient) {
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

    return this.http.get<ServiceResponse<SearchResult<Empresa>>>(`${this.apiUrl}/getempresas`, { params })
  }

  getAllEmpresas() {
    return this.http.get<ServiceResponse<Empresa[]>>(`${this.apiUrl}/getallempresas`);
  }

  getEmpresasWithCategories() {
    return this.http.get<ServiceResponse<Empresa[]>>(`${this.apiUrl}/getempresaswithcategories`);
  }

  getEmpresa(idEmpresa: number) {
    return this.http.get<ServiceResponse<Empresa>>(`${this.apiUrl}/getempresa/${idEmpresa}`);
  }

  createEmpresa(empresa: Empresa) {
    return this.http.post<ServiceResponse<object>>(`${this.apiUrl}/createempresa`, empresa);
  }

  updateEmpresa(empresa: Empresa) {
    return this.http.put<ServiceResponse<object>>(`${this.apiUrl}/updateempresa`, empresa);
  }
}

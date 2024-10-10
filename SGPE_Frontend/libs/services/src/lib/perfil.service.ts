import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '..'
import { Perfil, PerfilMenu, UsuarioPerfil } from '@sgpe-ws/models';
import { DataTableServiceBase, SearchResult, ServiceResponse } from '@sgpe-ws/general';

@Injectable({
  providedIn: 'root'
})
export class PerfilService extends DataTableServiceBase<Perfil> {
  private apiUrl = `${environment.API_URL}/perfil`

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

    return this.http.get<ServiceResponse<SearchResult<Perfil>>>(`${this.apiUrl}/getPerfiles`, { params });
  }

  createPerfil(perfil: Perfil) {
    return this.http.post<ServiceResponse<object>>(`${this.apiUrl}/createperfil`, perfil);
  }

  updatePerfil(perfil: Perfil) {
    return this.http.put<ServiceResponse<object>>(`${this.apiUrl}/updateperfil`, perfil);
  }

  getPerfil(id: string) {
    return this.http.get<ServiceResponse<Perfil>>(`${this.apiUrl}/getperfil/${id}`);
  }

  getActivePerfiles() {
    return this.http.get<ServiceResponse<UsuarioPerfil[]>>(`${this.apiUrl}/getactiveperfiles`);
  }

  changeStatusPerfil(id: string) {
    return this.http.get<ServiceResponse<object>>(`${this.apiUrl}/changestatusperfil/${id}`);
  }

  getActiveMenus() {
    return this.http.get<ServiceResponse<PerfilMenu[]>>(`${this.apiUrl}/getactivemenus`);
  }
}

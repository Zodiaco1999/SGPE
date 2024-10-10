import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '..';
import { Usuario, UsuarioEdit } from '@sgpe-ws/models';
import { DataTableServiceBase, SearchResult, ServiceResponse} from '@sgpe-ws/general';

@Injectable({
  providedIn: 'root',
})
export class UsuarioService extends DataTableServiceBase<Usuario>  {
  private apiUrl = `${environment.API_URL}/usuario`;

  constructor(private http: HttpClient) {
    super();
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

    return this.http.get<ServiceResponse<SearchResult<Usuario>>>(`${this.apiUrl}/getusuarios`, { params });
  }

  getUsuario(idUsuario: string) {
    return this.http.get<ServiceResponse<UsuarioEdit>>(`${this.apiUrl}/getusuario/${idUsuario}`);
  }

  createUsuario(usuario: UsuarioEdit) {
    return this.http.post<ServiceResponse<object>>(`${this.apiUrl}/createusuario`, usuario);
  }

  updateUsuario(usuario: UsuarioEdit) {
    return this.http.put<ServiceResponse<object>>(`${this.apiUrl}/updateusuario`, usuario);
  }

  changeStatusUsuario(idUsuario: string) {
    return this.http.get<ServiceResponse<object>>(`${this.apiUrl}/changestatususuario/${idUsuario}`);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '..'
import { CategoriaProducto } from '@sgpe-ws/models';
import { DataTableServiceBase, SearchResult, ServiceResponse } from '@sgpe-ws/general';

@Injectable({
  providedIn: 'root'
})
export class CategoriaProductoService extends DataTableServiceBase<CategoriaProducto>{
  private apiUrl = `${environment.API_URL}/categoriaproducto`

  constructor(private http: HttpClient) {
    super()
  }

  getSearch() {
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

    return this.http.get<ServiceResponse<SearchResult<CategoriaProducto>>>(`${this.apiUrl}/getcategorias`, { params })
  }

  getAllCategorias() {
    return this.http.get<ServiceResponse<CategoriaProducto[]>>(`${this.apiUrl}/getallcategorias`);
  }

  getCategoria(idCategoriaProducto: number) {
    return this.http.get<ServiceResponse<CategoriaProducto>>(`${this.apiUrl}/getcategoria/${idCategoriaProducto}`);
  }

  getCategoriasByIdEmpresa(idEmpresa: number) {
    return this.http.get<ServiceResponse<CategoriaProducto[]>>(`${this.apiUrl}/getcategoriasbyidempresa/${idEmpresa}`);
  }

  createCategoria(categoria: CategoriaProducto) {
    return this.http.post<ServiceResponse<object>>(`${this.apiUrl}/createcategoria`, categoria);
  }

  updateCategoria(categoria: CategoriaProducto) {
    return this.http.put<ServiceResponse<object>>(`${this.apiUrl}/updatecategoria`, categoria);
  }

  changeStatusCategoria(idCategoriaProducto: number) {
    return this.http.get<ServiceResponse<CategoriaProducto>>(`${this.apiUrl}/changestatuscategoria/${idCategoriaProducto}`);
  }
}

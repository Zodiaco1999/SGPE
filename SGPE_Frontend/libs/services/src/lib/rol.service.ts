import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '..';
import { Rol } from '@sgpe-ws/models';
import { ServiceResponse } from '@sgpe-ws/general';
@Injectable({
  providedIn: 'root'
})
export class RolService {
  private apiUrl = `${environment.API_URL}/rol`

  constructor( private http: HttpClient) { }

  getRols(){
    return this.http.get<ServiceResponse<Rol[]>>(`${this.apiUrl}/getrols`);
  }

}

import { Injectable } from '@angular/core';
import { MenuService, environment } from '..';
import { HttpClient } from '@angular/common/http';
import { CambioContrasena, LoginResult, Usuario, UsuarioLogin } from '@sgpe-ws/models';
import { map, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ServiceResponse } from '@sgpe-ws/general';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.API_URL}/auth`
  private readonly JWT_TOKEN = 'JWT_TOKEN';
  private readonly USER = 'USER';
  redirectUrl = '';

  constructor(
    private http: HttpClient,
    private router: Router,
    private menuService: MenuService
    ) { }

  login(usuario: UsuarioLogin) {
    return this.http.post<ServiceResponse<LoginResult>>(`${this.apiUrl}/login`,  usuario)
    .pipe(
      tap(response => {
        this.storeTokens(response.data);
      }),
      map(r => r.success)
    );
  }

  changePassword(changePassword: CambioContrasena) {
    return this.http.put<ServiceResponse<object>>(`${this.apiUrl}/changepassword`,  changePassword);
  }

  sendEmailChangeEmail(email: string) {
    return this.http.get<ServiceResponse<object>>(`${this.apiUrl}/sendemailchangeemail/${email}`);
  }

  changeEmail(email: string, token: string) {
    return this.http.get<ServiceResponse<object>>(`${this.apiUrl}/changeemail/${email}/${token}`);
  }

  sendEmailResetPassword(email: string) {
    return this.http.get<ServiceResponse<object>>(`${this.apiUrl}/sendemailresetpassword/${email}`);
  }

  ResetPassword(resetPassword: CambioContrasena, token: string) {
    return this.http.put<ServiceResponse<object>>(`${this.apiUrl}/resetpassword/${token}`, resetPassword);
  }

  verifyToken(email: string, token: string) {
    return this.http.get<ServiceResponse<object>>(`${this.apiUrl}/verifytoken/${email}/${token}`);
  }

  getJwtToken() {
    const jwt = localStorage.getItem(this.JWT_TOKEN);
    return jwt ? JSON.parse(jwt).token : '';
  }

  private storeTokens(resultLogin: LoginResult) {
    const user = JSON.stringify(resultLogin.usuario);
    localStorage.setItem(this.USER, user);
    localStorage.setItem(this.JWT_TOKEN, resultLogin.jwt);
  }

  isLoggedIn() {
    const jwt = localStorage.getItem(this.JWT_TOKEN)
    return !!jwt;
  }

  skipUrlsAuthorize = ['/', '/auth', '/noautorizado', '/auth/recuperarcontrasena', '/usuarios/restablecercorreo'];

  autorizaUrl(url: string): boolean {
    if (this.skipUrlsAuthorize.some(f => f === url) || url.includes('/usuarios/restablecercontrasena')) {
      return true;
    }

    // Reemplazar /editar/{string} con /editar
    url = url.replace(/\/editar\/[^/]+/, '/editar');
    // Reemplazar /detalle/{string} con /detalle
    url = url.replace(/\/detalle\/[^/]+/, '/detalle');

    return this.menuService.urlAutorizada(url);;
  }

  expirationToken(): boolean {
    const jwt = localStorage.getItem(this.JWT_TOKEN);
    if (!jwt) {
      return true;
    }
    const exp = JSON.parse(jwt)?.expiration;
    const expiration = new Date(exp)
    if (expiration < new Date()) {
      this.logout();
      return true;
    }
    return false;
  }

  getUser() {
    const userString = localStorage.getItem(this.USER);
    const user: Usuario | null = JSON.parse(userString ?? '');
    return user;
  }

  getUserName(): string {
    const user = this.getUser();
    return user ? `${user.nombres} ${user.apellidos}` : '';
  }

  setUserEmail(email: string) {
    const user = this.getUser();
    if (user) {
      const newUser: Usuario = { ...user, correo: email }
      localStorage.setItem(this.USER, JSON.stringify(newUser));
    }
  }

  getUserEmail(): string {
    const user = this.getUser();
    return user ? user.correo : '';
  }

  logout() {
    this.router.navigateByUrl('/auth');
  }

  removeTokens() {
    localStorage.clear();
  }


}

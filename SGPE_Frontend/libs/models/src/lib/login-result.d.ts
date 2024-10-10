import { Usuario } from "./usuario";

export interface LoginResult {
  usuario: Usuario;
  jwt: string;
  tokenSession: string;
}

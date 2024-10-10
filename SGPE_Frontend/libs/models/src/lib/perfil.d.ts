import { PerfilMenu } from "./perfil-menu";

export interface Perfil {
  idPerfil: string;
  nombrePerfil: string;
  descPerfil: string;
  activo: boolean;

  perfilMenus: PerfilMenu[];
}

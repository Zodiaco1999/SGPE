import { UsuarioPerfil } from "./usuario-perfil"

export interface UsuarioEdit {
    idUsuario: string;
    idEmpresa: number;
    cedulaUsuario: string;
    correo: string;
    nombres: string;
    apellidos: string;
    activo: boolean
    nombreEmpresa: string;

    usuarioPerfils: UsuarioPerfil[];
}

export interface PerfilMenu {
  idPerfil: string;
  idModulo: string;
  idMenu: string;
  consulta: boolean = false;
  inserta: boolean = false;
  actualiza: boolean = false;
  elimina: boolean = false;
  activa: boolean = false;
  ejecuta: boolean = false;

  nombrePerfil?: string;
  nombreModulo?: string;
  nombreMenu?: string;
  descMenu?: string;

  menuConsulta?: boolean;
  menuInserta?: boolean;
  menuActualiza?: boolean;
  menuActiva?: boolean;
  menuElimina?: boolean;
  menuEjecuta?: boolean;
  seleccionado?: boolean;

  todos?: boolean;
  todosInde?: boolean;
}

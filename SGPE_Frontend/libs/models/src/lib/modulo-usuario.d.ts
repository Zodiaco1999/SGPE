export interface ModuloUsuario {
  idModulo: string;
  nombreModulo: string;
  descModulo: string;
  iconoPrefijo: string;
  iconoNombre: string;
  activado: boolean = false;

  menusUsuario: MenuUsuario[];
}

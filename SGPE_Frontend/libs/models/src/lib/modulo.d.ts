export interface Modulo {
  idModulo?: string;
  nombreModulo?: string;
  descModulo?: string;
  iconoPrefijo?: string;
  iconoNombre?: string;
  orden?: number;
  activo?: boolean;

  menus?: Menu[];
}

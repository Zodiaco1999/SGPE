import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon, SweetAlertPosition } from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class SweetAlertService {

  msgSimple(icon: SweetAlertIcon, title: string, position: SweetAlertPosition = 'bottom-end', timer = 4000) {
    Swal.fire({
      icon,
      title,
      position,
      showConfirmButton: false,
      timer,
    });
  }

  msgSimpleSuccess(title: string, position: SweetAlertPosition = 'bottom-end', timer = 4000) {
    Swal.fire({
      icon: 'success',
      title,
      position,
      showConfirmButton: false,
      timer,
    });
  }

  msgNormal(icon: SweetAlertIcon, title: string, text: string) {
    Swal.fire({
      icon,
      title,
      text,
    });
  }

  msgNormalError(title: string, text: string) {
    Swal.fire({
      icon: 'error',
      title,
      text,
    });
  }

  async msgComfirm(title: string, text: string, icon: SweetAlertIcon = 'question', showCancelButton = true) {
    return new Promise<boolean>((resolve) => {
      Swal.fire({
        icon,
        title,
        text,
        showCancelButton,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Comfirmar',
        cancelButtonText: 'Cancelar',
      }).then((result) => {
        if (result.isConfirmed) resolve(true);
        else resolve(false);
      });
    });
  }

}

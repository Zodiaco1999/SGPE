import { Component, Input } from '@angular/core';

@Component({
  selector: 'sgpe-ws-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss'],
})
export class ErrorComponent {
  @Input() code = '400';
  @Input() text = 'Oops ha ocurrido un error'
  @Input() textButton = 'Volver al incio'
  @Input() urlImage = 'assets/images/error-img.png'
  @Input() url =  '/';
}

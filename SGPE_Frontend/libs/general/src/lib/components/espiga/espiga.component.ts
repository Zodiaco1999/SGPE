import { Component, Input } from '@angular/core';
import { PreviousPage } from '../../models/previous-page';

@Component({
  selector: 'sgpe-ws-espiga',
  templateUrl: './espiga.component.html',
  styleUrls: ['./espiga.component.scss'],
})

export class EspigaComponent {
  @Input() title = '';
  @Input() titleColor = '';
  @Input() titleSize = '1';
  @Input() previousPages: PreviousPage[] = [];
}

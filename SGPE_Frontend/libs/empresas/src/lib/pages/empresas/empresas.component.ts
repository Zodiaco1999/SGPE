import { Component, OnInit } from '@angular/core';
import { Empresa } from '@sgpe-ws/models';
import { EmpresaService} from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-empresas',
  templateUrl: './empresas.component.html',
  styleUrls: ['./empresas.component.scss'],
})
export class EmpresasComponent implements OnInit {
  data$: Observable<Empresa[]>;
  totalRecords$: Observable<number>;

  constructor (
    public empresaService: EmpresaService
  ) {
    this.data$ = empresaService.data$;
    this.totalRecords$ = empresaService.totalRecords$;
  }

  ngOnInit() {
    this.empresaService.Search();
  }
}

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'sgpe-ws-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnInit {
  @Input() pageSizes = [10, 20, 50, 100];
  @Input() maxSize = 10;
  @Input() size: 'sm' | 'md' | 'lg' = 'sm';
  @Input() totalRecordsObs = new Observable<number>();
  totalRecords = 0;

  _page = 1;

  get page() {
    return this._page;
  }

  @Input() set page(value: number) {
    this._page = value;
    this.updateRecordNumber();
    this.pageChange.emit(this._page);
  }

  @Output() pageChange = new EventEmitter<number>();

  _pageSize = 10;

  get pageSize() {
    return this._pageSize;
  }

  @Input() set pageSize(value: number) {
    this._pageSize = value;
    this.updateRecordNumber();
    this.pageSizeChange.emit(this._pageSize);
  };

  @Output() pageSizeChange = new EventEmitter<number>();

  recordNumber = 0;
  recordMaxPage = 10;

  updateRecordNumber() {
    this.recordNumber = ((this._page - 1) * this._pageSize) + 1;
  }

  ngOnInit() {
    this.totalRecordsObs.subscribe(v => {
      this.totalRecords = v;
      const records = ((this._page) *  this._pageSize);
      this.recordMaxPage = records > this.totalRecords ? this.totalRecords : records;
    });
  }

}

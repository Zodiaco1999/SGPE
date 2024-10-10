import { SearchResult } from '../models/search-result';
import { SearchState } from '../models/search-state';
import { HttpHeaders } from '@angular/common/http';
import { SortDirection, SortEvent } from '../directives/ngbd-sortable-header.directive';
import { SweetAlertService } from '../services/sweet-alert.service';
import { ServiceResponse } from './service-response';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Subject } from 'rxjs/internal/Subject';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/internal/operators/tap';
import { debounceTime } from 'rxjs/internal/operators/debounceTime';
import { switchMap } from 'rxjs/internal/operators/switchMap';
import { inject } from '@angular/core';

export abstract class DataTableServiceBase<T> {
  alert = inject(SweetAlertService);

  public displayedColumns: string[] = [];

  protected _searchState: SearchState = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: ''
  };

  protected httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  protected _loading$ = new BehaviorSubject<boolean>(true);
  public _search$ = new Subject<void>();
  protected _data$ = new BehaviorSubject<T[]>([]);
  protected _totalRecords$ = new BehaviorSubject<number>(0);

  public abstract getSearch(): Observable<ServiceResponse<SearchResult<T>>>;

  get loading$() { return this._loading$.asObservable(); }

  get data$() { return this._data$.asObservable(); }
  get totalRecords$() { return this._totalRecords$.asObservable(); }

  constructor() {
    this.Init();
  }

  get page() { return this._searchState.page; }
  set page(page: number) { this._set({ page }); }
  get pageSize() { return this._searchState.pageSize; }
  set pageSize(pageSize: number) { this._set({ pageSize }); }
  get recordNumber() { return ((this._searchState.page - 1) * this._searchState.pageSize) + 1; }
  get recordMaxPage() {
    let recordMaxPage = ((this._searchState.page) * this._searchState.pageSize);
    recordMaxPage = recordMaxPage > this._totalRecords$.value ? this._totalRecords$.value : recordMaxPage;
    return recordMaxPage;
  }
  get searchTerm() { return this._searchState.searchTerm; }
  set searchTerm(searchTerm: string) { this._set({ searchTerm }); }
  set sortColumn(sortColumn: string) { this._set({ sortColumn }); }
  set sortDirection(sortDirection: SortDirection) { this._set({ sortDirection }); }

  protected _set(patch: Partial<SearchState>) {
    Object.assign(this._searchState, patch);
    this._search$.next();
  }

  public Search() {
    this._search$.next();
  }

  protected Init() {
    this._search$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(500),
      // distinctUntilChanged(),
      switchMap(() => this.getSearch()),
      tap(() => this._loading$.next(false)),
    ).subscribe({
      next: (result) => {
        // this.setData(result.data);
        this._data$.next(result.data.results);
        this._totalRecords$.next(result.data.rowsCount);
      },
      error: (err) => {
        this._loading$.next(false);
        this.alert.msgNormalError('Error en la consulta', err.message);
      }
  });
  }

  public onSort({ column, direction }: SortEvent) {
    if (column) {
      this._set({ sortColumn: column, sortDirection: direction });
    }
  }
}



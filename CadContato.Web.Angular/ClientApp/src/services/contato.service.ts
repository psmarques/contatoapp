import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Contato } from '../models/contato.model';
import { Observable } from 'rxjs';
import { genericResult } from '../models/genericResult.model';

@Injectable()
export class contatoService {

  private http: HttpClient;
  private baseUrl: string;
  private headerOptions = { headers: new HttpHeaders().set('Content-Type', 'application/json') }

  constructor(httpClient: HttpClient, @Inject('BASE_URL') pBaseUrl: string) {
    this.http = httpClient;
    this.baseUrl = pBaseUrl;
  }

  getAll(): Observable<Contato[]> {
    return this.http.get<Contato[]>(this.baseUrl + 'contato');
  }

  post(item: Contato): Observable<genericResult> {
    return this.http.post<genericResult>(this.baseUrl + 'contato', item, this.headerOptions);
  }

  update(item: Contato): Observable<genericResult> {
    return this.http.put<genericResult>(this.baseUrl + 'contato', item, this.headerOptions);
  }

  delete(id: String) {
    return this.http.delete<genericResult>(this.baseUrl + 'contato?id=' + id);
  }

}

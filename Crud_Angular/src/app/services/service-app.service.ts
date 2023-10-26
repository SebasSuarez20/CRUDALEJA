import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceAppService {

  public Url: string = 'https://localhost:7086/api/ControllerUser/';

  constructor(private httpcliente: HttpClient) { }

  public GetAll(NameFunction: any): Observable<any> {
    return this.httpcliente.get(`${this.Url}${NameFunction}`);
  }

  public PostAll(body: any): Observable<any> {
    return this.httpcliente.post(`${this.Url}CreateUser`, body);
  }

  public DeleteAll(id: number): Observable<any> {
    return this.httpcliente.delete(`${this.Url}DeleteUser?id=${id}`);
  }

  public DisabledAll() {

  }

  public PutAll(body: any): Observable<any> {
    return this.httpcliente.put(`${this.Url}UpdateUser`, body);
  }

}

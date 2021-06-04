import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  constructor(private http:HttpClient) { }

  GetLookup():Observable<any>{
    const api = environment.apiUrl + "Store/GetLookup";

    return this.http.get(api);
  }
}

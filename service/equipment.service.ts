import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { observable, Observable, of } from 'rxjs';
//import {equipment} from '../model/equipmenttype';
import {map} from 'rxjs/operators';
import {equipment, equipmentType, equipmentModel} from '../model/equipmenttype';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  constructor(private http:HttpClient) { }

  /*
  loadMockEquipment() : Observable<equipmentModel[]> {
    
    const eqps  = [
      {  uid: 1, transactionDate: new Date(2020,12,1), equipmentTypeDesc:'type 1', storeNo: '777'},
      {  uid: 2, transactionDate: new Date(2021,1,1), equipmentTypeDesc:'type 2', storeNo: '888'},
      {  uid: 3, transactionDate: new Date(2021,2,1), equipmentTypeDesc:'type 3', storeNo: '999'},
    ];

    return of(eqps);

    //return eqps;
  }
  
  loadEquipment(storeId:number, filter='',sortOrder='asc', pageNumber=0,pageSize=3):Observable<equipment[]>
  {
    return this.http.get(environment.apiUrl + 'Equipment/GetPagedEquipment', {
      params: new HttpParams()
        .set ('storeId', storeId.toString())
        .set('filter', filter)
        .set('sortOrder', sortOrder)
        .set('pageNumber', pageNumber.toString()) 
        .set('pageSize', pageSize.toString())
    })
    .pipe(
        map(res => res as equipment[])
      )

  }
  */

  public LoadEquipments(){
    return this.http.get(environment.apiUrl + 'EquipmentCommon/GetPagedEquipment')
    .pipe(
      map(res => console.log(res))
    )
  }

  public LoadEquipmentTransaction(storeID:number, filter='', sortOrder='asc', pageNumber = 0, pageSize=3):Observable<any>
  {
    return this.http.get(environment.apiUrl + 'EquipmentCommon/GetPagedEquipmentAdo',
      {params: new HttpParams()
        .set('storeId', storeID.toString())  
        .set('filter', filter)
        .set('sortOrder', sortOrder)
        .set('pageNumber', pageNumber.toString())
        .set('pageSize', pageSize.toString())
      }
    )
    .pipe(
      map(
          res => {
            console.log(res);
            }
        )
    )
  }
  
}

import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { AccountService } from './account.service';
import { equipment, equipmentChecklist, equipmentType } from 'app/model/equipmenttype';
import { SharedService } from './shared.service';
import { apiresponseresult, WarningType } from 'app/model/common';
import { GoingOutEquipmentDetails, GoingOutStoreInfo } from 'app/model/goingout';


@Injectable({
  providedIn: 'root'
})
export class GoingOutService {
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json').set("userID", this.accountService.userValue.id.toString()) };

  constructor(
    public _sharedService: SharedService,
    private http: HttpClient,
    private accountService: AccountService
  ) { }

  public async GetGoingOutStoreInfo(tabSelected:number,fromDate:string,toDate:string): Promise<GoingOutStoreInfo[]> {

    console.log(fromDate);
    const response = await this.http.get(environment.apiUrl + "EquipmentCommon/GetGoingOutStoreInfo/" + tabSelected + "/" + fromDate + "/" + toDate  )
      .toPromise()
      .catch((err: HttpErrorResponse) => {
        return new GoingOutStoreInfo();
        // simple logging, but you can do a lot more, see below

        console.error('An error occurred:', err.error);
      });

    return <GoingOutStoreInfo[]>response;


  }

  public async GetEquipmentBySerialNo(serialNo: string): Promise<equipment> {

    const response = await this.http.get(environment.apiUrl + "EquipmentCommon/GetEquipmentBySerialNo/" + serialNo)
      .toPromise()
      .catch((err: HttpErrorResponse) => {
        return new equipment("", "", "");
        // simple logging, but you can do a lot more, see below

        console.error('An error occurred:', err.error);
      });

    return <equipment>response;


  }


  public async GetEquipmentDetailsByGOTSerialNo(gotSerialNo: string, storeID:number): Promise<GoingOutEquipmentDetails> {

    const response = await this.http.get(environment.apiUrl + "EquipmentCommon/GetGoingOutEquipmentByGOTSerialNo/" + gotSerialNo + "/" + storeID)
      .toPromise()
      .catch((err: HttpErrorResponse) => {   
        this._sharedService.showNotification(err.error, WarningType.danger) ;   
        console.error('An error occurred:', err);
        return new equipment("", "", "");
        // simple logging, but you can do a lot more, see below
        
        
      });

    return <GoingOutEquipmentDetails>response;


  }

  async SaveGoingOut(goingOutData: GoingOutStoreInfo): Promise<apiresponseresult> {
    const response = await this.http.post(environment.apiUrl + "EquipmentCommon/PostEquipmentGoingOut",
      goingOutData, this.options).toPromise();
    return <apiresponseresult>response;
  }

  async SaveGoingOutInProcess(goingOutData: GoingOutStoreInfo): Promise<apiresponseresult> {
    console.log(goingOutData);
    const response = await this.http.post(environment.apiUrl + "EquipmentCommon/PostEquipmentGoingOutInProcess",
      goingOutData, this.options).toPromise();
    return <apiresponseresult>response;
  }
  async SaveStarted(goingOutData: GoingOutStoreInfo): Promise<apiresponseresult> {
    const response = await this.http.post(environment.apiUrl + "EquipmentCommon/PostEquipmentGoingOutStartProcess",
      goingOutData, this.options).toPromise();
    return <apiresponseresult>response;
  }  

  async DeleteAllGoingOutEquipments(goingOutData: GoingOutStoreInfo): Promise<apiresponseresult> {
    const response = await this.http.get(environment.apiUrl + "EquipmentCommon/DeleteAllGoingOutEquipments/" + 
      goingOutData.uid, this.options).toPromise();
    return <apiresponseresult>response;
  }  
  


}
import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { equipmentCheckListMapping, equipmentReceivedBack, equipmentTransactionHistory, equipmentType } from 'app/model/equipmenttype';
import { apiresponseresult, WarningType } from 'app/model/common';
import { AccountService } from './account.service';
import { SharedService } from './shared.service';

@Injectable({
  providedIn: 'root'
})
export class ReceivedbackService {

  //apiurl = "http://localhost:62794/api/";
  apiurl = environment.apiUrl;

  enteredSerialNo:string;
  enteredGotSerialNo:string;
  equipmentReceivedBack:equipmentReceivedBack;
  equipmentChecklistMapping:equipmentCheckListMapping[];
  equipmentTypes:equipmentType[];
  equipmentTransactionHistorys:equipmentTransactionHistory[];
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json').set("userID", this.accountService.userValue.id.toString()) };

  constructor(
    private http: HttpClient,
    private accountService : AccountService,
    private _sharedService:SharedService
    ) { }

  getSerialNumberDetails()
  {
    
  }
  //GetAllTransactionsForSerialNo

  // public getAllTransactionsForSerialNo(serialNo:string)
  // {
  //   this.http.get(this.apiurl + "EquipmentCommon/GetAllTransactionsForSerialNo/" + serialNo)
  //   .subscribe(result => this.equipmentTransactionHistorys = result as equipmentTransactionHistory[] )
  //   console.log("atfunction");
  //   console.log(equipmentTransactionHistory);
  // }

  async getAllTransactionsForSerialNo(serialNo:string): Promise<equipmentTransactionHistory[]> {
    const response = await this.http.get(this.apiurl + "EquipmentCommon/GetAllTransactionsForSerialNo/" + serialNo) 
    .toPromise();
    // .catch((err: HttpErrorResponse) => {
    //     return new equipmentTransactionHistory();
    //   // simple logging, but you can do a lot more, see below
    //   console.error('An error occurred:', err.error);

    // });

    return <equipmentTransactionHistory[]>response;
  }

  
  async getAllTransactionsForGotSerialNo(gotserialNo:string): Promise<equipmentTransactionHistory[]> {
    const response = await this.http.get(this.apiurl + "EquipmentCommon/GetAllTransactionsForGOTSerialNo/" + gotserialNo) 
    .toPromise(); 
    return <equipmentTransactionHistory[]>response;
  }


  async loadReceivedBackDataForGOTSerialNo(serialNo:string): Promise<equipmentReceivedBack> {
    const response = await this.http.get(this.apiurl + "EquipmentCommon/GeReceivedBackByGOTSerialNo/" + serialNo) 
    .toPromise()
    .catch((err: HttpErrorResponse) => {
        throw err;    
            
    });

    return <equipmentReceivedBack>response;
  }


  async loadReceivedBackDataForSerialNo(serialNo:string): Promise<equipmentReceivedBack> {
    const response = await this.http.get(this.apiurl + "EquipmentCommon/GeReceivedBackBySerialNo/" + serialNo) 
    .toPromise()
    .catch((err: HttpErrorResponse) => {
      throw err;              
  });

    return <equipmentReceivedBack>response;
  }


  // public loadReceivedBackDataForSerialNo(serialNo:string)
  // {

  //   // this.http.get(this.apiurl + "EquipmentCommon/GetAllEquipmentByGOTSerialNo/" + serialNo)
  //   // .toPromise().then(result => {this.equipmentReceivedBack = result as equipmentReceivedBack;
  //   // resolve("success"); } ,
  //   // msg =>{
  //   //   rejects(msg);
  //   // });
    
  //   console.log(this.equipmentReceivedBack);

  //   this.http.get(this.apiurl + "EquipmentCommon/GetAllEquipmentByGOTSerialNo/" + serialNo)
  //   .subscribe(result => this.equipmentReceivedBack = result as equipmentReceivedBack );

  //   console.log(this.equipmentReceivedBack);
  // }

  public getAllEquipmentTypes()
  {
    this.http.get(this.apiurl + "EquipmentCommon/GetAllEquipmentTypes")
    .subscribe(result => this.equipmentTypes = result as equipmentType[] )
    console.log("tpes");
    console.log(this.equipmentTypes);
  }

  public getCheckListMappings()
  {
    this.http.get(this.apiurl + "EquipmentCommon/GetEquipmentChecklistMapping/ReceivedBack")
    .subscribe(result => this.equipmentChecklistMapping = result as equipmentCheckListMapping[] )

  }

  async saveReceivedBack(): Promise<apiresponseresult> {
    const response = await this.http.post(this.apiurl + "EquipmentCommon/PostEquipmentReceivedBack", 
    this.equipmentReceivedBack,this.options).toPromise();
    return <apiresponseresult>response;
  }


}

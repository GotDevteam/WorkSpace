import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { SharedService } from 'app/service/shared.service';
import { NgForm } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { SerialtransactionhistoryComponent } from 'app/serialtransactionhistory/serialtransactionhistory.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReceivedbackService } from 'app/service/receivedback.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort, SortDirection } from '@angular/material/sort';

import {EquipmentDataSource} from '../datasource/equipment.datasource'
import {EquipmentService } from '../Service/equipment.service'
import { equipmentModel } from 'app/model/equipmenttype';

import { pipe, merge, Observable, of as observableOf } from 'rxjs';
import { map, tap, startWith, switchMap, catchError } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { StoreService } from 'app/service/store.service';

@Component({
  selector: 'app-equipmenttransaction',
  templateUrl: './equipmenttransaction.component.html',
  styleUrls: ['./equipmenttransaction.component.css']
})
export class EquipmenttransactionComponent implements AfterViewInit, OnInit {
  /*
  displayedColumns:string[] = ["uid","transactionDate", "equipmentTypeDesc", "recordTypeDescription", "transactionType", "storeID",
  "storeName","userName","reasonDescription","notes"];
  dataSource = new MatTableDataSource();
  */
  storeList: any;

  dataSource : EquipmentDataSource;  
  displayedColumns = ["equipment","serialNo", "storename","lastStatusDate", "lastStatus", "location", "grind", "purchaseDate", "warrentyEndDate","note"];
  db: EquipmentDb | null;
  data: EquipmentResult[] = [];
  data1: EquipmentResult[] = []; ;

  pageSizeOptions: number[] = [5, 10, 25, 100];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  @ViewChild(MatPaginator)  paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  equipment: equipmentModel;
  equipmentCount: number = 0;
  
  transactionStartDate:Date;
  transactionEndDate:Date;

  constructor(private _httpClient: HttpClient,
    private storeService : StoreService,
    private eqService: EquipmentService)
    {}     

  ngOnInit(): void {
    //this.data1 = [];
    
  }

  ngAfterViewInit() {
    this.loadStore();
    this.loadGridDataNew();    
  }

  loadStore(){
    this.storeService.GetLookup().pipe(
      map(res => {
        console.log(res)
      })
    ).subscribe(data => {
      this.storeList = data;
      
    })
      
  }

  async loadGridData()
  {
    this.eqService.LoadEquipmentTransaction(0,'','asc', 1, 20)
        .subscribe(res => {
          //this.data = res["payload"];
          //return this.data;
        });
  }

  async loadGridDataNew()
  {
    this.db  = new EquipmentDb(this._httpClient);
    merge()
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.db.getEquipment(0,'','asc',1,100)
            .pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.isRateLimitReached = data === null;

          if (data === null) {
            alert("Data is null")
            return [];
          }

          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.
          this.resultsLength = data.length;
          console.log(data);
          return data;
        })
      ).subscribe(data => this.data = data);

  }


  OnRowClicked(row){
    console.clear();
    console.log(row);
  }

  save() {
    //this.dialogRef.close();
  }

  close() {
    //this.dialogRef.close();
  }
  
}

export interface StoreLookupModel{
  storeID : number;
  storeName : string
}

export interface EquipmentResult{
  items: equipmentModel;
  total_count : number;
}

export class EquipmentDb{
  constructor(private _httpClient: HttpClient){}

  getEquipment(storeID:number, filter='', sortOrder='asc', pageNumber = 1, pageSize=100){
    return this._httpClient.get<any[]>(environment.apiUrl + 'EquipmentCommon/GetPagedEquipmentAdo',
    {params: new HttpParams()
      .set('storeId', storeID.toString())  
      .set('filter', filter)
      .set('sortOrder', sortOrder)
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString())
    }
    );
  }
}

import {CollectionViewer, DataSource} from '@angular/cdk/collections';
import {EquipmentService} from '../Service/equipment.service';
import { BehaviorSubject, fromEventPattern, Observable, of } from 'rxjs';
import {equipment, equipmentModel, equipmentType} from '../model/equipmenttype';
import { catchError, finalize } from 'rxjs/operators';
import { inherits } from 'util';

export class EquipmentDataSource extends DataSource<equipmentModel>{

    private eqSubject = new BehaviorSubject<equipment[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private eqService: EquipmentService){
        super();
    }

    connect(collectionViewer: CollectionViewer):Observable<any[]>{
        return this.eqSubject.asObservable();
    }

    disconnect(collectionViewer : CollectionViewer){
        this.eqSubject.complete();
        this.loadingSubject.complete();
    }

    public loadPagedResult()
    {
        this.loadingSubject.next(true);

        this.eqService.LoadEquipments();
            
    }
    /*
    loadMockEquipment(){
        this.eqService.loadMockEquipment()
    }
    
    loadEquipment(eqId:number, filter = '', sortDirection = 'asc', pageIndex = 0, pageSize = 10)
    {
        this.loadingSubject.next(true);

        this.eqService.loadEquipment(eqId, filter, sortDirection, pageIndex, pageSize)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(eq => this.eqSubject.next(eq));
    }
    */

    
}
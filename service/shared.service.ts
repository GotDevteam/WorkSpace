import { Injectable } from '@angular/core';
import { UserNotifications, WarningType } from 'app/model/common';
import { Subject } from 'rxjs';
import {AccountService} from '../service/account.service';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  public _notificationMessageSource = new Subject<UserNotifications>();

  currentUserID:number;
  currentUserName:string;

  notificationMessage$ = this._notificationMessageSource.asObservable();
  constructor(private accountservice:AccountService ) { 
    try{
    this.currentUserID = accountservice.userValue.id;
    this.currentUserName = accountservice.userValue.shortName;
    }
    catch(error)
    {

    }
  }

  public showNotification(message:string,warningtype:WarningType)
  {
    try
    {
      if (message.length > 0)    
    {
      
      
      let userNotifications:UserNotifications = new UserNotifications(message,warningtype);
      this.SendNotification(userNotifications);   
      this._notificationMessageSource.next();
      //this._notificationMessageSource.unsubscribe();      
    }

  }
  catch(error)
  {

  }

    return;

   // this.notificationsComponent.showNotification(window,position);
  }


  SendNotification(message: UserNotifications){    
    
    
    this._notificationMessageSource.next(message);    
  }

  logout(){
    
    localStorage.removeItem("currentUser");
    this.currentUserID=0;
    this.currentUserName="";

  }

}


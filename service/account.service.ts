// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class AccountService {

//   constructor() { }
// }


import { ErrorHandler, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { environment } from 'environments/environment';
import { User, loginobj, apiresponseresult } from 'app/model/common';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;
    public logindata: loginobj;
    private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
    private apiresponseResult:apiresponseresult = new apiresponseresult();

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
        this.user = this.userSubject.asObservable();
    }

    public get userValue(): User {
        return this.userSubject.value;
    }


    async login(username:string, password:string): Promise<apiresponseresult> {
      const response = await this.http.post( `${environment.apiUrl}Users/authenticate`, 
      {username:username,password:password},this.options).toPromise().then(
        user => {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('user', JSON.stringify(user));
          this.userSubject.next(<User>user);
          console.log(JSON.stringify(user));
          this.apiresponseResult.message = "Successfully logged in as " + this.userValue.userName ;
          this.apiresponseResult.success = true;
          return this.apiresponseResult;                      
      }).catch(error =>{
              let errorresponse:HttpErrorResponse = error;
              this.apiresponseResult.message = errorresponse.error;
              this.apiresponseResult.success = false;
              // alert(errorresponse.error);
              // console.error(error); }
              return this.apiresponseResult;
      }
      )           
      
      return <apiresponseresult>response;
    }

    // login<apiresponseresult>(username:string, password:string) {      
    //     return this.http.post<User>(`${environment.apiUrl}Users/authenticate`, {username:username,password:password},this.options )
    //         .subscribe(user => {
    //             // store user details and jwt token in local storage to keep user logged in between page refreshes
    //             localStorage.setItem('user', JSON.stringify(user));
    //             this.userSubject.next(user);
    //             this.apiresponseResult.message = "Success";
    //             this.apiresponseResult.success = true;
    //             return this.apiresponseResult;                
    //         }, error =>{
    //           let errorresponse:HttpErrorResponse = error;
    //           this.apiresponseResult.message = errorresponse.error;
    //           this.apiresponseResult.success = false;
    //           // alert(errorresponse.error);
    //           // console.error(error); }
    //           return this.apiresponseResult;
    //         })           
            
    // }

  //   login(username, password) {      
  //     return this.http.post<User>(`${environment.apiUrl}Users/authenticate`, {username:username,password:password},this.options )
  //         .pipe(map(user => {
  //             // store user details and jwt token in local storage to keep user logged in between page refreshes
  //             localStorage.setItem('user', JSON.stringify(user));
  //             this.userSubject.next(user);
  //             return user;                
  //         })            
  //         )
  // }

    logout() {
        // remove user from local storage and set current user to null
        localStorage.removeItem('user');
        this.userSubject.next(null);
        this.router.navigate(['/login']);
    }

    // register(user: User) {
    //     return this.http.post(`${environment.apiUrl}/users/register`, user);
    // }

    // getAll() {
    //     return this.http.get<User[]>(`${environment.apiUrl}/users`);
    // }

    // getById(id: string) {
    //     return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
    // }

    // update(id, params) {
    //     return this.http.put(`${environment.apiUrl}/users/${id}`, params)
    //         .pipe(map(x => {
    //             // update stored user if the logged in user updated their own record
    //             if (id == this.userValue.id) {
    //                 // update local storage
    //                 const user = { ...this.userValue, ...params };
    //                 localStorage.setItem('user', JSON.stringify(user));

    //                 // publish updated user to subscribers
    //                 this.userSubject.next(user);
    //             }
    //             return x;
    //         }));
    // }

    // delete(id: string) {
    //     return this.http.delete(`${environment.apiUrl}/users/${id}`)
    //         .pipe(map(x => {
    //             // auto logout if the logged in user deleted their own record
    //             if (id == this.userValue.id) {
    //                 this.logout();
    //             }
    //             return x;
    //         }));
    // }
}
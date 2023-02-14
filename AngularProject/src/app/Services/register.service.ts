import { Injectable } from '@angular/core';
import {
  HttpClient,
  
} from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  baseUrl =  environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get(this.baseUrl + '/GetUsers');
  }
  addUsers(newUser:any) {
    return this.http.post(this.baseUrl + '/PostUsers',newUser);
  }
  loginAccess(cred:any){
    return this.http.post(this.baseUrl+'/loginUsers',cred)
  }

  getRoles(){
    return this.http.get(this.baseUrl+'/getroles');
  }

  getemail(email:any){
    return this.http.get(this.baseUrl+'/GetUserDetailsbyEmail',email);

  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../shared/models/register';
import { environment } from '../../environments/environment.development';
import { Login } from '../shared/models/login';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient) {}

  login(model: Login) {
    return this.http.post(`${environment.appUrl}/api/account/login`, model);
  }

  regiter(model: Register) {
    return this.http.post(`${environment.appUrl}/api/account/register`, model);
  }
}

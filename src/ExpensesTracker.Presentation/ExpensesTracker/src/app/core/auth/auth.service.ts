import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginRequest} from '../communication/login/loginRequest';
import {LoginResponse} from '../communication/login/loginResponse';
import {Observable, tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = "http://localhost:5124/api/user/login";
  private readonly client: HttpClient;
  private tokenKey = "auth_token";

  constructor(client: HttpClient) {
    this.client = client;
  }

  public getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  public setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  public login(request: LoginRequest): Observable<LoginResponse> {
    return this.client.post<LoginResponse>(this.apiUrl, request)
      .pipe(
        tap(
          (res) => this.setToken(res.token)
        ));
  }

  public logout() {
    localStorage.removeItem(this.tokenKey);
  }
}

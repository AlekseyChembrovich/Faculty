import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthUserModel} from "../models/auth.user.model";
import {JwtHelperService} from "@auth0/angular-jwt";
import {tap} from "rxjs";
import {environment} from "../../../environments/environment";

@Injectable()
export class AuthService {
  private readonly baseApiUrl: string = "";
  private readonly keyToken = "access_token";
  private readonly nameAdminRole = "administrator";
  private readonly nameClaimRole = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
  private readonly nameClaimLogin = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
  private jwtHelper: JwtHelperService = new JwtHelperService();
  public jsonToken: { jwtToken: string } | undefined;

  constructor(private httpClient: HttpClient) {
    this.baseApiUrl = environment.authApiUrl + 'api/auth';
  }

  public login(loginUser: AuthUserModel) {
    return  this.httpClient.post<{jwtToken: string}>(this.baseApiUrl + '/login', loginUser)
      .pipe(
        tap(response => {
        this.jsonToken = response;
        let token =  this.jsonToken.jwtToken;
        localStorage.setItem(this.keyToken, token);
      }));
  }

  public logout(): void {
    localStorage.clear();
  }

  public register(loginUser: AuthUserModel) {
    return this.httpClient.post(this.baseApiUrl + '/register', loginUser);
  }

  public get isAuthenticated(): boolean {
    let token = localStorage.getItem(this.keyToken);
    if (token === null) return false;
    let jwtDate = this.jwtHelper.decodeToken(token);
    let expires: number = +jwtDate['exp'];
    let expiresDate: Date = new Date(expires * 1000);
    let nowDate = new Date;
    console.log("NowDate < ExpiresDate", nowDate < expiresDate);
    return nowDate < expiresDate;
  }

  public get isAdmin(): boolean {
    let token = localStorage.getItem(this.keyToken);
    if (token === null) return false;
    let jwtDate = this.jwtHelper.decodeToken(token);
    let roles: Array<string> = jwtDate[this.nameClaimRole];
    let isAdmin = roles.includes(this.nameAdminRole);
    return isAdmin === undefined ? false : isAdmin;
  }

  public get getUserLogin(): string | null {
    let token = localStorage.getItem(this.keyToken);
    if (token === null) return null;
    let jwtDate = this.jwtHelper.decodeToken(token);
    let login: string = jwtDate[this.nameClaimLogin];
    console.log("Login", login);
    return login;
  }

  public get getToken(): string | null {
    return localStorage.getItem(this.keyToken);
  }
}

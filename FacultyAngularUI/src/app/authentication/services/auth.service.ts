import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthUserDto} from "../models/auth.user.dto";
import {AuthOptions} from "../models/auth.options";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable()
export class AuthService {
  private readonly baseUrl: string = 'https://localhost:44342/api/auth';
  private readonly keyToken = "access_token";
  private readonly nameAdminRole = "administrator";
  private readonly nameClaimRole = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
  private readonly nameClaimLogin = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

  public authOptions: AuthOptions | undefined;
  private jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(private httpClient: HttpClient) {
  }

  public login(loginUser: AuthUserDto): void {
    this.httpClient.post<AuthOptions>(this.baseUrl + '/login', loginUser)
      .subscribe(response => {
        this.authOptions = response;
        let token =  this.authOptions.jwtToken;
        console.log(this.jwtHelper.decodeToken(token!));
        localStorage.setItem(this.keyToken, token);
      });
  }

  public logout(): void {
    localStorage.clear();
  }

  public register(loginUser: AuthUserDto): void {
    this.httpClient.post(this.baseUrl + '/register', loginUser).subscribe(response => console.log(response) );
  }

  public get isAuth(): boolean {
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

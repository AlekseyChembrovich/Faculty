import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {UserModel} from "../models/user.model";
import {UserAddModel} from "../models/user.add.model";
import {UserEditPasswordModel} from "../models/user.edit.password.model";

@Injectable()
export class UserService {
  private readonly baseUrl: string = 'https://localhost:44342/api/users';

  constructor(private httpClient: HttpClient) {
  }

  public getUsers() {
    return this.httpClient.get<UserModel[]>(this.baseUrl);
  }

  public getUser(id: string) {
    return this.httpClient.get<UserModel>(this.baseUrl + `/${id}`);
  }

  public getRoles() {
    return this.httpClient.get<Array<string>>(this.baseUrl + `/roles`);
  }

  public createUser(user: UserAddModel) {
    return this.httpClient.post(this.baseUrl, user);
  }

  public deleteUser(id: string) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateUser(user: UserModel) {
    return this.httpClient.put(this.baseUrl, user);
  }

  public updatePasswordUser(user: UserEditPasswordModel) {
    return this.httpClient.patch(this.baseUrl, user);
  }
}

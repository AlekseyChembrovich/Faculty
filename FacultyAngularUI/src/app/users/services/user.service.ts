import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {UserDto} from "../models/user.dto";
import {UserAddDto} from "../models/user.add.dto";
import {UserEditPasswordDto} from "../models/user.edit.password.dto";

@Injectable()
export class UserService {
  private readonly baseUrl: string = 'https://localhost:44342/api/users';

  constructor(private httpClient: HttpClient) {
  }

  public getUsers() {
    return this.httpClient.get<UserDto[]>(this.baseUrl);
  }

  public getUser(id: string) {
    return this.httpClient.get<UserDto>(this.baseUrl + `/${id}`);
  }

  public getRoles() {
    return this.httpClient.get<Array<string>>(this.baseUrl + `/roles`);
  }

  public createUser(user: UserAddDto) {
    return this.httpClient.post(this.baseUrl, user);
  }

  public deleteUser(id: string) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateUser(user: UserDto) {
    return this.httpClient.put(this.baseUrl, user);
  }

  public updatePasswordUser(user: UserEditPasswordDto) {
    return this.httpClient.patch(this.baseUrl, user);
  }
}

import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {GroupDisplayModel} from "../models/group.display.model";
import {GroupModel} from "../models/group.model";

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private readonly baseUrl: string = 'https://localhost:44354/api/groups';

  constructor(private httpClient: HttpClient) {
  }

  public getGroups() {
    return this.httpClient.get<GroupDisplayModel[]>(this.baseUrl);
  }

  public getGroup(id: number) {
    return this.httpClient.get<GroupModel>(this.baseUrl + `/${id}`);
  }

  public createGroup(group: GroupModel) {
    return this.httpClient.post(this.baseUrl, group);
  }

  public deleteGroup(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateGroup(group: GroupModel) {
    return this.httpClient.put(this.baseUrl, group);
  }
}

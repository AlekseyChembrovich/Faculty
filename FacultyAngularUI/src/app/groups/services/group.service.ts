import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {GroupDisplayModel} from "../models/group.display.model";
import {GroupModel} from "../models/group.model";
import {environment} from "../../../environments/environment";

@Injectable()
export class GroupService {
  private readonly baseUrl: string = "";

  constructor(private httpClient: HttpClient) {
    this.baseUrl = environment.resourceApiUrl + 'api/groups';
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

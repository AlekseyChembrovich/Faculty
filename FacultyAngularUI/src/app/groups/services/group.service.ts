import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {GroupDisplayDto} from "../models/group.display.dto";
import {GroupDto} from "../models/group.dto";

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private readonly baseUrl: string = 'https://localhost:44354/api/groups';

  constructor(private httpClient: HttpClient) {
  }

  public getGroups() {
    return this.httpClient.get<GroupDisplayDto[]>(this.baseUrl);
  }

  public getGroup(id: number) {
    return this.httpClient.get<GroupDto>(this.baseUrl + `/${id}`);
  }

  public createGroup(group: GroupDto) {
    return this.httpClient.post(this.baseUrl, group);
  }

  public deleteGroup(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateGroup(group: GroupDto) {
    return this.httpClient.put(this.baseUrl, group);
  }
}

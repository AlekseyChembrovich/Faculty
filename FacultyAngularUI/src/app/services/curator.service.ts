import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CuratorDto } from "../dto/curator/curator.dto";

@Injectable({
  providedIn: 'root'
})
export class CuratorsService {
  readonly baseUser: string = 'https://localhost:44354/api/curators';

  constructor(private httpClient: HttpClient) {
  }

  public getCurators() {
    return this.httpClient.get<CuratorDto[]>(this.baseUser);
  }

  public createCurator(curator: CuratorDto) {
    return this.httpClient.post<CuratorDto[]>(this.baseUser, curator)
      .subscribe(response => console.log(response));
  }
}

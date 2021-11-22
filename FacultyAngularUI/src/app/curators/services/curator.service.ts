import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {CuratorModel} from "../models/curator.model";

@Injectable()
export class CuratorsService {
  private readonly baseUrl: string = 'https://localhost:44354/api/curators';

  constructor(private httpClient: HttpClient) {
  }

  public getCurators() {
    return this.httpClient.get<CuratorModel[]>(this.baseUrl);
  }

  public getCurator(id: number) {
    return this.httpClient.get<CuratorModel>(this.baseUrl + `/${id}`);
  }

  public createCurator(curator: CuratorModel) {
    return this.httpClient.post(this.baseUrl, curator);
  }

  public deleteCurator(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateCurator(curator: CuratorModel) {
    return this.httpClient.put(this.baseUrl, curator);
  }
}

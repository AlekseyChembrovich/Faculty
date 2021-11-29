import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {SpecializationModel} from "../models/specialization.model";
import {environment} from "../../../environments/environment";

@Injectable()
export class SpecializationService {
  private readonly baseUrl: string = "";

  constructor(private httpClient: HttpClient) {
    this.baseUrl = environment.resourceApiUrl + 'api/specializations';
  }

  public getSpecializations() {
    return this.httpClient.get<SpecializationModel[]>(this.baseUrl);
  }

  public getSpecialization(id: number) {
    return this.httpClient.get<SpecializationModel>(this.baseUrl + `/${id}`);
  }

  public createSpecialization(specialization: SpecializationModel) {
    return this.httpClient.post(this.baseUrl, specialization);
  }

  public deleteSpecialization(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateSpecialization(specialization: SpecializationModel) {
    return this.httpClient.put(this.baseUrl, specialization);
  }
}

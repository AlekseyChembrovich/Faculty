import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {SpecializationDto} from "../models/specialization.dto";

@Injectable({
  providedIn: 'root'
})
export class SpecializationService {
  private readonly baseUrl: string = 'https://localhost:44354/api/specializations';

  constructor(private httpClient: HttpClient) {
  }

  public getSpecializations() {
    return this.httpClient.get<SpecializationDto[]>(this.baseUrl);
  }

  public getSpecialization(id: number) {
    return this.httpClient.get<SpecializationDto>(this.baseUrl + `/${id}`);
  }

  public createSpecialization(specialization: SpecializationDto) {
    return this.httpClient.post(this.baseUrl, specialization);
  }

  public deleteSpecialization(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateSpecialization(specialization: SpecializationDto) {
    return this.httpClient.put(this.baseUrl, specialization);
  }
}

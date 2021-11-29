import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {FacultyDisplayModel} from "../models/faculty.display.model";
import {FacultyModel} from "../models/faculty.model";
import {environment} from "../../../environments/environment";

@Injectable()
export class FacultyService {
  private readonly baseUrl: string = "";

  constructor(private httpClient: HttpClient) {
    this.baseUrl = environment.resourceApiUrl + 'api/faculties';
  }

  public getFaculties() {
    return this.httpClient.get<FacultyDisplayModel[]>(this.baseUrl);
  }

  public getFaculty(id: number) {
    return this.httpClient.get<FacultyModel>(this.baseUrl + `/${id}`);
  }

  public createFaculty(faculty: FacultyModel) {
    return this.httpClient.post(this.baseUrl, faculty);
  }

  public deleteFaculty(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateFaculty(faculty: FacultyModel) {
    return this.httpClient.put(this.baseUrl, faculty);
  }
}

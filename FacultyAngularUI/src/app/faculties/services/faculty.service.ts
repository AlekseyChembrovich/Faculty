import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {FacultyDisplayDto} from "../models/faculty.display.dto";
import {FacultyDto} from "../models/faculty.dto";

@Injectable({
  providedIn: 'root'
})
export class FacultyService {
  private readonly baseUrl: string = 'https://localhost:44354/api/faculties';

  constructor(private httpClient: HttpClient) {
  }

  public getFaculties() {
    return this.httpClient.get<FacultyDisplayDto[]>(this.baseUrl);
  }

  public getFaculty(id: number) {
    return this.httpClient.get<FacultyDto>(this.baseUrl + `/${id}`);
  }

  public createFaculty(faculty: FacultyDto) {
    return this.httpClient.post(this.baseUrl, faculty);
  }

  public deleteFaculty(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateFaculty(faculty: FacultyDto) {
    return this.httpClient.put(this.baseUrl, faculty);
  }
}

import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StudentModel} from "../models/student.model";

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private readonly baseUrl: string = 'https://localhost:44354/api/students';

  constructor(private httpClient: HttpClient) {
  }

  public getStudents() {
    return this.httpClient.get<StudentModel[]>(this.baseUrl);
  }

  public getStudent(id: number) {
    return this.httpClient.get<StudentModel>(this.baseUrl + `/${id}`);
  }

  public createStudent(student: StudentModel) {
    return this.httpClient.post(this.baseUrl, student);
  }

  public deleteStudent(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateStudent(student: StudentModel) {
    return this.httpClient.put(this.baseUrl, student);
  }
}

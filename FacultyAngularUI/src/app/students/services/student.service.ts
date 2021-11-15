import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StudentDto} from "../models/student.dto";

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private readonly baseUrl: string = 'https://localhost:44354/api/students';

  constructor(private httpClient: HttpClient) {
  }

  public getStudents() {
    return this.httpClient.get<StudentDto[]>(this.baseUrl);
  }

  public getStudent(id: number) {
    return this.httpClient.get<StudentDto>(this.baseUrl + `/${id}`);
  }

  public createStudent(student: StudentDto) {
    return this.httpClient.post(this.baseUrl, student);
  }

  public deleteStudent(id: number) {
    return this.httpClient.delete(this.baseUrl + `/${id}`);
  }

  public updateStudent(student: StudentDto) {
    return this.httpClient.put(this.baseUrl, student);
  }
}

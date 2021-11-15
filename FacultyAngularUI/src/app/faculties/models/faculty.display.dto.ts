export class FacultyDisplayDto {
  constructor(public startDateEducation: Date,
              public countYearEducation: number, public studentSurname: string,
              public groupName: string, public curatorSurname: string, public id?: number) {
  }
}

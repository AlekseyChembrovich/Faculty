export class UserAddDto {
  constructor(public login: string, public roles: Array<string>, public birthday: Date, public password: string, public id?: string) {
  }
}

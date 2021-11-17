export class UserDto {
  constructor(public login: string, public roles: Array<string>, public birthday: Date, public id?: string) {
  }
}

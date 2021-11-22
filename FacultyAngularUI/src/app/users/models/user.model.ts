export class UserModel {
  constructor(public login: string, public roles: Array<string>, public birthday: Date, public id?: string) {
  }
}

export interface Script {
  DbType: string;
  User: User;
  DbScript: string;
  ConStr: string;
}
export interface User {
  Id: number;
  Name: string;
  AccType: string;
}


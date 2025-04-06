export interface LoginResponse {
  userId: number;
  userName: string;
  typeId: number;
}

export interface VerifiedCodeResponse {
  token: string;
  userId: number;
  userName: string;
  typeId: number;
}

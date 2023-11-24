export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  expiresIn: number;
  userToken: UserLoginResponse;
}
export interface UserLoginResponse {
  id: string;
  email: string;
  claims: ClaimsResponse[];
}
export interface ClaimsResponse {
  value: string;
  type: string;
}
export interface AuthorResponse {
  id: string;
  name: string;
  lastName: string;
  fullName: string;
  email: string;
  phoneNumber: string;
}
export interface AuthorRequest {
  name: string;
  lastName: string;
  fullName: string;
  email: string;
  phoneNumber: string;
} 
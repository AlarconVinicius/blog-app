import { ImageRequest, ImageResponse } from "../image/image.model";

export interface AuthorResponse {
  id: string;
  name: string;
  lastName: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  profileImage: ImageResponse;
}
export interface AuthorRequest {
  name: string;
  lastName: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  profileImage: ImageRequest;
} 

export interface UserPasswordRequest {
  oldPassword: string;
  newPassword: string;
  confirmNewPassword: string;
} 
export class User{
  token?: string;
}

export class RegisterRequest {
  login?: string;
  password?: string;
  confirmPassword?: string;
}
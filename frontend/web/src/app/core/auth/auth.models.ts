export interface ILoginRequest {
  username: string;
  password: string;
  rememberMe: boolean;
}

export interface IAuthUser {
  id: number;
  username: string;
  email: string;
  fullName: string;
  roles: string[];
  permissions: string[];
  companyId: number;
  branchId: number | null;
  avatarUrl: string | null;
}

export interface ILoginResponse {
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
  user: IAuthUser;
}

export interface ITokenPayload {
  sub: string;
  email: string;
  roles: string[];
  permissions: string[];
  companyId: number;
  exp: number;
  iat: number;
}

export interface IRefreshTokenRequest {
  refreshToken: string;
}

export interface IAuthState {
  user: IAuthUser | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  error: string | null;
}

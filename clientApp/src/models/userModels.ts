export enum Gender {
  Male = 'Male',
  Female = 'Female',
}

export type UserModel = {
  token: string;
  gender: Gender;
  isAuthenticated: () => boolean;
};

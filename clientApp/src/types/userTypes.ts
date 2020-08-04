import { string } from "yup";

export enum Gender {
  Male = 'Male',
  Female = 'Female',
}

export enum MeasurementSystemName {
  Imperial = 'Imperial',
  Metric = 'Metric',
}

export type MeasurementPreference = {
  measurementSystemName: MeasurementSystemName;
  lengthUnit: string;
  weightUnit: string;
};

export type CreateUserType = {
  email: string;
  password: string;
  confirmedPassword: string;
  height: number | string;
  gender: Gender;
  unitsOfMeasure: MeasurementSystemName;
};

export type CreateUserResultError = 'email';

export type CreateUserResult = {
  errors: Record<CreateUserResultError, string>;
  succeeded: boolean;
  token: string;
};

export type ChangePasswordType = {
  currentPassword: string;
  newPassword: string;
  confirmedNewPassword: string;
};

export type ChangePasswordResultError = 'currentPassword';

export type ChangePasswordResult = {
  errors: Record<ChangePasswordResultError, string>;
  succeeded: boolean;
};

export type ResetPasswordStepOneType = {
  email: string
}

export type ResetPasswordStepTwoType = {
  resetPasswordToken: string,
  newPassword: string,
  confirmedNewPassword: string
}

export type ResetPasswordValidationResult = {
  succeeded: boolean,
  errorMessage: string
}

export type ResetPasswordResult = {
  succeeded: boolean,
  errorMessage: string
}


export type User = {
  gender: Gender;
  height: number;
  email: string;
  measurementPreference: MeasurementPreference;
};

export type UserContextType = User & {
  isAuthenticated: () => boolean;
  setGender: (gender: Gender) => void;
  setHeight: (height: number) => void;
  setEmail: (email: string) => void;
  setMeasurementPreference: (measurementPreference: MeasurementPreference) => void;
};

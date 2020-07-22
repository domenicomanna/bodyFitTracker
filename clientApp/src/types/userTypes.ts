export enum Gender {
  Male = 'Male',
  Female = 'Female',
}

export enum MeasurementSystemName {
  Imperial = 'Imperial',
  Metric = 'Metric'
} ;

export type MeasurementPreference = {
  measurementSystemName: MeasurementSystemName
  lengthUnit: string;
  weightUnit: string;
};

export type CreateUserType = {
  email: string,
  password: string,
  confirmedPassword: string,
  height: number | string,
  gender: Gender
  unitsOfMeasure: MeasurementSystemName
}

export type CreateUserResultError = 'email';

export type CreateUserResult = {
  errors: Record<CreateUserResultError, string>,
  succeeded: boolean,
  token: string
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

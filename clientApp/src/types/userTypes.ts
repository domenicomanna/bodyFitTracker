export enum Gender {
  Male = 'Male',
  Female = 'Female',
}

export type MeasurementPreference = {
  measurementSystemName: 'Imperial' | 'Metric';
  lengthUnit: string;
  weightUnit: string;
};

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

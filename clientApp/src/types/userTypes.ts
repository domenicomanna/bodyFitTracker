export enum Gender {
  Male = 'Male',
  Female = 'Female',
}

export type MeasurementPreference = {
  measurementSystemName: 'Imperial' | 'Metric';
  lengthUnit: string;
  weightUnit: string;
};

export type UserSettings = {
  gender: Gender;
  height: number;
  email: string;
  measurementPreference: MeasurementPreference;
};

export type UserContextType = UserSettings & {
  token: string;
  isAuthenticated: () => boolean;
  setHeight: (height: number) => void;
  setEmail: (email: string) => void;
  setMeasurementPreference: (measurementPreference: MeasurementPreference) => void;
  setToken: (token: string) => void;
};

export enum Gender {
  Male = 'Male',
  Female = 'Female',
}

export type MeasurementPreferenceModel = {
  measurementSystemName: 'Imperial' | 'Metric';
  lengthUnit: string;
  weightUnit: string;
};

export type UserModel = {
  token: string;
  gender: Gender;
  height: number;
  measurementPreference: MeasurementPreferenceModel;
  isAuthenticated: () => boolean;
};

export type UserSettings = {
  gender: Gender;
  height: number;
  email: string;
  measurementPreference: MeasurementPreferenceModel;
};

export type UserContextModel = UserSettings & {
  token: string;
  isAuthenticated: () => boolean;
  setHeight: (height: number) => void;
  setEmail: (email: string) => void;
  setMeasurementPreference: (measurementPreference: MeasurementPreferenceModel) => void;
  setToken: (token: string) => void;
};

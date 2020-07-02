export enum Gender {
  Male = 'Male',
  Female = 'Female',
}

export type MeasurementPreferenceModel = {
  measurementSystemName: 'Imperial' | 'Metric',
  lengthUnit: string,
  weightUnit: string
}

export type UserModel = {
  token: string;
  gender: Gender;
  measurementPreference: MeasurementPreferenceModel
  isAuthenticated: () => boolean;
};

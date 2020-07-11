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
  height: number,
  measurementPreference: MeasurementPreferenceModel
  isAuthenticated: () => boolean;
};

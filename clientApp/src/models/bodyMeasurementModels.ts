import { Gender } from './userModels';

export type BodyMeasurementModel = {
  bodyMeasurementId: number;
  neckCircumference: number;
  waistCircumference: number;
  hipCircumference?: number;
  bodyFatPercentage: number;
  weight: number;
  dateAdded: Date;
};

export type MeasurementModel = {
  name: string;
  abbreviation: string;
};

export type BodyMeasurementCollectionModel = {
  measurementSystemName: string;
  genderTypeName: Gender;
  length: MeasurementModel;
  weight: MeasurementModel;
  bodyMeasurements: BodyMeasurementModel[];
};

export type CreateOrEditMeasurementModel = {
  neckCircumference: string | number;
  waistCircumference: string | number;
  hipCircumference?: string | number;
  weight: string | number;
  creationDate: string | Date;
};

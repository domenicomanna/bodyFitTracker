export type BodyMeasurementModel = {
  bodyMeasurementId: number;
  neckCircumference: number;
  waistCircumference: number;
  hipCircumference?: number;
  bodyFatPercentage: number;
  weight: number;
  dateAdded: Date;
};

export type CreateOrEditMeasurementModel = {
  neckCircumference: string | number;
  waistCircumference: string | number;
  hipCircumference?: string | number;
  height: string | number;
  weight: string | number;
  dateAdded: string | Date;
};

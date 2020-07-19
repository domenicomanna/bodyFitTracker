export type BodyMeasurementType = {
  bodyMeasurementId: number;
  neckCircumference: number;
  waistCircumference: number;
  hipCircumference?: number;
  bodyFatPercentage: number;
  weight: number;
  dateAdded: Date;
};

export type CreateOrEditMeasurement = {
  neckCircumference: string | number;
  waistCircumference: string | number;
  hipCircumference?: string | number;
  height: string | number;
  weight: string | number;
  dateAdded: string | Date;
};

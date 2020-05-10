export type BodyMeasurementModel = {
    bodyMeasurementId: number,
    neckCircumference: number,
    waistCircumference: number,
    hipCircumference?: number,
    bodyFatPercentage: number,
    weight: number,
    dateAdded: Date
}

export type MeasurementModel = {
    name: string,
    abbreviation: string
}

export type BodyMeasurementCollectionModel = {
    measurementSystemName: string,
    genderTypeName: string,
    length: MeasurementModel,
    weight: MeasurementModel,
    bodyMeasurements: BodyMeasurementModel[]
}

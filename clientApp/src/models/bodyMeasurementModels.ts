import { Gender } from "./gender"

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
    genderTypeName: Gender,
    length: MeasurementModel,
    weight: MeasurementModel,
    bodyMeasurements: BodyMeasurementModel[]
}

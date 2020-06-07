import {get, post, put, destroy} from './baseConfiguration';
import { BodyMeasurementCollectionModel } from '../models/bodyMeasurementModels';
import { AxiosResponse } from 'axios';


const requests = {
    getAllMeasurements: () => get('bodyMeasurements').then(response => response.data),
    deleteMeasurement: (id: number) => destroy(`bodyMeasurements/${id}`)
}

const bodyMeasurementsClient = {
    getAllMeasurements: (): Promise<BodyMeasurementCollectionModel> => requests.getAllMeasurements(),
    deleteMeasurement: (id: number): Promise<AxiosResponse> => requests.deleteMeasurement(id)
}

export default bodyMeasurementsClient;
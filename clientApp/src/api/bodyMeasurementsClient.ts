import {get, post, put, destroy} from './baseConfiguration';
import { BodyMeasurementCollectionModel } from '../models/bodyMeasurementModels';


const requests = {
    getAllMeasurements: () => get('bodyMeasurements').then(response => response.data)
}

const bodyMeasurementsClient = {
    getAllMeasurements: (): Promise<BodyMeasurementCollectionModel> => requests.getAllMeasurements(),
}

export default bodyMeasurementsClient;
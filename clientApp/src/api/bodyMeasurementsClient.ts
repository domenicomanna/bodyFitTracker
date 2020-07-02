import { get, post, put, destroy } from './baseConfiguration';
import { BodyMeasurementModel, CreateOrEditMeasurementModel } from '../models/bodyMeasurementModels';
import { AxiosResponse } from 'axios';

const requests = {
  getAllMeasurements: () => get('bodyMeasurements').then((response) => response.data.bodyMeasurements),
  deleteMeasurement: (id: number) => destroy(`bodyMeasurements/${id}`),
  createMeasurement: (createMeasurementModel: CreateOrEditMeasurementModel) =>
    post('bodyMeasurements', createMeasurementModel),
};

const bodyMeasurementsClient = {
  getAllMeasurements: (): Promise<BodyMeasurementModel[]> => requests.getAllMeasurements(),
  deleteMeasurement: (id: number): Promise<AxiosResponse> => requests.deleteMeasurement(id),
  createMeasurement: (createMeasurementModel: CreateOrEditMeasurementModel): Promise<AxiosResponse> =>
    requests.createMeasurement(createMeasurementModel),
};

export default bodyMeasurementsClient;

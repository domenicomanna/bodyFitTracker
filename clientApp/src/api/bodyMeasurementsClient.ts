import { get, post, put, destroy } from './baseConfiguration';
import { BodyMeasurementType, CreateOrEditMeasurement } from '../types/bodyMeasurementTypes';
import { AxiosResponse } from 'axios';

const requests = {
  getAllMeasurements: () => get('bodyMeasurements').then((response) => response.data),
  getMeasurement: (id: number) => get(`bodyMeasurements/${id}`).then((response) => response.data),
  deleteMeasurement: (id: number) => destroy(`bodyMeasurements/${id}`),
  createMeasurement: (createMeasurementModel: CreateOrEditMeasurement) =>
    post('bodyMeasurements', createMeasurementModel),
  editMeasurement: (id: number, editMeasurementModel: CreateOrEditMeasurement) => 
    put(`bodyMeasurements/${id}`, editMeasurementModel),
};

const bodyMeasurementsClient = {
  getAllMeasurements: (): Promise<BodyMeasurementType[]> => requests.getAllMeasurements(),
  getMeasurement: (id: number): Promise<CreateOrEditMeasurement> => requests.getMeasurement(id),
  deleteMeasurement: (id: number): Promise<AxiosResponse> => requests.deleteMeasurement(id),
  createMeasurement: (createMeasurementModel: CreateOrEditMeasurement): Promise<AxiosResponse> =>
    requests.createMeasurement(createMeasurementModel),
  editMeasurement: (id: number, editMeasurementModel: CreateOrEditMeasurement): Promise<AxiosResponse> =>
    requests.editMeasurement(id, editMeasurementModel),
};

export default bodyMeasurementsClient;

import { get, post, put, destroy } from './baseConfiguration';
import { BodyMeasurementType, CreateOrEditMeasurement } from '../types/bodyMeasurementTypes';
import { AxiosResponse } from 'axios';

const requests = {
  getAllMeasurements: () => get('bodyMeasurements').then((response) => response.data),
  getMeasurement: (id: number) => get(`bodyMeasurements/${id}`).then((response) => response.data),
  deleteMeasurement: (id: number) => destroy(`bodyMeasurements/${id}`),
  createMeasurement: (createMeasurementType: CreateOrEditMeasurement) =>
    post('bodyMeasurements', createMeasurementType),
  editMeasurement: (id: number, editMeasurementType: CreateOrEditMeasurement) => 
    put(`bodyMeasurements/${id}`, editMeasurementType),
};

const bodyMeasurementsClient = {
  getAllMeasurements: (): Promise<BodyMeasurementType[]> => requests.getAllMeasurements(),
  getMeasurement: (id: number): Promise<CreateOrEditMeasurement> => requests.getMeasurement(id),
  deleteMeasurement: (id: number): Promise<AxiosResponse> => requests.deleteMeasurement(id),
  createMeasurement: (createMeasurementType: CreateOrEditMeasurement): Promise<AxiosResponse> =>
    requests.createMeasurement(createMeasurementType),
  editMeasurement: (id: number, editMeasurementType: CreateOrEditMeasurement): Promise<AxiosResponse> =>
    requests.editMeasurement(id, editMeasurementType),
};

export default bodyMeasurementsClient;

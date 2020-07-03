import { get, post, put, destroy } from './baseConfiguration';
import { BodyMeasurementModel, CreateOrEditMeasurementModel } from '../models/bodyMeasurementModels';
import { AxiosResponse } from 'axios';

const requests = {
  getAllMeasurements: () => get('bodyMeasurements').then((response) => response.data),
  getMeasurement: (id: number) => get(`bodyMeasurements/${id}`).then((response) => response.data),
  deleteMeasurement: (id: number) => destroy(`bodyMeasurements/${id}`),
  createMeasurement: (createMeasurementModel: CreateOrEditMeasurementModel) =>
    post('bodyMeasurements', createMeasurementModel),
  editMeasurement: (id: number, editMeasurementModel: CreateOrEditMeasurementModel) => 
    put(`bodyMeasurements/${id}`, editMeasurementModel),
};

const bodyMeasurementsClient = {
  getAllMeasurements: (): Promise<BodyMeasurementModel[]> => requests.getAllMeasurements(),
  getMeasurement: (id: number): Promise<CreateOrEditMeasurementModel> => requests.getMeasurement(id),
  deleteMeasurement: (id: number): Promise<AxiosResponse> => requests.deleteMeasurement(id),
  createMeasurement: (createMeasurementModel: CreateOrEditMeasurementModel): Promise<AxiosResponse> =>
    requests.createMeasurement(createMeasurementModel),
  editMeasurement: (id: number, editMeasurementModel: CreateOrEditMeasurementModel): Promise<AxiosResponse> =>
    requests.editMeasurement(id, editMeasurementModel),
};

export default bodyMeasurementsClient;

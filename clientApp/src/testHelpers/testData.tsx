import { UserContextType, Gender, MeasurementPreference } from '../types/userTypes';
import { AxiosResponse } from 'axios';

export const defaultUserContextType: UserContextType = {
  gender: Gender.Female,
  token: '',
  height: 60,
  email: '',
  measurementPreference: {
    measurementSystemName: 'Imperial',
    weightUnit: 'lb',
    lengthUnit: 'in',
  },
  setHeight: () => {},
  setEmail: () => {},
  setMeasurementPreference: () => {},
  isAuthenticated: () => false,
  setToken: () => {},
};

export const defaultAxiosResponse: AxiosResponse = {
  data: '',
  status: 200,
  statusText: 'OK',
  config: {},
  headers: {},
};

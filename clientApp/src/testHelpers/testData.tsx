import { UserContextType, Gender, MeasurementPreference } from '../types/userTypes';
import { AxiosResponse } from 'axios';

export const defaultUserContextType: UserContextType = {
  gender: Gender.Female,
  height: 60,
  email: '',
  measurementPreference: {
    measurementSystemName: 'Imperial',
    weightUnit: 'lb',
    lengthUnit: 'in',
  },
  setGender: () => {},
  setHeight: () => {},
  setEmail: () => {},
  setMeasurementPreference: () => {},
  isAuthenticated: () => false,
};

export const defaultAxiosResponse: AxiosResponse = {
  data: '',
  status: 200,
  statusText: 'OK',
  config: {},
  headers: {},
};

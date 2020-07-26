import { UserContextType, Gender, MeasurementPreference, MeasurementSystemName, User } from '../types/userTypes';
import { AxiosResponse } from 'axios';

export const defaultUserContextType: UserContextType = {
  gender: Gender.Female,
  height: 60,
  email: '',
  measurementPreference: {
    measurementSystemName: MeasurementSystemName.Imperial,
    weightUnit: 'lb',
    lengthUnit: 'in',
  },
  setGender: () => {},
  setHeight: () => {},
  setEmail: () => {},
  setMeasurementPreference: () => {},
  isAuthenticated: () => false,
};

export const defaultUser: User = {
  email: '',
  height: 60,
  gender: Gender.Female,
  measurementPreference: {
    measurementSystemName: MeasurementSystemName.Imperial,
    lengthUnit: 'in',
    weightUnit: 'lb',
  },
};

export const defaultAxiosResponse: AxiosResponse = {
  data: '',
  status: 200,
  statusText: 'OK',
  config: {},
  headers: {},
};

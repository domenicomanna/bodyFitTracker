import React, { createContext, useState, FunctionComponent } from 'react';
import { UserModel, MeasurementPreferenceModel } from '../models/userModels';
import { Gender } from '../models/userModels';

export const UserContext = createContext({} as UserModel);

const UserContextProvider: FunctionComponent = ({ children }) => {
  const [token, setToken] = useState('');
  const [gender, setGender] = useState<Gender>(Gender.Male);
  const [measurementPreference, setMeasurementPreference] = useState<MeasurementPreferenceModel>({
    measurementSystemName : 'Imperial',
    weightUnit : 'lb',
    lengthUnit : 'in',
  });

  const isAuthenticated = () => {
    return true;
  };

  return <UserContext.Provider value={{ token, isAuthenticated, gender, measurementPreference }}>{children}</UserContext.Provider>;
};

export default UserContextProvider;

import React, { createContext, useState, FunctionComponent } from 'react';
import { UserModel, MeasurementPreferenceModel, UserContextModel } from '../models/userModels';
import { Gender } from '../models/userModels';

export const UserContext = createContext({} as UserContextModel);

const UserContextProvider: FunctionComponent = ({ children }) => {
  const [token, setToken] = useState('');
  const [gender, setGender] = useState<Gender>(Gender.Male);
  const [email, setEmail] = useState('');
  const [measurementPreference, setMeasurementPreference] = useState<MeasurementPreferenceModel>({
    measurementSystemName: 'Imperial',
    weightUnit: 'lb',
    lengthUnit: 'in',
  });
  const [height, setHeight] = useState(60);

  const isAuthenticated = () => {
    return true;
  };

  return (
    <UserContext.Provider
      value={{
        gender,
        height,
        email,
        measurementPreference,
        token,
        setHeight,
        setEmail,
        setMeasurementPreference,
        setToken,
        isAuthenticated,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export default UserContextProvider;

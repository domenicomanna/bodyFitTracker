import React, { createContext, useState, FunctionComponent } from 'react';
import { MeasurementPreference, UserContextType } from '../types/userTypes';
import { Gender } from '../types/userTypes';
import tokenKey from '../constants/tokenKey';
import { string } from 'yup';

export const UserContext = createContext({} as UserContextType);

const UserContextProvider: FunctionComponent = ({ children }) => {
  const [gender, setGender] = useState<Gender>(Gender.Male);
  const [email, setEmail] = useState('');
  const [measurementPreference, setMeasurementPreference] = useState<MeasurementPreference>({
    measurementSystemName: 'Imperial',
    weightUnit: 'lb',
    lengthUnit: 'in',
  });
  const [height, setHeight] = useState(60);

  const isAuthenticated = () => {
    const token = localStorage.getItem(tokenKey);
    return token ? true : false;
  };

  return (
    <UserContext.Provider
      value={{
        gender,
        height,
        email,
        measurementPreference,
        setGender,
        setHeight,
        setEmail,
        setMeasurementPreference,
        isAuthenticated,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export default UserContextProvider;

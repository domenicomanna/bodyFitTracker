import React, { createContext, useState, FunctionComponent, useEffect } from 'react';
import { MeasurementPreference, UserContextType, MeasurementSystemName, User } from '../types/userTypes';
import { Gender } from '../types/userTypes';
import tokenKey from '../constants/tokenKey';
import isExpired from '../utils/tokenExpiration/tokenExpiration';
import usersClient from '../api/usersClient';

export const UserContext = createContext({} as UserContextType);

const UserContextProvider: FunctionComponent = ({ children }) => {
  const [userDetailsAreBeingFetched, setUserDetailsAreBeingFetched] = useState(true);
  const [gender, setGender] = useState<Gender>(Gender.Male);
  const [email, setEmail] = useState('');
  const [measurementPreference, setMeasurementPreference] = useState<MeasurementPreference>({
    measurementSystemName: MeasurementSystemName.Imperial,
    weightUnit: 'lb',
    lengthUnit: 'in',
  });
  const [height, setHeight] = useState(0);

  const isAuthenticated = (): boolean => {
    const token = localStorage.getItem(tokenKey);
    if (!token) return false;
    return !isExpired(token);
  };


  useEffect(() => {
    const fetchUser = async () => {
      const user: User = await usersClient.getUser();
      setHeight(user.height);
      setEmail(user.email);
      setGender(user.gender);
      setMeasurementPreference(user.measurementPreference);
      setUserDetailsAreBeingFetched(false)
    };
    if (isAuthenticated()) fetchUser();
    else setUserDetailsAreBeingFetched(false);
  }, []);

  return (
    <UserContext.Provider
      value={{
        userDetailsAreBeingFetched,
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

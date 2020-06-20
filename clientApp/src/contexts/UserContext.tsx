import React, { createContext, useState, FunctionComponent } from 'react';
import { UserModel } from '../models/userModels';
import { Gender } from '../models/userModels';

export const UserContext = createContext({} as UserModel);

const UserContextProvider: FunctionComponent = ({ children }) => {
  const [token, setToken] = useState('');
  const [gender, setGender] = useState<Gender>(Gender.Female);

  const isAuthenticated = () => {
    return true;
  };

  return <UserContext.Provider value={{ token, isAuthenticated, gender }}>{children}</UserContext.Provider>;
};

export default UserContextProvider;

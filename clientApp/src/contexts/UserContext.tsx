import React, { createContext, useState, FunctionComponent } from 'react';
import { Gender } from '../models/gender';

export type UserContextModel = {
  token: string;
  gender: Gender,
  isAuthenticated: () => boolean;
}

export const UserContext = createContext({} as UserContextModel);

const UserContextProvider:FunctionComponent = ({ children }) => {
  const [token, setToken] = useState('');
  const [gender, setGender] = useState<Gender>(Gender.Female);

  const isAuthenticated = () => {
    return true;
  };

  return (
    <UserContext.Provider value={{ token, isAuthenticated, gender}}>
      {children}
    </UserContext.Provider>
  );
};

export default UserContextProvider;

import React, { createContext, useState, FunctionComponent } from 'react';

interface IUserContext {
  token: string;
  isAuthenticated: () => boolean;
}

export const UserContext = createContext({} as IUserContext);

const UserContextProvider:FunctionComponent = ({ children }) => {
  const [token, setToken] = useState('');

  const isAuthenticated = () => {
    return true;
  };

  return (
    <UserContext.Provider value={{ token, isAuthenticated }}>
      {children}
    </UserContext.Provider>
  );
};

export default UserContextProvider;

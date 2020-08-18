import React, { useContext } from 'react';
import AuthenticatedLayout from '../authenticatedLayout/AuthenticatedLayout';
import UnauthenticatedLayout from '../unauthenticatedLayout/UnauthenticatedLayout';
import { UserContext } from '../../contexts/UserContext';

export const WrappingLayout: React.FunctionComponent = ({ children }) => {
  const { isAuthenticated } = useContext(UserContext);

  if (isAuthenticated()) return <AuthenticatedLayout>{children}</AuthenticatedLayout>;
  return <UnauthenticatedLayout>{children}</UnauthenticatedLayout>;
};

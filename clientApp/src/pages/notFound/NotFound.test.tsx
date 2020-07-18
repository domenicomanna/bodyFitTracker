import React from 'react';
import { render, screen } from '@testing-library/react';
import NotFound from './NotFound';
import { UserModel, Gender, UserContextModel } from '../../models/userModels';
import { UserContext } from '../../contexts/UserContext';
import { MemoryRouter } from 'react-router-dom';
import { defaultUserContextModel } from '../../testHelpers/testData';

let userContextModel: UserContextModel;

beforeEach(() => {
  userContextModel = defaultUserContextModel;
});

const handleRendering = (isAuthenticated: boolean) => {
  userContextModel.isAuthenticated = () => isAuthenticated;
  return render(
    <UserContext.Provider value={userContextModel}>
      <NotFound />
    </UserContext.Provider>,
    { wrapper: MemoryRouter }
  );
};

it('should render authenticated layout if user is authenticated', () => {
  handleRendering(true);
  const myProfileElement = screen.getByText(/My Profile/i);
  const loginElement = screen.queryByText(/Login/i);

  expect(myProfileElement).toBeTruthy();
  expect(loginElement).toBeFalsy();
});

it('should render unauthenticated layout if user is not authenticated', () => {
  handleRendering(false);
  const myProfileElement = screen.queryByText(/My Profile/i);
  const loginElement = screen.getByText(/Login/i);

  expect(myProfileElement).toBeFalsy();
  expect(loginElement).toBeTruthy();
});

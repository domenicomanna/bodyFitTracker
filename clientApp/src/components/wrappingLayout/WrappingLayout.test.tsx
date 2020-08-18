import React from 'react';
import { render, screen } from '@testing-library/react';
import { UserContextType } from '../../types/userTypes';
import { UserContext } from '../../contexts/UserContext';
import { MemoryRouter } from 'react-router-dom';
import { defaultUserContextType } from '../../testHelpers/testData';
import { WrappingLayout } from './WrappingLayout';

let userContextType: UserContextType;

beforeEach(() => {
  userContextType = defaultUserContextType;
});

const handleRendering = (isAuthenticated: boolean) => {
  userContextType.isAuthenticated = () => isAuthenticated;
  return render(
    <UserContext.Provider value={userContextType}>
      <WrappingLayout>
        <h1>hi</h1>
      </WrappingLayout>
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

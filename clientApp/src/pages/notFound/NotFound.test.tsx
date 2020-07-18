import React from 'react';
import { render, screen } from '@testing-library/react';
import NotFound from './NotFound';
import { UserModel, Gender } from '../../models/userModels';
import { UserContext } from '../../contexts/UserContext';
import { MemoryRouter } from 'react-router-dom';

let userModel: UserModel;

beforeEach(() => {
  userModel = {
    isAuthenticated: () => false,
    gender: Gender.Female,
    token: '',
    height: 60,
    measurementPreference: {
      measurementSystemName: 'Imperial',
      weightUnit: 'lb',
      lengthUnit: 'in',
    },
  };
});

const handleRendering = (isAuthenticated: boolean) => {
  userModel.isAuthenticated = () => isAuthenticated;
  return render(
    <UserContext.Provider value={userModel}>
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

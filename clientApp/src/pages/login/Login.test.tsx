import React from 'react';
import { Router, Route, Switch } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import Login from './Login';
import { mocked } from 'ts-jest/utils';
import authenticationClient from '../../api/authenticationClient';
import usersClient from '../../api/usersClient';
import { Gender, UserContextType, User, MeasurementSystemName } from '../../types/userTypes';
import { UserContext } from '../../contexts/UserContext';
import { defaultUserContextType } from '../../testHelpers/testData';
import routeUrls from '../../constants/routeUrls';

jest.mock('../../api/usersClient');
jest.mock('../../api/authenticationClient');

let mockedAuthenticationClient = mocked(authenticationClient, true);
let mockedUsersClient = mocked(usersClient, true);
let userContextType: UserContextType;
let user: User;

beforeEach(() => {
  mockedAuthenticationClient.signIn.mockReset;
  mockedUsersClient.getUser.mockReset;
  userContextType = defaultUserContextType;
  user = {
    email: '',
    height: 60,
    gender: Gender.Female,
    measurementPreference: {
      measurementSystemName: MeasurementSystemName.Imperial,
      lengthUnit: 'in',
      weightUnit: 'lb',
    },
  };
});

const TestComponent = () => {
  return <div data-testid='testComponent'>testing component</div>;
};

const handleRendering = () => {
  const route = '/login';
  const history = createMemoryHistory();
  history.push(route);
  return render(
    <UserContext.Provider value={userContextType}>
      <Router history={history}>
        <Switch>
          <Route path={route} component={Login} />
          <Route path={routeUrls.home} component={TestComponent} />
        </Switch>
      </Router>
    </UserContext.Provider>
  );
};

describe('Component when form is invalid', () => {
  it('should display an error message when the form is invalid', async () => {
    handleRendering();
    fireEvent.click(screen.getByText(/sign in/i));
    const errorMessageElement = await screen.findByTestId('signInErrorMessage');
    expect(errorMessageElement).toBeTruthy();
  });
});

describe('Component when form is valid', () => {
  it('should display the returned error message when sign in is unsuccessful', async () => {
    handleRendering();
    const errorMessage = 'sign in failed';
    mockedAuthenticationClient.signIn.mockResolvedValue({ signInWasSuccessful: false, errorMessage, token: '' });

    const emailInput = screen.getByLabelText(/email/i);
    const passwordInput = screen.getByLabelText(/password/i);

    await waitFor(() => fireEvent.change(emailInput, { target: { value: 'd@gmail.com' } }));
    await waitFor(() => fireEvent.change(passwordInput, { target: { value: 'abc' } }));
    fireEvent.click(screen.getByText(/sign in/i));

    const errorMessageElement = await screen.findByText(errorMessage);
    expect(errorMessageElement).toBeTruthy();
  });

  it('should redirect on successful sign in', async () => {
    handleRendering();
    const errorMessage = 'sign in failed';
    mockedAuthenticationClient.signIn.mockResolvedValue({ signInWasSuccessful: true, errorMessage, token: '' });
    mockedUsersClient.getUser.mockResolvedValue(user);

    const emailInput = screen.getByLabelText(/email/i);
    const passwordInput = screen.getByLabelText(/password/i);

    await waitFor(() => fireEvent.change(emailInput, { target: { value: 'd@gmail.com' } }));
    await waitFor(() => fireEvent.change(passwordInput, { target: { value: 'abc' } }));
    fireEvent.click(screen.getByText(/sign in/i));

    const testComponentElement = await screen.findByTestId('testComponent');
    expect(testComponentElement).toBeTruthy();
  });
});

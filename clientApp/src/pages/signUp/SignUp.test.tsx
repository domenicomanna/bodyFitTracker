import React from 'react';
import { mocked } from 'ts-jest/utils';
import { Router, Route, Switch } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import usersClient from '../../api/usersClient';
import { UserContextType, User, CreateUserResult } from '../../types/userTypes';
import { defaultUserContextType, defaultUser } from '../../testHelpers/testData';
import { render, screen, fireEvent, getByLabelText, waitFor } from '@testing-library/react';
import { UserContext } from '../../contexts/UserContext';
import routeUrls from '../../constants/routeUrls';
import SignUp from './SignUp';

jest.mock('../../api/usersClient');
jest.mock('../../api/authenticationClient');

let mockedUsersClient = mocked(usersClient, true);
let userContextType: UserContextType;

beforeEach(() => {
  mockedUsersClient.createUser.mockReset;
  mockedUsersClient.getUser.mockReset;
  userContextType = defaultUserContextType;
});

const TestComponent = () => {
  return <div data-testid='testComponent'>testing component</div>;
};

const handleRendering = () => {
  const route = '/signUp';
  const history = createMemoryHistory();
  history.push(route);
  return render(
    <UserContext.Provider value={userContextType}>
      <Router history={history}>
        <Switch>
          <Route path={route} component={SignUp} />
          <Route path={routeUrls.home} component={TestComponent} />
        </Switch>
      </Router>
    </UserContext.Provider>
  );
};

describe('Component when different untis of measure are selected', () => {
  it('should display the correct imperial units if imperial is selected', async () => {
    handleRendering();
    const imperialRadioButton = screen.getByLabelText(/imperial/i);
    fireEvent.click(imperialRadioButton);
    const lengthUnit = await screen.findByTestId(/lengthUnit/i);
    expect(lengthUnit.textContent?.toLowerCase()).toBe('in');
  });

  it('should display the correct metric units if metric is selected', async () => {
    handleRendering();
    const metricRadioButton = screen.getByLabelText(/metric/i);
    fireEvent.click(metricRadioButton);
    const lengthUnit = await screen.findByTestId(/lengthUnit/i);
    expect(lengthUnit.textContent?.toLowerCase()).toBe('cm');
  });
});

describe('Component when creating an account fails', () => {
  it('should display the error indicating why the account could not be created', async () => {
    handleRendering();
    const emailError = 'the email address is already taken';
    mockedUsersClient.createUser.mockResolvedValue({
      succeeded: false,
      token: '',
      errors: {
        email: emailError,
      },
    });

    const emailInput = screen.getByLabelText(/email/i);
    const passwordInput = screen.getByLabelText('Password');
    const confirmedPasswordInput = screen.getByLabelText('Confirm Password');
    const heightInput = screen.getByLabelText(/height/i);

    await waitFor(() => fireEvent.change(emailInput, { target: { value: 'd@gmail.com' } }));
    await waitFor(() => fireEvent.change(passwordInput, { target: { value: 'abcd' } }));
    await waitFor(() => fireEvent.change(confirmedPasswordInput, { target: { value: 'abcd' } }));
    await waitFor(() => fireEvent.change(heightInput, { target: { value: '60' } }));

    expect(screen.queryByText(emailError)).toBeFalsy();
    fireEvent.click(screen.getByText(/Submit/i));
    expect(await screen.findByText(emailError)).toBeTruthy();
  });
});

describe('Component when account creation succeeds', () => {
  it('should redirect the user if account creation is successful', async () => {
    handleRendering();
    mockedUsersClient.createUser.mockResolvedValue({
      succeeded: true,
      token: 'abcd',
      errors: {
        email: '',
      },
    });

    mockedUsersClient.getUser.mockResolvedValue(defaultUser);

    const emailInput = screen.getByLabelText(/email/i);
    const passwordInput = screen.getByLabelText('Password');
    const confirmedPasswordInput = screen.getByLabelText('Confirm Password');
    const heightInput = screen.getByLabelText(/height/i);

    await waitFor(() => fireEvent.change(emailInput, { target: { value: 'd@gmail.com' } }));
    await waitFor(() => fireEvent.change(passwordInput, { target: { value: 'abcd' } }));
    await waitFor(() => fireEvent.change(confirmedPasswordInput, { target: { value: 'abcd' } }));
    await waitFor(() => fireEvent.change(heightInput, { target: { value: '60' } }));

    fireEvent.click(screen.getByText(/Submit/i));

    const testComponentElement = await screen.findByTestId('testComponent');
    expect(testComponentElement).toBeTruthy();
  });
});

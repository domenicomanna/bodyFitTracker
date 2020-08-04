import React from 'react';
import { createMemoryHistory } from 'history';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import { mocked } from 'ts-jest/utils';
import { Router, Route } from 'react-router-dom';
import ResetPasswordStepTwo from './ResetPasswordStepTwo';
import routeUrls from '../../../constants/routeUrls';
import usersClient from '../../../api/usersClient';
import { ResetPasswordValidationResult } from '../../../types/userTypes';

jest.mock('../../../api/usersClient');

let mockedUsersClient = mocked(usersClient, true);
const invalidTokenComponentTestId = 'invalidTokenComponent';
const loginComponentTestId = 'login';
const validTokenResponse: ResetPasswordValidationResult = { succeeded: true, errorMessage: '' };
const invalidTokenResponse: ResetPasswordValidationResult = { succeeded: false, errorMessage: 'invalid token' };

beforeEach(() => {
  mockedUsersClient.validateResetPasswordToken.mockReset();
});

const InvalidTokenComponent = () => {
  return <div data-testid={invalidTokenComponentTestId}>invalid token </div>;
};

const LoginComponent = () => {
  return <div data-testid={loginComponentTestId}>login...</div>;
};

const handleRendering = (measurementWasCreated: boolean = false, measurementWasEdited: boolean = false) => {
  const history = createMemoryHistory();
  const route = '/reset-password/abcde';
  history.push(route);
  return render(
    <Router history={history}>
      <Route path='/reset-password/:token' component={ResetPasswordStepTwo} />
      <Route path={routeUrls.resetPassword.invalidToken} component={InvalidTokenComponent} />
      <Route path={routeUrls.login} component={LoginComponent} />
    </Router>
  );
};

describe('Component when the page is initially loaded', () => {
  it('should show a loader when the token is first being validated', async () => {
    mockedUsersClient.validateResetPasswordToken.mockResolvedValue(validTokenResponse);

    handleRendering();
    const pageLoaderElement = await screen.findByTestId('pageLoader');
    expect(pageLoaderElement).toBeTruthy();
  });

  it('should show the form content if the token is valid', async () => {
    mockedUsersClient.validateResetPasswordToken.mockResolvedValue(validTokenResponse);

    handleRendering();
    const formContent = await screen.findByTestId('formContent');
    expect(formContent).toBeTruthy();
  });

  it('should redirect the user to an invalid token page if the token is invalid', async () => {
    mockedUsersClient.validateResetPasswordToken.mockResolvedValue(invalidTokenResponse);

    handleRendering();
    const invalidTokenComponent = await screen.findByTestId(invalidTokenComponentTestId);
    expect(invalidTokenComponent).toBeTruthy();
  });
});

describe('Component on form submit', () => {
  it('should redirect the user to an invalid token page if the token becomes invalid', async () => {
		// this could happen if the user loads the pages and idles for a long time causing the token to expire
		mockedUsersClient.validateResetPasswordToken.mockResolvedValue(validTokenResponse);
    mockedUsersClient.resetPasswordStepTwo.mockResolvedValue({ succeeded: false, errorMessage: '' });
    handleRendering();

    const newPasswordInput = await waitFor(() => screen.getByLabelText('New Password'));
    const confirmNewPasswordInput = await waitFor(() => screen.getByLabelText('Confirm New Password'));
    await waitFor(() => fireEvent.change(newPasswordInput, { target: { value: 'abcd' } }));
    await waitFor(() => fireEvent.change(confirmNewPasswordInput, { target: { value: 'abcd' } }));

    const submitButton = await waitFor(() => screen.getByText('Reset Password'));
    fireEvent.click(submitButton);

    const invalidTokenComponent = await screen.findByTestId(invalidTokenComponentTestId);
    expect(invalidTokenComponent).toBeTruthy();
  });

  it('should redirect the user to a login page if the token is still valid', async () => {
		mockedUsersClient.validateResetPasswordToken.mockResolvedValue(validTokenResponse);
    mockedUsersClient.resetPasswordStepTwo.mockResolvedValue({ succeeded: true, errorMessage: '' });
    handleRendering();

    const newPasswordInput = await waitFor(() => screen.getByLabelText('New Password'));
    const confirmNewPasswordInput = await waitFor(() => screen.getByLabelText('Confirm New Password'));
    await waitFor(() => fireEvent.change(newPasswordInput, { target: { value: 'abcd' } }));
    await waitFor(() => fireEvent.change(confirmNewPasswordInput, { target: { value: 'abcd' } }));

    const submitButton = await waitFor(() => screen.getByText('Reset Password'));
    fireEvent.click(submitButton);

    const loginComponent = await screen.findByTestId(loginComponentTestId);
    expect(loginComponent).toBeTruthy();
	});
});

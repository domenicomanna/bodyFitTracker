import React from 'react';
import { Router, Route, Switch } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { mocked } from 'ts-jest/utils';
import usersClient from '../../api/usersClient';
import routeUrls from '../../constants/routeUrls';
import { ChangePassword } from './ChangePassword';

jest.mock('../../api/usersClient');

const idForTestComponent = 'testComponent';
let mockedUsersClient = mocked(usersClient, true);

beforeEach(() => {
  mockedUsersClient.changePassword.mockReset();
});

const TestComponent = () => {
  return <div data-testid={idForTestComponent}>testing component</div>;
};

const handleRendering = () => {
  const route = '/change-password';
  const history = createMemoryHistory();
  history.push(route);
  return render(
    <Router history={history}>
      <Switch>
        <Route path={route} component={ChangePassword} />
        <Route path={routeUrls.login} component={TestComponent} />
      </Switch>
    </Router>
  );
};

it('should display the returned error message when changing the password in is unsuccessful', async () => {
  handleRendering();
  const errorMessage = 'The entered password is invalid';
  mockedUsersClient.changePassword.mockResolvedValue({
    succeeded: false,
    errors: {
      currentPassword: errorMessage,
    },
  });

  const currentPasswordInput = screen.getByLabelText('Current Password');
  const newPasswordInput = screen.getByLabelText('New Password');
  const confirmedNewPasswordInput = screen.getByLabelText('Confirm New Password');

  await waitFor(() => fireEvent.change(currentPasswordInput, { target: { value: 'random' } }));
  await waitFor(() => fireEvent.change(newPasswordInput, { target: { value: 'abcde' } }));
  await waitFor(() => fireEvent.change(confirmedNewPasswordInput, { target: { value: 'abcde' } }));
  fireEvent.click(screen.getByText(/Submit/i));

  const errorMessageElement = await screen.findByText(errorMessage);
  expect(errorMessageElement).toBeTruthy();
});

it('should redirect the user when changing the password is successful', async () => {
  handleRendering();
  mockedUsersClient.changePassword.mockResolvedValue({
    succeeded: true,
    errors: {
      currentPassword: '',
    },
  });

  const currentPasswordInput = screen.getByLabelText('Current Password');
  const newPasswordInput = screen.getByLabelText('New Password');
  const confirmedNewPasswordInput = screen.getByLabelText('Confirm New Password');

  await waitFor(() => fireEvent.change(currentPasswordInput, { target: { value: 'random' } }));
  await waitFor(() => fireEvent.change(newPasswordInput, { target: { value: 'abcde' } }));
  await waitFor(() => fireEvent.change(confirmedNewPasswordInput, { target: { value: 'abcde' } }));
  fireEvent.click(screen.getByText(/Submit/i));

  const testComponentElement = await screen.findByTestId(idForTestComponent);
  expect(testComponentElement).toBeTruthy();
});

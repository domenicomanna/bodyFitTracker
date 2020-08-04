import React from 'react';
import { createMemoryHistory } from 'history';
import { Router, Route } from 'react-router-dom';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import { mocked } from 'ts-jest/utils';
import routeUrls from '../../../constants/routeUrls';
import usersClient from '../../../api/usersClient';
import { ResetPasswordStepOne } from './ResetPasswordStepOne';
import { defaultAxiosResponse } from '../../../testHelpers/testData';

const successComponentTestId = 'success';
jest.mock('../../../api/usersClient');
let mockedUsersClient = mocked(usersClient, true);

beforeEach(()=>{
	mockedUsersClient.resetPasswordStepOne.mockReset();
})

const SuccessComponent = () => {
  return <div data-testid={successComponentTestId}>Success</div>;
};

const handleRendering = () => {
  const history = createMemoryHistory();
  const route = '/reset-password-step-one/';
  history.push(route);
  return render(
    <Router history={history}>
      <Route path={route} component={ResetPasswordStepOne} />
      <Route path={routeUrls.resetPassword.stepOneSuccess} component={SuccessComponent} />
    </Router>
  );
};

it('should redirect the user to the success page after the form is submitted', async () => {
	handleRendering();
	mockedUsersClient.resetPasswordStepOne.mockResolvedValue(defaultAxiosResponse);

	const emailInput = screen.getByLabelText(/Email/i);
	await waitFor(() => fireEvent.change(emailInput, { target: { value: 'abc@gmail.com' } }));

	const findAccountButton = screen.getByText(/Find Account/i)
	fireEvent.click(findAccountButton);

	const successComponent = await screen.findByTestId(successComponentTestId);
	expect(successComponent).toBeTruthy();
})

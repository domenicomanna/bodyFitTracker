import React from 'react';
import { Router, Route, Switch } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { UserContext } from '../../contexts/UserContext';
import { defaultUserContextType } from '../../testHelpers/testData';
import EditAccount from './EditAccount';
import routeUrls from '../../constants/routeUrls';
import { mocked } from 'ts-jest/utils';
import usersClient from '../../api/usersClient';

jest.mock('../../api/usersClient');

let mockedUsersClient = mocked(usersClient, true);
const testIdForHomeComponent = 'home';

beforeEach(() => {
  mockedUsersClient.changeProfileSettings.mockReset();
});

const HomeComponent = () => {
  return <div data-testid={testIdForHomeComponent}>testing component</div>;
};

const handleRendering = () => {
  const history = createMemoryHistory();
  const path = '/';
  history.push(path);
  return render(
    <Router history={history}>
      <Switch>
        <UserContext.Provider value={defaultUserContextType}>
          <Route path={path} component={EditAccount} />
          <Route path={routeUrls.home} component={HomeComponent} />
        </UserContext.Provider>
      </Switch>
    </Router>
  );
};

it('should redirect to the home page after settings have been changed', async () => {
  handleRendering();
  const emailInput = await screen.findByLabelText('Email');
  await waitFor(() => fireEvent.change(emailInput, { target: { value: 'random@gmail.com' } }));

  fireEvent.click(screen.getByText(/Edit Settings/i, { selector: 'button' }));

  const testComponentElement = await screen.findByTestId(testIdForHomeComponent);
  expect(testComponentElement).toBeTruthy();
});

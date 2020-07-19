import React from 'react';
import { Router, Route, Switch } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import ProtectedRoute from './ProtectedRoute';
import { UserContext } from '../../contexts/UserContext';
import { UserContextType } from '../../types/userTypes';
import { defaultUserContextType } from '../../testHelpers/testData';

const TestComponent = () => {
  return <div data-testid='protectedComponent'>testing component</div>;
};

const FakeLoginComponent = () => {
  return <div data-testid='loginComponent'>Login</div>;
};

const route = '/test';
let userContextType: UserContextType;

beforeEach(() => {
  userContextType = defaultUserContextType;
});

const handleRendering = (isAuthenticated: boolean) => {
  const history = createMemoryHistory();
  history.push(route);
  userContextType.isAuthenticated = () => isAuthenticated;
  return render(
    <UserContext.Provider value={userContextType}>
      <Router history={history}>
        <Switch>
          <ProtectedRoute component={TestComponent} path={route} />
          <Route path='/login' component={FakeLoginComponent} />
        </Switch>
      </Router>
    </UserContext.Provider>
  );
};

it('should redirect the user to an unprotected component if the user is not authenticated', () => {
  handleRendering(false);
  const loginComponent = screen.getByTestId('loginComponent');
  const protectedComponent = screen.queryByTestId(/protectedComponent/i);
  expect(loginComponent).toBeTruthy();
  expect(protectedComponent).toBeFalsy();
});

it('should render the protected component if the user is authenticated', () => {
  handleRendering(true);
  const protectedComponent = screen.getByTestId(/protectedComponent/i);
  expect(protectedComponent).toBeTruthy();
});

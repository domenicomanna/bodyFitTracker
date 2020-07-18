import React from 'react';
import { Router, Route } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { render, fireEvent, screen } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import ProtectedRoute from './ProtectedRoute';
import { UserContext } from '../../contexts/UserContext';
import { UserContextModel } from '../../models/userModels';
import { defaultUserContextModel } from '../../testHelpers/testData';

const TestComponent = () => {
  return <div>testing component</div>;
};

const FakeLoginComponent = () => {
  return <div>Login</div>
}

const route = '/test';
let userContextModel: UserContextModel;

beforeEach(() => {
  userContextModel = defaultUserContextModel
});

it('should render a login page if the user is not authenticated', () => {
  const history = createMemoryHistory();
  history.push(route);
  userContextModel.isAuthenticated = () => false;
  render(
    <Router history={history}>
      <UserContext.Provider value={userContextModel}>
        <ProtectedRoute component={TestComponent} path={route} />
        <Route path="/login" component={FakeLoginComponent}/>
      </UserContext.Provider>
    </Router>
  );

  const loginElement = screen.getByText(/login/i);
  expect(loginElement).toBeTruthy();
});

it('should render the component if the user is authenticated', () => {
  const history = createMemoryHistory();
  history.push(route);
  userContextModel.isAuthenticated = () => true;
  render(
    <Router history={history}>
      <UserContext.Provider value={userContextModel}>
        <ProtectedRoute render={() => <TestComponent />} path={route} />{' '}
        {/* test that the render prop instead of the component prop */}
      </UserContext.Provider>
    </Router>
  );
  
  const loginElement = screen.getByText(/testing component/i);
  expect(loginElement).toBeTruthy();
});

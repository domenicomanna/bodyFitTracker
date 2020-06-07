import React from 'react'
import { Router } from 'react-router-dom'
import { createMemoryHistory } from 'history'
import { render, fireEvent, screen } from '@testing-library/react'
import '@testing-library/jest-dom/extend-expect'
import ProtectedRoute from './ProtectedRoute'
import { UserContext, UserContextModel } from '../../contexts/UserContext'

const TestComponent = () => {
    return(
        <div>testing component</div>
    )
}

const route = "/test";
let userContextValue: UserContextModel

beforeEach( () => {
    userContextValue = {
        token : "", 
        isAuthenticated : () => false
    }
})

it('should render a login page if the user is not authenticated', () => {
    const history = createMemoryHistory();
    history.push(route)
    userContextValue.isAuthenticated = () => false;
    render(
      <Router history={history}>
          <UserContext.Provider value = {userContextValue}>
            <ProtectedRoute component = {TestComponent} path={route}/>
          </UserContext.Provider>
      </Router>
    )
    const loginElement = screen.getByText(/login/i);
    expect(loginElement).toBeTruthy;
})

it('should render the component if the user is authenticated', () => {
    const history = createMemoryHistory();
    history.push(route)
    userContextValue.isAuthenticated = () => true;
    render(
      <Router history={history}>
          <UserContext.Provider value = {userContextValue}>
            <ProtectedRoute render = { () => <TestComponent/> }  path={route}/> {/* test that the render prop instead of the component prop */} 
          </UserContext.Provider>
      </Router>
    )
    const loginElement = screen.getByText(/testing component/i);
    expect(loginElement).toBeTruthy;
})
import React, { FunctionComponent, useContext } from 'react';
import { RouteProps, Route, RouteComponentProps } from 'react-router-dom';
import UnauthenticatedApp from '../UnauthenticatedApp';
import { UserContext } from '../../contexts/UserContext';

const ProtectedRoute: FunctionComponent<RouteProps> = ({ component: Component, ...rest }) => {
  if (!Component) {
    throw Error('component is undefined');
  }

  const userContext = useContext(UserContext);

  const render = (props: RouteComponentProps<any>): React.ReactNode => {
    return userContext.isAuthenticated() ? <Component {...props} /> : <UnauthenticatedApp />;
  };

  return <Route {...rest} render={render} />;
};

export default ProtectedRoute;

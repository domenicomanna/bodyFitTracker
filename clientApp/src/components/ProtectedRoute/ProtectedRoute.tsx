import React, { FunctionComponent, useContext } from 'react';
import { RouteProps, Route, RouteComponentProps } from 'react-router-dom';
import UnauthenticatedApp from '../UnauthenticatedApp';
import { UserContext } from '../../contexts/UserContext';

const ProtectedRoute: FunctionComponent<RouteProps> = ({ component: Component, ...rest }) => {
  
  if (!Component && !rest.render) {
    throw Error('component and render method are both undefined');
  }

  const userContext = useContext(UserContext);
  
  const render = (props: RouteComponentProps<any>): React.ReactNode => {
    if (rest.render){
      return userContext.isAuthenticated() ? rest.render(props) : <UnauthenticatedApp/>
    }
    if (!Component) throw Error('component is undefined'); // this is redundant but typescript will not compile if this line is removed
    return userContext.isAuthenticated() ? <Component {...props} /> : <UnauthenticatedApp />;
  };

  return <Route {...rest} render={render} />;
};

export default ProtectedRoute;

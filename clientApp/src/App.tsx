import React, { useContext } from 'react';
import { UserContext } from './contexts/UserContext';
import AuthenticatedApp from './containers/AuthenticatedApp';
import UnauthenticatedApp from './containers/UnauthenticatedApp';

function App() {
  const userContext = useContext(UserContext)
  
  return userContext.isAuthenticated() ? <AuthenticatedApp/> : <UnauthenticatedApp/> 
}

export default App;

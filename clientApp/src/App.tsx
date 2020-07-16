import React, { useContext } from 'react';
import { UserContext } from './contexts/UserContext';
import AuthenticatedApp from './components/AuthenticatedApp';
import UnauthenticatedApp from './components/UnauthenticatedApp';

function App() {
  const userContext = useContext(UserContext)
  return <UnauthenticatedApp/>
  // return userContext.isAuthenticated() ? <AuthenticatedApp/> : <UnauthenticatedApp/> 
}

export default App;

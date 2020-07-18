import React from 'react';
import { Switch, Route } from 'react-router-dom';
import routeUrls from './constants/routeUrls';
import AuthenticatedLayout from './components/authenticatedLayout/AuthenticatedLayout';
import ProtectedRoute from './components/protectedRoute/ProtectedRoute';
import NotFound from './pages/notFound/NotFound';
import BodyMeasurementsPage from './pages/bodyMeasurementsPage/BodyMeasurementsPage';
import CreateOrEditMeasurementPage from './pages/createOrEditMeasurementPage/CreateOrEditMeasurementPage';
import About from './pages/about/About';
import UnauthenticatedLayout from './components/unauthenticatedLayout/UnauthenticatedLayout';
import Login from './pages/login/Login';

function App() {
  return (
    <Switch>
      <Route exact path={[routeUrls.home, routeUrls.editMeasurement, routeUrls.createMeasurement]}>
        <AuthenticatedLayout>
          <ProtectedRoute path={routeUrls.home} exact component={BodyMeasurementsPage} />
          <ProtectedRoute path={routeUrls.editMeasurement} component={CreateOrEditMeasurementPage} />
          <ProtectedRoute path={routeUrls.createMeasurement} component={CreateOrEditMeasurementPage} />
        </AuthenticatedLayout>
      </Route>

      <Route exact path={[routeUrls.login, routeUrls.about]}>
        <UnauthenticatedLayout>
          <Route path={routeUrls.login} exact component={Login} />
          <Route path={routeUrls.about} exact component={About} />
        </UnauthenticatedLayout>
      </Route>

      <Route render={() => <NotFound />} />
    </Switch>
  );
}

export default App;

import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import routeUrls from './constants/routeUrls';
import AuthenticatedLayout from './components/authenticatedLayout/AuthenticatedLayout';
import ProtectedRoute from './components/protectedRoute/ProtectedRoute';
import NotFound from './pages/notFound/NotFound';
import BodyMeasurementsPage from './pages/bodyMeasurementsPage/BodyMeasurementsPage';
import CreateOrEditMeasurementPage from './pages/createOrEditMeasurementPage/CreateOrEditMeasurementPage';
import About from './pages/about/About';
import UnauthenticatedLayout from './components/unauthenticatedLayout/UnauthenticatedLayout';
import Login from './pages/login/Login';
import SignUp from './pages/signUp/SignUp';
import { ChangePassword } from './pages/changePassword/ChangePassword';
import siteTitle from './constants/siteTitle';

function App() {
  return (
    <>
      <Helmet>
        <title>{siteTitle}</title>
      </Helmet>
      <Switch>
        <Route
          exact
          path={[routeUrls.home, routeUrls.editMeasurement, routeUrls.createMeasurement, routeUrls.changePassword]}
        >
          <AuthenticatedLayout>
            <ProtectedRoute path={routeUrls.home} exact component={BodyMeasurementsPage} />
            <ProtectedRoute path={routeUrls.editMeasurement} component={CreateOrEditMeasurementPage} />
            <ProtectedRoute path={routeUrls.createMeasurement} component={CreateOrEditMeasurementPage} />
            <ProtectedRoute path={routeUrls.changePassword} component={ChangePassword} />
          </AuthenticatedLayout>
        </Route>

        <Route exact path={[routeUrls.login, routeUrls.signUp, routeUrls.about]}>
          <UnauthenticatedLayout>
            <Route path={routeUrls.login} exact component={Login} />
            <Route path={routeUrls.signUp} exact component={SignUp} />
            <Route path={routeUrls.about} exact component={About} />
          </UnauthenticatedLayout>
        </Route>

        <Route render={() => <NotFound />} />
      </Switch>
    </>
  );
}

export default App;

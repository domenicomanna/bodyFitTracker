import React, { useEffect, useState } from 'react';
import { Switch, Route } from 'react-router-dom';
import ProtectedRoute from './protectedRoute/ProtectedRoute';
import Layout from './authenticatedLayout/Layout';
import bodyMeasurementsClient from '../api/bodyMeasurementsClient';
import BodyMeasurementsPage from '../pages/bodyMeasurementsPage/BodyMeasurementsPage';
import CreateOrEditMeasurementPage from '../pages/createOrEditMeasurementPage/CreateOrEditMeasurementPage';
import routeUrls from '../constants/routeUrls';
import NotFound from '../pages/notFound/NotFound';

bodyMeasurementsClient.getAllMeasurements().then((res) => console.log(res));
const AuthenticatedApp = () => {
  return (
    <Switch>
      <Route exact path={[routeUrls.home, routeUrls.editMeasurement, routeUrls.createMeasurement]}>
        <Layout>
          <ProtectedRoute path={routeUrls.home} exact component={BodyMeasurementsPage} />
          <ProtectedRoute path={routeUrls.editMeasurement} component={CreateOrEditMeasurementPage} />
          <ProtectedRoute path={routeUrls.createMeasurement} component={CreateOrEditMeasurementPage} />
        </Layout>
      </Route>
      <Route path={routeUrls.notFound} component={NotFound}/>
      <Route render={() => <NotFound/>} />
    </Switch>
  );
};

export default AuthenticatedApp;

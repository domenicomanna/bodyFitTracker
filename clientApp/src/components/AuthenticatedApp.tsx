import React, { useEffect, useState } from 'react';
import { Switch, Route } from 'react-router-dom';
import ProtectedRoute from './protectedRoute/ProtectedRoute';
import Layout from './layout/Layout';
import bodyMeasurementsClient from '../api/bodyMeasurementsClient';
import BodyMeasurementsPage from '../pages/bodyMeasurementsPage/BodyMeasurementsPage';
import CreateOrEditMeasurementPage from '../pages/createOrEditMeasurementPage/CreateOrEditMeasurementPage';
import routeUrls from '../constants/routeUrls';

bodyMeasurementsClient.getAllMeasurements().then((res) => console.log(res));
const AuthenticatedApp = () => {
  return (
    <Layout>
      <Switch>
        <ProtectedRoute path={routeUrls.home} exact component={BodyMeasurementsPage} />
        <ProtectedRoute path={routeUrls.editMeasurement} exact component = {CreateOrEditMeasurementPage} />
        <ProtectedRoute path={routeUrls.createMeasurement} exact component = {CreateOrEditMeasurementPage} />
      </Switch>
    </Layout>
  );
};

export default AuthenticatedApp;

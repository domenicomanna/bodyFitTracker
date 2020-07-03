import React, { useEffect, useState } from 'react';
import { Switch, Route } from 'react-router-dom';
import ProtectedRoute from './protectedRoute/ProtectedRoute';
import Layout from './layout/Layout';

import bodyMeasurementsClient from '../api/bodyMeasurementsClient';
import BodyMeasurementsPage from '../pages/bodyMeasurementsPage/BodyMeasurementsPage';
import CreateOrEditMeasurementPage from '../pages/createOrEditMeasurementPage/CreateOrEditMeasurementPage';

bodyMeasurementsClient.getAllMeasurements().then((res) => console.log(res));

const AuthenticatedApp = () => {
  return (
    <Layout>
      <Switch>
        {/* <ProtectedRoute path='/' exact component={BodyMeasurementsPage} /> */}
        <ProtectedRoute path='/edit-measurement/:measurementIdToEdit(\d+)' exact component = {CreateOrEditMeasurementPage} />
        {/* <ProtectedRoute path='/create-measurement/' exact component = {CreateOrEditMeasurementPage} /> */}
      </Switch>
    </Layout>
  );
};

export default AuthenticatedApp;

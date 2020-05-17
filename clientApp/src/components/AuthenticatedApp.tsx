import React, { useEffect, useState } from 'react';
import { Switch, Route } from 'react-router-dom';
import ProtectedRoute from './ProtectedRoute/ProtectedRoute';
import Layout from './layout/Layout';

import bodyMeasurementsClient from '../api/bodyMeasurementsClient';
import BodyMeasurementsPage from '../pages/BodyMeasurementsPage/BodyMeasurementsPage';

bodyMeasurementsClient.getAllMeasurements().then((res) => console.log(res));

const AuthenticatedApp = () => {
  return (
    <Layout>
      <Switch>
        <ProtectedRoute path='/' exact component={BodyMeasurementsPage} />
      </Switch>
    </Layout>
  );
};

export default AuthenticatedApp;

import React from 'react';
import { Layout } from './Layout/Layout';

import bodyMeasurementsClient from '../api/bodyMeasurementsClient';

bodyMeasurementsClient.getAllMeasurements().then(res => console.log(res));

const AuthenticatedApp = () => {
  return (
    <Layout>
      You are authenticated.................................................
    </Layout>
  );
};

export default AuthenticatedApp;

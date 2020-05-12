import React, { useEffect, useState } from 'react';
import Layout from './layout/Layout';

import bodyMeasurementsClient from '../api/bodyMeasurementsClient';
import BodyMeasurementsPage from '../containers/BodyMeasurementsPage/BodyMeasurementsPage';


bodyMeasurementsClient.getAllMeasurements().then((res) => console.log(res));

const AuthenticatedApp = () => {

  return <Layout> <BodyMeasurementsPage/> </Layout>;
};

export default AuthenticatedApp;

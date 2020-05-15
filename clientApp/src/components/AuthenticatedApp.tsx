import React, { useEffect, useState } from 'react';
import Layout from './layout/Layout';
import { Switch, Route } from "react-router-dom";

import bodyMeasurementsClient from '../api/bodyMeasurementsClient';
import BodyMeasurementsPage from '../containers/BodyMeasurementsPage/BodyMeasurementsPage';
import About from './About';


bodyMeasurementsClient.getAllMeasurements().then((res) => console.log(res));

const AuthenticatedApp = () => {

  return (
    <Layout> 
      <Switch>
        <Route path='/' exact component={BodyMeasurementsPage}/>
        <Route path='/about' component={About}/>
      </Switch>
    </Layout>
  );
};

export default AuthenticatedApp;

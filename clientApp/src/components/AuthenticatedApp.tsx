import React, { useEffect, useState } from 'react';
import Layout from './layout/Layout';

import bodyMeasurementsClient from '../api/bodyMeasurementsClient';
import BodyMeasurementList from './bodyMeasurementList/BodyMeasurementList';
import { BodyMeasurementCollectionModel } from '../models/bodyMeasurementModels';

bodyMeasurementsClient.getAllMeasurements().then((res) => console.log(res));

const AuthenticatedApp = () => {
  const [isLoading, setLoading] = useState(true)
  const [bodyMeasurementCollection, setBodyMeasurementCollection] = useState<BodyMeasurementCollectionModel>();

  useEffect(() => {
    bodyMeasurementsClient
      .getAllMeasurements()
      .then((bodyMeasurementCollection) => {
        setBodyMeasurementCollection(bodyMeasurementCollection) 
        setLoading(false)
      });
  });

  let contentToRender;
  if (isLoading) contentToRender = <p>Loading...</p>
  else contentToRender = <BodyMeasurementList bodyMeasurementCollection = {bodyMeasurementCollection!}/>
  return <Layout> {contentToRender} </Layout>;
};

export default AuthenticatedApp;

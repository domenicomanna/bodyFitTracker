import React, { useState, useEffect, useContext } from 'react';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import BodyMeasurementList from '../../components/bodyMeasurementList/BodyMeasurementList';
import { BodyMeasurementCollectionModel } from '../../models/bodyMeasurementModels';
import UnauthenticatedApp from '../../components/UnauthenticatedApp';

const BodyMeasurementsPage = () => {
  const [isLoading, setLoading] = useState(true);
  const [bodyMeasurementCollection, setBodyMeasurementCollection] = useState<BodyMeasurementCollectionModel>();

  useEffect( () => {
    bodyMeasurementsClient.getAllMeasurements().then((bodyMeasurementCollection) => {
      setBodyMeasurementCollection(bodyMeasurementCollection);
      setLoading(false);
    });
  }, []);

  if (isLoading) return <p>Loading...</p>;

  return <BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection!} />;
};

export default BodyMeasurementsPage;

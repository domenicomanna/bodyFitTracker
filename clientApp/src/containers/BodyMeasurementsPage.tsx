import React, { useState, useEffect, useContext } from 'react';
import bodyMeasurementsClient from '../api/bodyMeasurementsClient';
import BodyMeasurementList from '../components/bodyMeasurementList/BodyMeasurementList';
import { BodyMeasurementCollectionModel } from '../models/bodyMeasurementModels';
import { UserContext } from '../contexts/UserContext';
import UnauthenticatedApp from '../components/UnauthenticatedApp';

const BodyMeasurementsPage = () => {
  const userContext = useContext(UserContext);

  const [isLoading, setLoading] = useState(true);
  const [bodyMeasurementCollection, setBodyMeasurementCollection] = useState<BodyMeasurementCollectionModel>();

  useEffect(() => {
    if (!userContext.isAuthenticated()) return;
    bodyMeasurementsClient.getAllMeasurements().then((bodyMeasurementCollection) => {
      setBodyMeasurementCollection(bodyMeasurementCollection);
      setLoading(false);
    });
  }, []);

  if (!userContext.isAuthenticated()) return <UnauthenticatedApp />;

  if (isLoading) return <p>Loading...</p>;

  return <BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection!} />;

};

export default BodyMeasurementsPage;

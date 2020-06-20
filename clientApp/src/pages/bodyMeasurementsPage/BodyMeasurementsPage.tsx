import React, { useState, useEffect, useContext } from 'react';
import update from 'immutability-helper';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import BodyMeasurementList from '../../components/bodyMeasurementList/BodyMeasurementList';
import { BodyMeasurementCollectionModel } from '../../models/bodyMeasurementModels';

const BodyMeasurementsPage = () => {
  const [isLoading, setLoading] = useState(true);
  const [bodyMeasurementCollection, setBodyMeasurementCollection] = useState<BodyMeasurementCollectionModel>();

  useEffect(() => {
    bodyMeasurementsClient.getAllMeasurements().then((bodyMeasurementCollection) => {
      setBodyMeasurementCollection(bodyMeasurementCollection);
      setLoading(false);
    });
  }, []);

  const deleteMeasurement = async (bodyMeasurementId: number) => {
    const measurementIndexToDelete = bodyMeasurementCollection!.bodyMeasurements.findIndex(
      (b) => b.bodyMeasurementId === bodyMeasurementId
    );
    await bodyMeasurementsClient.deleteMeasurement(bodyMeasurementId);
    const bodyMeasuremenetCollectionWithMeasurementRemoved = update(bodyMeasurementCollection, {
      bodyMeasurements: { $splice: [[measurementIndexToDelete, 1]] },
    });
    setBodyMeasurementCollection(bodyMeasuremenetCollectionWithMeasurementRemoved);
  };

  if (isLoading) return <p>Loading...</p>;

  return (
    <BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection!} deleteMeasurement={deleteMeasurement} />
  );
};

export default BodyMeasurementsPage;

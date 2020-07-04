import React, { useState, useEffect, useContext } from 'react';
import { useHistory } from 'react-router-dom';
import update from 'immutability-helper';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import BodyMeasurementList from '../../components/bodyMeasurementList/BodyMeasurementList';
import { BodyMeasurementModel } from '../../models/bodyMeasurementModels';
import routeUrls from '../../constants/routeUrls';

const BodyMeasurementsPage = () => {
  const history = useHistory();
  const [isLoading, setLoading] = useState(true);
  const [bodyMeasurements, setBodyMeasurements] = useState<BodyMeasurementModel[]>();

  useEffect(() => {
    bodyMeasurementsClient.getAllMeasurements().then((bodyMeasurements) => {
      setBodyMeasurements(bodyMeasurements);
      setLoading(false);
    });
  }, []);

  const editMeasurement = (bodyMeasurementId: number) => {
    history.push(`${routeUrls.editMeasurementWithoutRouteParameter}/${bodyMeasurementId}`);
  };
  const deleteMeasurement = async (bodyMeasurementId: number) => {
    const measurementIndexToDelete = bodyMeasurements!.findIndex((b) => b.bodyMeasurementId === bodyMeasurementId);
    await bodyMeasurementsClient.deleteMeasurement(bodyMeasurementId);
    const bodyMeasurementsWithMeasurementRemoved = update(bodyMeasurements, {
      $splice: [[measurementIndexToDelete, 1]],
    });
    setBodyMeasurements(bodyMeasurementsWithMeasurementRemoved);
  };

  if (isLoading) return <p>Loading...</p>;

  return (
    <>
      <BodyMeasurementList
        bodyMeasurements={bodyMeasurements!}
        editMeasurement={editMeasurement}
        deleteMeasurement={deleteMeasurement}
      />
    </>
  );
};

export default BodyMeasurementsPage;

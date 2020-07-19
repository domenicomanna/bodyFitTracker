import React, { useState, useEffect } from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import update from 'immutability-helper';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import BodyMeasurementList from '../../components/bodyMeasurementList/BodyMeasurementList';
import { BodyMeasurementType } from '../../types/bodyMeasurementTypes';
import routeUrls from '../../constants/routeUrls';
import { toast } from 'react-toastify';

export type LocationState = {
  measurementWasCreated: boolean;
  measurementWasEdited: boolean;
};

const BodyMeasurementsPage = () => {
  const location = useLocation<LocationState>();
  const history = useHistory();
  const [isLoading, setLoading] = useState(true);
  const [bodyMeasurements, setBodyMeasurements] = useState<BodyMeasurementType[]>();

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
    toast.success('Measurement removed!');
  };

  if (location.state && location.state.measurementWasCreated) toast.success('Measurement created!');
  else if (location.state && location.state.measurementWasEdited) toast.success('Measurement edited!');
  if (location.state) {
    // prevent toast message from showing multiple times by clearing the state
    history.replace(location.pathname, undefined);
  }

  if (isLoading) return <p>Loading...</p>;

  return (
    <BodyMeasurementList
      bodyMeasurements={bodyMeasurements!}
      editMeasurement={editMeasurement}
      deleteMeasurement={deleteMeasurement}
    />
  );
};

export default BodyMeasurementsPage;

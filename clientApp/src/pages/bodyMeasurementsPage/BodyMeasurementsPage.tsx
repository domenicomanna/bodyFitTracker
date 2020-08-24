import React, { useState, useEffect } from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import update from 'immutability-helper';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import BodyMeasurementList from '../../components/bodyMeasurementList/BodyMeasurementList';
import { BodyMeasurementType } from '../../types/bodyMeasurementTypes';
import routeUrls from '../../constants/routeUrls';
import { toast } from 'react-toastify';
import PageTitle from '../../components/pageTitle/PageTitle';
import { PageLoader } from '../../components/ui/pageLoader/PageLoader';
import { Helmet } from 'react-helmet';
import brandName from '../../constants/brandName';

export type BodyMeasurementsPageLocationState = {
  measurementWasCreated?: boolean;
  measurementWasEdited?: boolean;
  profileSettingsUpdated?: boolean;
};

const BodyMeasurementsPage = () => {
  const location = useLocation<BodyMeasurementsPageLocationState>();
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
  if (location.state) {
    if (location.state.measurementWasCreated) toast.success('Measurement created!');
    else if (location.state.measurementWasEdited) toast.success('Measurement edited!');
    else if (location.state.profileSettingsUpdated) toast.success('Settings updated!');

    history.replace(location.pathname, undefined);
  }

  return (
    <>
      <Helmet>
        <title>{brandName} | Measurements</title>
      </Helmet>
      <PageTitle>Measurements</PageTitle>
      {isLoading ? (
        <PageLoader testId='pageLoader' />
      ) : (
        <BodyMeasurementList
          bodyMeasurements={bodyMeasurements!}
          editMeasurement={editMeasurement}
          deleteMeasurement={deleteMeasurement}
        />
      )}
    </>
  );
};

export default BodyMeasurementsPage;

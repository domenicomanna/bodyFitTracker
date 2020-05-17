import React, { FunctionComponent, Fragment } from 'react';
import { BodyMeasurementCollectionModel, BodyMeasurementModel } from '../../models/bodyMeasurementModels';
import BodyMeasurement from '../bodyMeasurement/BodyMeasurement';
import { Gender } from '../../models/gender';
import styles from './BodyMeasurementList.module.css';
import PageTitle from '../PageTitle/PageTitle';

type Props = {
  bodyMeasurementCollection: BodyMeasurementCollectionModel;
  deleteMeasurement: (bodyMeasurementId: number) => void;
};

const BodyMeasurementList: FunctionComponent<Props> = ({
  bodyMeasurementCollection,
  deleteMeasurement
}) => {
  const { length, weight } = bodyMeasurementCollection;
  const hipCircumferenceDataShouldBeRendered: boolean = bodyMeasurementCollection.genderTypeName == Gender.Female;
  const hipCircumferenceRow = hipCircumferenceDataShouldBeRendered ? (
    <th>Hip Circumference ({length.abbreviation})</th>
  ) : null;

  const transformedBodyMeasurements = bodyMeasurementCollection.bodyMeasurements.map((bodyMeasurement) => (
    <BodyMeasurement
      key={bodyMeasurement.bodyMeasurementId}
      measurement={bodyMeasurement}
      hipCircumferenceDataShouldBeRendered={hipCircumferenceDataShouldBeRendered}
      deleteMeasurement={deleteMeasurement}
    />
  ));

  let contentToRender = (
    <>
      <PageTitle>Measurements</PageTitle>
      <div className={styles.measurementsWrapper}>
        <table className={styles.measurements}>
          <thead>
            <tr>
              <th>Date Added</th>
              <th>Neck Circumference ({length.abbreviation})</th>
              <th>Waist Circumference ({length.abbreviation})</th>
              {hipCircumferenceRow}
              <th>Weight ({weight.abbreviation})</th>
              <th>Body fat (%)</th>
              <th></th>
            </tr>
          </thead>
          <tbody data-testid='measurements'>{transformedBodyMeasurements}</tbody>
        </table>
      </div>
    </>
  );

  if (bodyMeasurementCollection.bodyMeasurements.length === 0) {
    contentToRender = <p>You do not have any body measurements. Create one now!</p>;
  }

  return contentToRender;
};

export default BodyMeasurementList;

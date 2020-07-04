import React, { FunctionComponent, useContext } from 'react';
import { BodyMeasurementModel } from '../../models/bodyMeasurementModels';
import BodyMeasurement from '../bodyMeasurement/BodyMeasurement';
import { Gender } from '../../models/userModels';
import styles from './bodyMeasurementList.module.css';
import PageTitle from '../pageTitle/PageTitle';
import { UserContext } from '../../contexts/UserContext';

type Props = {
  bodyMeasurements: BodyMeasurementModel[];
  deleteMeasurement: (bodyMeasurementId: number) => void;
  editMeasurement: (bodyMeasurementId: number) => void;
};

const BodyMeasurementList: FunctionComponent<Props> = ({ bodyMeasurements, editMeasurement, deleteMeasurement }) => {
  const { gender, measurementPreference } = useContext(UserContext);
  const { lengthUnit, weightUnit } = measurementPreference;
  const hipCircumferenceDataShouldBeRendered: boolean = gender === Gender.Female;
  const hipCircumferenceRow = hipCircumferenceDataShouldBeRendered ? <th>Hip Circumference ({lengthUnit})</th> : null;

  const transformedBodyMeasurements = bodyMeasurements.map((bodyMeasurement) => (
    <BodyMeasurement
      key={bodyMeasurement.bodyMeasurementId}
      measurement={bodyMeasurement}
      hipCircumferenceDataShouldBeRendered={hipCircumferenceDataShouldBeRendered}
      deleteMeasurement={deleteMeasurement}
      editMeasurement={editMeasurement}
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
              <th>Neck Circumference ({lengthUnit})</th>
              <th>Waist Circumference ({lengthUnit})</th>
              {hipCircumferenceRow}
              <th>Weight ({weightUnit})</th>
              <th>Body fat (%)</th>
              {/* empty headers for edit and delete icons */}
              <th></th>
              <th></th>
            </tr>
          </thead>
          <tbody data-testid='measurements'>{transformedBodyMeasurements}</tbody>
        </table>
      </div>
    </>
  );

  if (bodyMeasurements.length === 0) {
    contentToRender = <p>You do not have any body measurements. Create one now!</p>;
  }

  return contentToRender;
};

export default BodyMeasurementList;

import React, { FunctionComponent, useContext } from 'react';
import { BodyMeasurementType } from '../../types/bodyMeasurementTypes';
import BodyMeasurement from '../bodyMeasurement/BodyMeasurement';
import { Gender } from '../../types/userTypes';
import styles from './bodyMeasurementList.module.css';
import { UserContext } from '../../contexts/UserContext';
import { NavLink } from 'react-router-dom';
import routeUrls from '../../constants/routeUrls';

type Props = {
  bodyMeasurements: BodyMeasurementType[];
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
    <div className={styles.measurementsWrapper}>
      <table className={styles.measurements}>
        <thead>
          <tr>
            <th style={{width:"100px"}}>Date Added</th>
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
  );

  if (bodyMeasurements.length === 0) {
    contentToRender = (
      <p style={{textAlign: "center"}}>
        You currently do not have any measurements. <NavLink to={routeUrls.createMeasurement}>Create one now</NavLink>
      </p>
    );
  }

  return contentToRender;
};

export default BodyMeasurementList;

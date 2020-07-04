import React, { FunctionComponent } from 'react';
import { BodyMeasurementModel } from '../../models/bodyMeasurementModels';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Button from '../ui/button/Button';
import styles from './bodyMeasurement.module.css';

type Props = {
  measurement: BodyMeasurementModel;
  hipCircumferenceDataShouldBeRendered: boolean;
  deleteMeasurement: (bodyMeasurementId: number) => void;
  editMeasurement: (bodyMeasurementId: number) => void;
};

const BodyMeasurement: FunctionComponent<Props> = ({
  measurement,
  hipCircumferenceDataShouldBeRendered,
  editMeasurement,
  deleteMeasurement,
}) => {
  const hipCircumferenceData = hipCircumferenceDataShouldBeRendered ? <td>{measurement.hipCircumference}</td> : null;

  return (
    <tr>
      <td>{measurement.dateAdded.toString()}</td>
      <td>{measurement.neckCircumference}</td>
      <td>{measurement.waistCircumference}</td>
      {hipCircumferenceData}
      <td>{measurement.weight}</td>
      <td>{measurement.bodyFatPercentage}</td>
      <td onClick={() => editMeasurement(measurement.bodyMeasurementId)}>
        <Button type='button' buttonClass='icon'>
          <FontAwesomeIcon icon='pencil-alt' className={styles.editMeasurement} />
        </Button>
      </td>
      <td data-testid='delete-measurement' onClick={() => deleteMeasurement(measurement.bodyMeasurementId)}>
        <Button type='button' buttonClass='icon'>
          <FontAwesomeIcon icon='trash-alt' className={styles.deleteMeasurement} />
        </Button> 
      </td>
    </tr>
  );
};

export default BodyMeasurement;

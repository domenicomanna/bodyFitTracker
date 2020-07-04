import React, { FunctionComponent } from 'react';
import { BodyMeasurementModel } from '../../models/bodyMeasurementModels';

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
      <td data-testid='edit-measurement' onClick={() => editMeasurement(measurement.bodyMeasurementId)}>Edit</td>
      <td data-testid='delete-measurement' onClick={() => deleteMeasurement(measurement.bodyMeasurementId)}>
        Delete
      </td>
    </tr>
  );
};

export default BodyMeasurement;

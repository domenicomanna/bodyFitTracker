import React, { FunctionComponent } from 'react';
import { BodyMeasurementModel } from '../../models/bodyMeasurementModels';

type Props = {
  measurement: BodyMeasurementModel;
  hipCircumferenceDataShouldBeRendered: boolean;
};

const BodyMeasurement: FunctionComponent<Props> = ({ measurement, hipCircumferenceDataShouldBeRendered }) => {

  const hipCircumferenceData = hipCircumferenceDataShouldBeRendered ? <td>{measurement.hipCircumference}</td> : null;

  return (
    <tr>
      <td>{measurement.dateAdded.toString()}</td>
      <td>{measurement.neckCircumference}</td>
      <td>{measurement.waistCircumference}</td>
      {hipCircumferenceData}
      <td>{measurement.weight}</td>
      <td>{measurement.bodyFatPercentage}</td>
    </tr>
  );
};

export default BodyMeasurement;

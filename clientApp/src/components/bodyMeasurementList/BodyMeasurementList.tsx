import React, { FunctionComponent } from 'react';
import { BodyMeasurementCollectionModel, BodyMeasurementModel } from '../../models/bodyMeasurementModels';
import BodyMeasurement from '../bodyMeasurement/BodyMeasurement';
import { Gender } from '../../models/gender';

type Props = {
  bodyMeasurementCollection: BodyMeasurementCollectionModel;
};

const BodyMeasurementList: FunctionComponent<Props> = ({ bodyMeasurementCollection }) => {
  const { length, weight } = bodyMeasurementCollection;
  const hipCircumferenceDataShouldBeRendered: boolean = bodyMeasurementCollection.genderTypeName == Gender.Female;
  const hipCircumferenceRow = hipCircumferenceDataShouldBeRendered ? (
    <th>Hip Circumference ({length.abbreviation})</th>
  ) : null;

  let contentToRender = (
    <table>
      <thead>
        <tr>
          <th>Date Added</th>
          <th>Neck Circumference ({length.abbreviation})</th>
          <th>Waist Circumference ({length.abbreviation})</th>
          {hipCircumferenceRow}
          <th>Weight ({weight.abbreviation})</th>
          <th>Body fat (%)</th>
        </tr>
      </thead>
      <tbody data-testid='measurements'>
        {transformBodyMeasurements(bodyMeasurementCollection.bodyMeasurements, hipCircumferenceDataShouldBeRendered)}
      </tbody>
    </table>
  );

  if (bodyMeasurementCollection.bodyMeasurements.length === 0) {
    contentToRender = <p>You do not have any body measurements. Create one now!</p>;
  }

  return contentToRender;
};

const transformBodyMeasurements = (
  bodyMeasurements: BodyMeasurementModel[],
  hipCircumferenceDataShouldBeRendered: boolean
) => {
  return bodyMeasurements.map((bodyMeasurement) => (
    <BodyMeasurement
      key={bodyMeasurement.bodyMeasurementId}
      measurement={bodyMeasurement}
      hipCircumferenceDataShouldBeRendered={hipCircumferenceDataShouldBeRendered}
    />
  ));
};

export default BodyMeasurementList;

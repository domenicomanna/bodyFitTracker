import React from 'react';
import { render, screen, getByTestId } from '@testing-library/react';
import BodyMeasurementList from './BodyMeasurementList';
import { BodyMeasurementCollectionModel, BodyMeasurementModel } from '../../models/bodyMeasurementModels';
import { Gender } from '../../models/gender';

let bodyMeasurementListProps: React.ComponentProps<typeof BodyMeasurementList>;

beforeEach(() => {
  const bodyMeasurementCollection = {
    measurementSystemName: '',
    genderTypeName: Gender.Male,
    length: {
      name: '',
      abbreviation: '',
    },
    weight: {
      name: '',
      abbreviation: '',
    },
    bodyMeasurements: [
      {
        bodyMeasurementId: 1,
        bodyFatPercentage: 10,
        neckCircumference: 10,
        waistCircumference: 10,
        hipCircumference: 10,
        weight: 10,
        dateAdded: new Date(2019, 9, 12),
      },
    ],
  };
  bodyMeasurementListProps = {
    bodyMeasurementCollection : bodyMeasurementCollection,
    deleteMeasurement:  () => { }
  }
});

describe('Component when measurements are not provided', () => {
  it('should render a message indicating the user has no body measurements', () => {
    bodyMeasurementListProps.bodyMeasurementCollection.bodyMeasurements = [];
    render(<BodyMeasurementList {...bodyMeasurementListProps} />);
    const messageElement = screen.getByText(/You do not have any body measurements/i);
    expect(messageElement).toBeTruthy;
  });
});

describe('Component when measurements are provided', () => {
  it("should render the user's body measurements", () => {
    render(<BodyMeasurementList {...bodyMeasurementListProps} />);
    const measurementsElement = screen.getByTestId(/measurements/i);
    expect(measurementsElement).toBeTruthy;
  });

  it('should render hip circumference if gender type is female', () => {
    bodyMeasurementListProps.bodyMeasurementCollection.genderTypeName = Gender.Female;
    render(<BodyMeasurementList {...bodyMeasurementListProps} />);
    const hipCircumferenceElement = screen.getByText(/hip circumference/i);
    expect(hipCircumferenceElement).toBeTruthy();
  });

  it('should not render hip circumference if gender type is male', () => {
    bodyMeasurementListProps.bodyMeasurementCollection.genderTypeName = Gender.Male;
    render(<BodyMeasurementList {...bodyMeasurementListProps} />);
    const hipCircumferenceElement = screen.queryByText(/hip circumference/i);
    expect(hipCircumferenceElement).toBeFalsy();
  });
});

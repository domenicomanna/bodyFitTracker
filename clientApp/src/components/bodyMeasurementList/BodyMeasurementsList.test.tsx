import React from 'react';
import { render, screen, getByTestId } from '@testing-library/react';
import BodyMeasurementList from './BodyMeasurementList';
import { BodyMeasurementCollectionModel, BodyMeasurementModel } from '../../models/bodyMeasurementModels';

let bodyMeasurementCollection: BodyMeasurementCollectionModel;

beforeEach(() => {
  bodyMeasurementCollection = {
    measurementSystemName: '',
    genderTypeName: '',
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
});

describe('Component when measurements are not provided', () => {
  it('should render a message indicating the user has no body measurements if no body measurements are provided', () => {
    bodyMeasurementCollection.bodyMeasurements = [];
    render(<BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection} />);
    const messageElement = screen.getByText(/You do not have any body measurements/i);
    expect(messageElement).toBeTruthy;
  });
});

describe('Component when measurements are provided', () => {
  it("should render the user's body measurements", () => {
    render(<BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection} />);
    const measurementsElement = screen.getByTestId(/measurements/i);
    expect(measurementsElement).toBeTruthy;
  });

  it('should render hip circumference if gender type is female', () => {
    bodyMeasurementCollection.genderTypeName = 'Female';
    render(<BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection} />);
    const hipCircumferenceElement = screen.getByText(/hip circumference/i);
    expect(hipCircumferenceElement).toBeTruthy();
  });

  it('should not render hip circumference if gender type is male', () => {
    bodyMeasurementCollection.genderTypeName = 'male';
    render(<BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection} />);
    const hipCircumferenceElement = screen.queryByText(/hip circumference/i);
    expect(hipCircumferenceElement).toBeFalsy();
  });
});

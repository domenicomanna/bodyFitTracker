import React from 'react';
import { render, screen } from '@testing-library/react';
import { BodyMeasurementModel } from '../../models/bodyMeasurementModels';
import BodyMeasurement from '../bodyMeasurement/BodyMeasurement';

let bodyMeasurementModel: BodyMeasurementModel;
let tableWithBody: HTMLElement;
const hipCircumference = 23.23432334324; // make it very precise so we don't accidentally retrieve another value when querying for this

beforeEach(() => {
  bodyMeasurementModel = {
    bodyMeasurementId: 1,
    neckCircumference: 10000,
    waistCircumference: 1000,
    hipCircumference: hipCircumference,
    bodyFatPercentage: 1000,
    weight: 1000,
    dateAdded: new Date(2019, 11, 24),
  };
  const table = document.createElement('table');
  const tableBody = document.createElement('tbody');
  tableWithBody = table.appendChild(tableBody);
});

it('should not render hip circumference if the hip circumference render property is false', () => {
  render(<BodyMeasurement measurement={bodyMeasurementModel} hipCircumferenceDataShouldBeRendered={false} />, {
    container: document.body.appendChild(tableWithBody),
  });
  const hipCircumferenceElement = screen.queryByText(hipCircumference.toString());
  expect(hipCircumferenceElement).toBeFalsy;
});

it('should render hip circumference if the hip circumference render property is true', () => {
  render(<BodyMeasurement measurement={bodyMeasurementModel} hipCircumferenceDataShouldBeRendered />, {
    container: document.body.appendChild(tableWithBody),
  });
  const hipCircumferenceElement = screen.getByText(hipCircumference.toString());
  expect(hipCircumferenceElement).toBeTruthy();
});

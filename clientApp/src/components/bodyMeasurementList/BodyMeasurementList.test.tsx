import React from 'react';
import { render, screen } from '@testing-library/react';
import BodyMeasurementList from './BodyMeasurementList';
import { BodyMeasurementModel } from '../../models/bodyMeasurementModels';
import { Gender, UserContextModel } from '../../models/userModels';
import { UserContext } from '../../contexts/UserContext';
import { defaultUserContextModel } from '../../testHelpers/testData';

let bodyMeasurementListProps: React.ComponentProps<typeof BodyMeasurementList>;
let userContextModel: UserContextModel;

beforeEach(() => {
  const bodyMeasurements: BodyMeasurementModel[] = [
    {
      bodyMeasurementId: 1,
      bodyFatPercentage: 10,
      neckCircumference: 10,
      waistCircumference: 10,
      hipCircumference: 10,
      weight: 10,
      dateAdded: new Date(2019, 9, 12),
    },
  ];
  bodyMeasurementListProps = {
    bodyMeasurements: bodyMeasurements,
    deleteMeasurement: () => {},
    editMeasurement: () => {},
  };
  userContextModel = defaultUserContextModel
});

const handleRendering = (gender: Gender = Gender.Male) => {
  userContextModel.gender = gender;
  return render(
    <UserContext.Provider value={userContextModel}>
      <BodyMeasurementList {...bodyMeasurementListProps} />
    </UserContext.Provider>
  );
};

describe('Component when measurements are not provided', () => {
  it('should render a message indicating the user has no body measurements', () => {
    bodyMeasurementListProps.bodyMeasurements = [];
    handleRendering();
    const messageElement = screen.getByText(/You do not have any body measurements/i);
    expect(messageElement).toBeTruthy();
  });
});

describe('Component when measurements are provided', () => {
  it("should render the user's body measurements", () => {
    handleRendering();
    const measurementsElement = screen.getByTestId(/measurements/i);
    expect(measurementsElement).toBeTruthy();
  });

  it('should render hip circumference if gender type is female', () => {
    handleRendering(Gender.Female);
    const hipCircumferenceElement = screen.getByText(/hip circumference/i);
    expect(hipCircumferenceElement).toBeTruthy();
  });

  it('should not render hip circumference if gender type is male', () => {
    handleRendering(Gender.Male);
    const hipCircumferenceElement = screen.queryByText(/hip circumference/i);
    expect(hipCircumferenceElement).toBeFalsy();
  });
});

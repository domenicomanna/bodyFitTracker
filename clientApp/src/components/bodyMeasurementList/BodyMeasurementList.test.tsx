import React from 'react';
import { render, screen } from '@testing-library/react';
import BodyMeasurementList from './BodyMeasurementList';
import { BodyMeasurementType } from '../../types/bodyMeasurementTypes';
import { Gender, UserContextType } from '../../types/userTypes';
import { UserContext } from '../../contexts/UserContext';
import { defaultUserContextType } from '../../testHelpers/testData';

let bodyMeasurementListProps: React.ComponentProps<typeof BodyMeasurementList>;
let userContextType: UserContextType;

beforeEach(() => {
  const bodyMeasurements: BodyMeasurementType[] = [
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
  userContextType = defaultUserContextType
});

const handleRendering = (gender: Gender = Gender.Male) => {
  userContextType.gender = gender;
  return render(
    <UserContext.Provider value={userContextType}>
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

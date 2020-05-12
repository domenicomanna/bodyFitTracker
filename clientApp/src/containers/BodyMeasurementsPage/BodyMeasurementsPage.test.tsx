import React from 'react';
import { render, screen, waitForElementToBeRemoved, waitFor } from '@testing-library/react';
import { mocked } from 'ts-jest/utils';
import { UserContext, UserContextModel } from '../../contexts/UserContext';
import BodyMeasurementsPage from './BodyMeasurementsPage';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import { BodyMeasurementCollectionModel } from '../../models/bodyMeasurementModels';
jest.mock('../../api/bodyMeasurementsClient');

let mockedBodyMeasurementsClient = mocked(bodyMeasurementsClient, true);
let userContextModel: UserContextModel;
let bodyMeasurementCollection: BodyMeasurementCollectionModel;

beforeEach(() => {
  userContextModel = {
    isAuthenticated: () => false,
    token: '',
  };
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
      {
        bodyMeasurementId: 2,
        bodyFatPercentage: 10,
        neckCircumference: 10,
        waistCircumference: 10,
        hipCircumference: 10,
        weight: 10,
        dateAdded: new Date(2019, 9, 12),
      },
    ],
  };
  mockedBodyMeasurementsClient.getAllMeasurements.mockReset();
});

describe('Component when the user is not authenticated', () => {
  it('should render a login message', () => {
    userContextModel.isAuthenticated = () => false;
    render(
      <UserContext.Provider value={userContextModel}>
        <BodyMeasurementsPage />
      </UserContext.Provider>
    );
    const loginElement = screen.getByText(/login/i);
    expect(loginElement).toBeTruthy();
  });
});

describe('Component when the user is authenticated', () => {
  it('should render a loading message when the measurements are being loaded', async () => {
    userContextModel.isAuthenticated = () => true;
    mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurementCollection);
    render(
      <UserContext.Provider value={userContextModel}>
        <BodyMeasurementsPage />
      </UserContext.Provider>
    );
    const loadingElement = screen.getByText(/loading/i);
    expect(loadingElement).toBeTruthy();
    await waitForElementToBeRemoved(() => screen.getByText(/loading/i));
  });

  it('should remove the loading message after the measurements have loaded', async () => {
    userContextModel.isAuthenticated = () => true;
    mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurementCollection);
    render(
      <UserContext.Provider value={userContextModel}>
        <BodyMeasurementsPage />
      </UserContext.Provider>
    );
    await waitFor(() => expect(mockedBodyMeasurementsClient.getAllMeasurements).toHaveBeenCalledTimes(1));
    const loadingElement = screen.queryByText(/Loading/i);
    expect(loadingElement).toBeFalsy();
  });
});

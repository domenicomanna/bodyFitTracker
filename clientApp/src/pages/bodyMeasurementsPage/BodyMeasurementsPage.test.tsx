import React from 'react';
import { render, screen, waitForElementToBeRemoved, waitFor, fireEvent } from '@testing-library/react';
import { mocked } from 'ts-jest/utils';
import { AxiosResponse } from 'axios';
import { UserContext } from '../../contexts/UserContext';
import { UserModel } from '../../models/userModels';
import BodyMeasurementsPage from './BodyMeasurementsPage';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import { BodyMeasurementCollectionModel } from '../../models/bodyMeasurementModels';
import { Gender } from '../../models/userModels';

jest.mock('../../api/bodyMeasurementsClient');

let mockedBodyMeasurementsClient = mocked(bodyMeasurementsClient, true);
let userModel: UserModel;
let bodyMeasurementCollection: BodyMeasurementCollectionModel;
let axiosResponse: AxiosResponse;

const waistCircumference = 23.4323432; // make it very precise so we don't accidentally retrieve another value when querying for this

beforeEach(() => {
  userModel = {
    isAuthenticated: () => false,
    gender : Gender.Female,
    token: '',
  };
  bodyMeasurementCollection = {
    measurementSystemName: '',
    genderTypeName: Gender.Female,
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
        bodyMeasurementId: 2,
        bodyFatPercentage: 10,
        neckCircumference: 10,
        waistCircumference: waistCircumference,
        hipCircumference: 10,
        weight: 10,
        dateAdded: new Date(2019, 9, 12),
      },
    ],
  };
  axiosResponse = {
    data: "",
    status: 200,
    statusText: "OK",
    config: {},
    headers: {}
  }
  mockedBodyMeasurementsClient.getAllMeasurements.mockReset();
});

describe('Component when measurements are loading', () => {
  it('should render a loading message when the measurements are being loaded', async () => {
    userModel.isAuthenticated = () => true;
    mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurementCollection);
    render(
      <UserContext.Provider value={userModel}>
        <BodyMeasurementsPage />
      </UserContext.Provider>
    );
    const loadingElement = screen.getByText(/loading/i);
    expect(loadingElement).toBeTruthy();
    await waitForElementToBeRemoved(() => screen.getByText(/loading/i));
  });
});

describe('Component when measurements have been loaded', () => {
  it('should remove the loading message after the measurements have loaded', async () => {
    userModel.isAuthenticated = () => true;
    mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurementCollection);
    render(
      <UserContext.Provider value={userModel}>
        <BodyMeasurementsPage />
      </UserContext.Provider>
    );
    await waitFor(() => expect(mockedBodyMeasurementsClient.getAllMeasurements).toHaveBeenCalledTimes(1));
    const loadingElement = screen.queryByText(/Loading/i);
    expect(loadingElement).toBeFalsy();
  });

  it('should remove the measurement that the user deletes', async () => {
    userModel.isAuthenticated = () => true;
    mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurementCollection);
    mockedBodyMeasurementsClient.deleteMeasurement.mockResolvedValue(axiosResponse);
    render(
      <UserContext.Provider value={userModel}>
        <BodyMeasurementsPage />
      </UserContext.Provider>
    );
    await waitFor(() => expect(mockedBodyMeasurementsClient.getAllMeasurements).toHaveBeenCalledTimes(1));

    const waistCircumferenceFromMeasurementBeforeDeletion = screen.getByText(waistCircumference.toString());
    expect(waistCircumferenceFromMeasurementBeforeDeletion).toBeTruthy();

    const deleteMeasurementAction = screen.getByTestId('delete-measurement');
    fireEvent.click(deleteMeasurementAction);
    await waitFor(() => expect(mockedBodyMeasurementsClient.deleteMeasurement).toHaveBeenCalledTimes(1));

    const waistCircumferenceFromMeasurementAfterDeletion = screen.queryByText(waistCircumference.toString());
    expect(waistCircumferenceFromMeasurementAfterDeletion).toBeFalsy();
  });

});

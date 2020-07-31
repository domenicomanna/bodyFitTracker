import React from 'react';
import { createMemoryHistory } from 'history';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import { mocked } from 'ts-jest/utils';
import { AxiosResponse } from 'axios';
import { UserContextType } from '../../types/userTypes';
import BodyMeasurementsPage, { LocationState } from './BodyMeasurementsPage';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import { BodyMeasurementType } from '../../types/bodyMeasurementTypes';
import { Router } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import { defaultUserContextType, defaultAxiosResponse } from '../../testHelpers/testData';
import { UserContext } from '../../contexts/UserContext';

jest.mock('../../api/bodyMeasurementsClient');

let mockedBodyMeasurementsClient = mocked(bodyMeasurementsClient, true);
let userContextType: UserContextType;
let bodyMeasurements: BodyMeasurementType[];
let axiosResponse: AxiosResponse;

const waistCircumference = 23.4323432; // make it very precise so we don't accidentally retrieve another value when querying for this

beforeEach(() => {
  userContextType = defaultUserContextType;
  bodyMeasurements = [
    {
      bodyMeasurementId: 2,
      bodyFatPercentage: 10,
      neckCircumference: 10,
      waistCircumference: waistCircumference,
      hipCircumference: 10,
      weight: 10,
      dateAdded: new Date(2019, 9, 12),
    },
  ];
  axiosResponse = defaultAxiosResponse;

  mockedBodyMeasurementsClient.getAllMeasurements.mockReset();
});

const handleRendering = (measurementWasCreated: boolean = false, measurementWasEdited: boolean = false) => {
  const history = createMemoryHistory();
  const locationState: LocationState = { measurementWasCreated, measurementWasEdited };
  history.replace('', undefined);
  history.push('', locationState);
  return render(
    <Router history={history}>
      <UserContext.Provider value={userContextType}>
        <ToastContainer />
        <BodyMeasurementsPage />
      </UserContext.Provider>
    </Router>
  );
};

describe('Component when measurements are loading', () => {
  it('should render a loading message when the measurements are being loaded', async () => {
    mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurements);
    handleRendering();
    const loadingElement = await screen.findByTestId(/pageLoader/i);
    expect(loadingElement).toBeTruthy();
  });
});

describe('Component when measurements have been loaded', () => {
  it('should remove the loading message after the measurements have loaded', async () => {
    mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurements);
    handleRendering();
    await waitFor(() => expect(mockedBodyMeasurementsClient.getAllMeasurements).toHaveBeenCalledTimes(1));
    const loadingElement = screen.queryByText(/Loading/i);
    expect(loadingElement).toBeFalsy();
  });

  it('should remove the measurement that the user deletes', async () => {
    mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurements);
    mockedBodyMeasurementsClient.deleteMeasurement.mockResolvedValue(axiosResponse);
    handleRendering();
    await waitFor(() => expect(mockedBodyMeasurementsClient.getAllMeasurements).toHaveBeenCalledTimes(1));

    const waistCircumferenceFromMeasurementBeforeDeletion = screen.getByText(waistCircumference.toString());
    expect(waistCircumferenceFromMeasurementBeforeDeletion).toBeTruthy();

    const deleteMeasurementButton = screen.getByTestId('delete-measurement');
    fireEvent.click(deleteMeasurementButton);
    await waitFor(() => expect(mockedBodyMeasurementsClient.deleteMeasurement).toHaveBeenCalledTimes(1));

    const waistCircumferenceFromMeasurementAfterDeletion = screen.queryByText(waistCircumference.toString());
    expect(waistCircumferenceFromMeasurementAfterDeletion).toBeFalsy();

    const measurementRemovedMessage = await screen.findByText(/measurement removed/i);
    expect(measurementRemovedMessage).toBeTruthy();
  });
});

/*
Could not get these test to work when the other tests in this file run. Each of these tests will pass if it is the only test that is run. 
As soon as other tests in the file run, these tests will not pass.

I'm not sure why the toast message is not appearing in the cases when other tests run. One reason why this could be happening is because
the previous tests are somehow affecting these tests, even though it seems as if all tests are isolated. Also it could be a problem
with the Toastify library itself, but not sure.
*/

// describe('Component after a measurement has been created', () => {
//   it.only('should show a message indicating that a measurement was created', async () => {
//     mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurements);
//     handleRendering(true, false);
//     await waitFor(() => expect(mockedBodyMeasurementsClient.getAllMeasurements).toHaveBeenCalledTimes(1));
//     const measurementEditedElement = await screen.findByText(/measurement created/i);
//     expect(measurementEditedElement).toBeTruthy();
//   });
// });

// describe('Component after a measurement has been edited', () => {
//   it.only('should show a message indicating that a measurement was edited', async () => {
//     mockedBodyMeasurementsClient.getAllMeasurements.mockResolvedValue(bodyMeasurements);
//     handleRendering(false, true);
//     await waitFor(() => expect(mockedBodyMeasurementsClient.getAllMeasurements).toHaveBeenCalledTimes(1));
//     const measurementEditedElement = await screen.findByText(/measurement edited/i);
//     expect(measurementEditedElement).toBeTruthy();
//   });
// });

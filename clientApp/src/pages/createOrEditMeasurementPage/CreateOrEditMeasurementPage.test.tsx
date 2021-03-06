import React from 'react';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import { Router, Route } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { mocked } from 'ts-jest/utils';
import CreateOrEditMeasurementPage from './CreateOrEditMeasurementPage';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import { Gender, UserContextType } from '../../types/userTypes';
import { UserContext } from '../../contexts/UserContext';
import { AxiosResponse } from 'axios';
import { defaultUserContextType, defaultAxiosResponse, defaultUser } from '../../testHelpers/testData';
import { CreateOrEditMeasurement } from '../../types/bodyMeasurementTypes';

jest.mock('../../api/bodyMeasurementsClient');
let mockedBodyMeasurementsClient = mocked(bodyMeasurementsClient, true);
let axiosResponse: AxiosResponse;
let defaultUserContext: UserContextType;
let editMeasurementType: CreateOrEditMeasurement;

beforeEach(() => {
  defaultUserContext = defaultUserContextType;
  defaultUserContext.userDetailsAreBeingFetched = false;
  axiosResponse = defaultAxiosResponse;
  mockedBodyMeasurementsClient.getMeasurement.mockReset();
  mockedBodyMeasurementsClient.createMeasurement.mockReset();
  editMeasurementType = {
    neckCircumference: 10,
    waistCircumference: 30,
    hipCircumference: 10,
    weight: 10,
    dateAdded: new Date(2019, 9, 12),
    height: 60,
  };
});

const handleRendering = (userContext: UserContextType, measurementIsBeingCreated: boolean) => {
  const history = createMemoryHistory();
  let path;
  if (measurementIsBeingCreated) {
    path = '/create-measurement';
    history.push(path);
  } else {
    path = '/:measurementIdToEdit';
    history.push('/10');
  }
  return render(
    <Router history={history}>
      <UserContext.Provider value={defaultUserContext}>
        <Route path={path} component={CreateOrEditMeasurementPage} />
      </UserContext.Provider>
    </Router>
  );
};

describe('Page title for different modes', () => {
  it('should have a title of create measurement when in create mode', async () => {
    handleRendering(defaultUserContext, true);
    const titleInCreateMode = await waitFor(() => screen.getByText(/create measurement/i, {selector: 'h1'}));
    expect(titleInCreateMode).toBeTruthy();
  });

  it('should have a title of edit measurement when in edit mode', async () => {
    mockedBodyMeasurementsClient.getMeasurement.mockResolvedValue(editMeasurementType);
    handleRendering(defaultUserContext, false);
    await waitFor(() => expect(mockedBodyMeasurementsClient.getMeasurement).toHaveBeenCalledTimes(1));
    const titleInEditMode = screen.getByText(/edit measurement/i, {selector : 'h1'});
    expect(titleInEditMode).toBeTruthy();
  });
});

describe('Component when user details are being fetched', () => {
  it('should show a page loader', async () => {
    defaultUserContext.userDetailsAreBeingFetched = true;
    handleRendering(defaultUserContext, true);
    const pageLoader = await screen.findByTestId('pageLoader');
    expect(pageLoader).toBeTruthy();
  })
})

describe('Component when measurement to edit is being loaded', () => {
  it('should show a page loader', async () => {
    mockedBodyMeasurementsClient.getMeasurement.mockResolvedValue(editMeasurementType);
    handleRendering(defaultUserContext, false);
    const pageLoader = await screen.findByTestId('pageLoader');
    expect(pageLoader).toBeTruthy();
    await waitFor(() => expect(mockedBodyMeasurementsClient.getMeasurement).toHaveBeenCalledTimes(1));
  });
});

describe('Form fields for different genders', () => {
  it('should not have a hip circumference field if gender is male', async () => {
    defaultUserContext.gender = Gender.Male;
    handleRendering(defaultUserContext, true);
    const hipCircumferenceElement = await waitFor(() => screen.queryByLabelText(/hip circumference/i));
    expect(hipCircumferenceElement).toBeFalsy();
  });

  it('should have a hip circumference field if gender is female', async () => {
    defaultUserContext.gender = Gender.Female;
    handleRendering(defaultUserContext, true);
    const hipCircumferenceElement = await waitFor(() => screen.getAllByText(/hip circumference/i));
    expect(hipCircumferenceElement).toBeTruthy();
  });
});

describe('Component when trying to submit the form', () => {
  it('should enable submission if all form fields are valid', async () => {
    defaultUserContext.gender = Gender.Male;
    handleRendering(defaultUserContext, true);
    mockedBodyMeasurementsClient.createMeasurement.mockResolvedValue(axiosResponse);

    const neckCircumferenceInputElement = await waitFor(() => screen.getByLabelText(/neck Circumference/i));
    const waistCircumferenceInputElement = await waitFor(() => screen.getByLabelText(/waist Circumference/i));
    const heightInputElement = await waitFor(() => screen.getByLabelText(/height/i));
    const weightInputElement = await waitFor(() => screen.getByLabelText(/weight/i));
    const dateInputElement = await waitFor(() => screen.getByLabelText(/date/i));
    const submitButton = await waitFor(() => screen.getByText(/Create Measurement/i, {selector: 'button'}));

    expect(submitButton).toBeDisabled();

    await waitFor(() => fireEvent.change(neckCircumferenceInputElement, { target: { value: '12' } }));
    await waitFor(() => fireEvent.change(waistCircumferenceInputElement, { target: { value: '32' } }));
    await waitFor(() => fireEvent.change(heightInputElement, { target: { value: '60' } }));
    await waitFor(() => fireEvent.change(weightInputElement, { target: { value: '123' } }));
    await waitFor(() => fireEvent.change(dateInputElement, { target: { value: '2020-06-20' } }));

    expect(submitButton).toBeEnabled();

    fireEvent.click(submitButton);

    await waitFor(() => expect(mockedBodyMeasurementsClient.createMeasurement).toHaveBeenCalledTimes(1));
  });
});

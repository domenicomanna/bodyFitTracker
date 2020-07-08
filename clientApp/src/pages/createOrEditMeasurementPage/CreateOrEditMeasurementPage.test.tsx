import React from 'react';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import { Router, Route } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { mocked } from 'ts-jest/utils';
import CreateOrEditMeasurementPage from './CreateOrEditMeasurementPage';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import { UserModel, Gender } from '../../models/userModels';
import { UserContext } from '../../contexts/UserContext';
import { AxiosResponse } from 'axios';

jest.mock('../../api/bodyMeasurementsClient');
let mockedBodyMeasurementsClient = mocked(bodyMeasurementsClient, true);
let axiosResponse: AxiosResponse;
let userModel: UserModel;

beforeEach(() => {
  userModel = {
    isAuthenticated: () => false,
    gender: Gender.Female,
    token: '',
    measurementPreference: {
      measurementSystemName: 'Imperial',
      weightUnit: 'lb',
      lengthUnit: 'in',
    },
  };
  axiosResponse = {
    data: '',
    status: 200,
    statusText: 'OK',
    config: {},
    headers: {},
  };
  mockedBodyMeasurementsClient.createMeasurement.mockReset();
});

describe('Page title for different modes', () => {
  it('should have a title of create measurement when in create mode', async () => {
    const path = '/create-measurement';
    const history = createMemoryHistory();
    history.push(path);
    render(
      <Router history={history}>
        <UserContext.Provider value={userModel}>
          <Route path={path} component={CreateOrEditMeasurementPage} />
        </UserContext.Provider>
      </Router>
    );
    const titleInCreateMode = await waitFor(() => screen.getByText(/create measurement/i));
    expect(titleInCreateMode).toBeTruthy();
  });

  it('should have a title of edit measurement when in edit mode', async () => {
    mockedBodyMeasurementsClient.getMeasurement.mockResolvedValue({
      neckCircumference: 10,
      waistCircumference: 30,
      hipCircumference: 10,
      weight: 10,
      dateAdded: new Date(2019, 9, 12),
      height: 60
    });
    const history = createMemoryHistory();
    history.push('/10');
    render(
      <Router history={history}>
        <UserContext.Provider value={userModel}>
          <Route path='/:measurementIdToEdit' component={CreateOrEditMeasurementPage} />
        </UserContext.Provider>
      </Router>
    );
    await waitFor(() => expect(mockedBodyMeasurementsClient.getMeasurement).toHaveBeenCalledTimes(1));
    const titleInEditMode = screen.getByText(/edit measurement/i);
    expect(titleInEditMode).toBeTruthy();
  });
});

describe('Form fields for different genders', () => {
  const handleRendering = (gender: Gender) => {
    userModel.gender = gender;
    const path = '/create-measurement';
    const history = createMemoryHistory();
    history.push(path);
    return render(
      <Router history={history}>
        <UserContext.Provider value={userModel}>
          <Route path={path} component={CreateOrEditMeasurementPage} />
        </UserContext.Provider>
      </Router>
    );
  };

  it('should not have a hip circumference field if gender is male', async () => {
    handleRendering(Gender.Male);
    const hipCircumferenceElement = await waitFor(() => screen.queryByLabelText(/hip circumference/i));
    expect(hipCircumferenceElement).toBeFalsy();
  });

  it('should have a hip circumference field if gender is female', async () => {
    handleRendering(Gender.Female);
    const hipCircumferenceElement = await waitFor(() => screen.getAllByText(/hip circumference/i));
    expect(hipCircumferenceElement).toBeTruthy();
  });
});

describe('Component when trying to submit the form', () => {
  const handleRendering = () => {
    userModel.gender = Gender.Male;
    const path = '/create-measurement';
    const history = createMemoryHistory();
    history.push(path);
    return render(
      <Router history={history}>
        <UserContext.Provider value={userModel}>
          <Route path={path} component={CreateOrEditMeasurementPage} />
        </UserContext.Provider>
      </Router>
    );
  };

  it('should have a disabled submit button if form fields are invalid', async () => {
    handleRendering();
    const weightInputElement = await waitFor(() => screen.getByLabelText(/weight/i));
    await waitFor(() => fireEvent.change(weightInputElement, { target: { value: '-123' } })); // invalid weight
    await waitFor(() => fireEvent.blur(weightInputElement));

    const weightValidationError = screen.getByTestId(/weightFieldValidationError/i);
    const submitButton = screen.getByText(/submit/i);
    expect(weightValidationError).toBeTruthy();
    expect(submitButton).toBeDisabled();
  });

  it('should enable submission if all form fields are valid', async () => {
    handleRendering();
    mockedBodyMeasurementsClient.createMeasurement.mockResolvedValue(axiosResponse);

    const neckCircumferenceInputElement = await waitFor(() => screen.getByLabelText(/neck Circumference/i));
    const waistCircumferenceInputElement = await waitFor(() => screen.getByLabelText(/waist Circumference/i));
    const heightInputElement = await waitFor(() => screen.getByLabelText(/height/i));
    const weightInputElement = await waitFor(() => screen.getByLabelText(/weight/i));
    const dateInputElement = await waitFor(() => screen.getByLabelText(/date/i));
    const submitButton = await waitFor(() => screen.getByText(/submit/i));

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

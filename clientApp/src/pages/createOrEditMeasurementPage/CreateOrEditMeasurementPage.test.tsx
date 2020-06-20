import React from 'react';
import { render, screen, waitForElementToBeRemoved, waitFor, fireEvent } from '@testing-library/react';
import { Router, Route } from 'react-router-dom';
import { createMemoryHistory } from 'history'
import CreateOrEditMeasurementPage from './CreateOrEditMeasurementPage';

beforeEach(() => {});

describe('Component when in create mode', () => {
  it('should have a title of create measurement', async () => {
    const history = createMemoryHistory();
    history.push('create-measurement');
    render(
      <Router history={history}>
        <Route path="/create-measurement" component = {CreateOrEditMeasurementPage}/>
      </Router>
    );
    const titleInCreateMode = screen.getByText(/create measurement/i);
    expect(titleInCreateMode).toBeTruthy();
  });
});

describe('Component when in edit mode', () => {
  it('should have a title of edit measurement', async () => {
    const history = createMemoryHistory();
    history.push('/10');
    render(
      <Router history={history}>
        <Route path="/:measurementIdToEdit" component = {CreateOrEditMeasurementPage}/>
      </Router>
    );
    const titleInEditMode = screen.getByText(/edit measurement/i);
    expect(titleInEditMode).toBeTruthy();
  });
});

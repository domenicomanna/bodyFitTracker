import React from 'react';
import { render, screen } from '@testing-library/react';
import Button from './Button';

it('should not render loader if button is not submitting', () => {
	render(<Button type='submit' buttonClass='primary' isSubmitting={false}/>)
  const loaderElement = screen.queryByTestId(/loader/i);
  expect(loaderElement).toBeFalsy();
});

it('should render loader if button is submitting', () => {
	render(<Button type='submit' buttonClass='primary' isSubmitting={true}/>)
	const loaderElement = screen.getByTestId(/loader/i);
  expect(loaderElement).toBeTruthy();
});

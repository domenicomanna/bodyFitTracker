import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import Header from './Header';
import { MemoryRouter } from 'react-router-dom';

describe('Drop down menu', () => {
  it('should toggle the profile drop down menu when the profile button is clicked', () => {
    render(<Header />, { wrapper: MemoryRouter });

    expect(screen.queryByTestId('dropDownMenu')).toBeFalsy();
    const dropDownButtonElement = screen.getByText(/my profile/i);

    fireEvent.click(dropDownButtonElement);
    expect(screen.getByTestId('dropDownMenu')).toBeTruthy();

    fireEvent.click(dropDownButtonElement);
    expect(screen.queryByTestId('dropDownMenu')).toBeFalsy();
  });

  it('should close drop down menu when an element outside of the drop down menu is clicked', () => {
    render(<Header />, { wrapper: MemoryRouter });

    fireEvent.click(screen.getByText(/my profile/i));
    expect(screen.getByTestId('dropDownMenu')).toBeTruthy();

    fireEvent.click(screen.getByTestId('header'));
    expect(screen.queryByTestId('dropDownMenu')).toBeFalsy();
  });
});

import { fireEvent, render, screen } from '@testing-library/react';
import { NavigateFunction, useNavigate } from 'react-router-dom';
import type { Mock } from 'vitest';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import UserSettings from '../../pages/UserSettings';
import { setLocation } from '../../services/DefaultLocationService';

vi.mock('react-router-dom', () => ({
  useNavigate: vi.fn(),
}));

vi.mock('../../services/DefaultLocationService', () => ({
  setLocation: vi.fn().mockResolvedValue(undefined)
}))


describe('UserSettings', () => {
  let mockNavigate: NavigateFunction & Mock; 
  
  beforeEach(() => {
    vi.clearAllMocks();

    mockNavigate = vi.fn();
    vi.mocked(useNavigate).mockReturnValue(mockNavigate);
  });

  it('should render the component correctly', () => {
    render(<UserSettings />);

    expect(screen.getByPlaceholderText('Enter location')).toBeInTheDocument();
    expect(screen.getByText('Save location')).toBeInTheDocument();
  });

  it('should show an alert if the location is empty', async () => {
    const mockAlert = vi.spyOn(window, 'alert').mockImplementation(() => {});

    render(<UserSettings />);

    const saveButton = screen.getByText('Save location');
    fireEvent.click(saveButton);

    expect(mockAlert).toHaveBeenCalledWith('Location cannot be empty');

    mockAlert.mockRestore();
  });

  it('should call setLocation and navigate when a valid location is entered', async () => {
    render(<UserSettings />);
  
    const input = screen.getByPlaceholderText('Enter location');
    fireEvent.change(input, { target: { value: 'London' } });
  
    const saveButton = screen.getByText('Save location');
    fireEvent.click(saveButton);
  
    await vi.waitFor(() => {   
      expect(setLocation).toHaveBeenCalledWith('London');   
      expect(mockNavigate).toHaveBeenCalledWith('/forecasts');
    });
  });  
});
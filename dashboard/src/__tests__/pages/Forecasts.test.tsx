import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import { WeatherForecast } from '../../models/WeatherForecast';
import WeatherForecasts from '../../pages/Forecasts';
import { getLocation } from '../../services/DefaultLocationService';
import { getForecasts } from '../../services/ForecastsService';

vi.mock('../../services/DefaultLocationService', () => ({
    getLocation: vi.fn(),
  }))
  
vi.mock('../../services/ForecastsService', () => ({  
  getForecasts: vi.fn(),
}));

describe('WeatherForecasts', () => {
    const mockLocation = 'London';
    const mockForecasts: WeatherForecast[] = [
      { temperature: 20, humidity: 60, windSpeed: 10, weatherIcon: '01d' },
      { temperature: 18, humidity: 70, windSpeed: 15, weatherIcon: '02d' },
    ];
  
    beforeEach(() => {
      vi.clearAllMocks();
  
      vi.mocked(getLocation).mockResolvedValue(mockLocation);
  
      vi.mocked(getForecasts).mockResolvedValue(mockForecasts);
    });
  
    it('should render the component correctly', async () => {
      render(<WeatherForecasts />);
  
      expect(screen.getByPlaceholderText('Enter location')).toBeInTheDocument();
      expect(screen.getByText('Forecast')).toBeInTheDocument();
  
      await waitFor(() => {
        expect(screen.getByDisplayValue(mockLocation)).toBeInTheDocument();
      });
    });
  
    it('should show an alert if the location is empty', async () => {

      const mockAlert = vi.spyOn(window, 'alert').mockImplementation(() => {});
  
      render(<WeatherForecasts />);
  
      const forecastButton = screen.getByText('Forecast');
      fireEvent.click(forecastButton);
  
      expect(mockAlert).toHaveBeenCalledWith('Location cannot be empty');
  
      mockAlert.mockRestore();
    });
  
    it('should call getForecasts and update the grid when a valid location is entered', async () => {
      render(<WeatherForecasts />);
  
      const input = screen.getByPlaceholderText('Enter location');
      await userEvent.type(input, 'Paris');
  
      const forecastButton = screen.getByText('Forecast');
      fireEvent.click(forecastButton);
      
      await waitFor(() => {
        expect(screen.getByText('20')).toBeInTheDocument(); 
        expect(screen.getByText('60')).toBeInTheDocument();
        expect(screen.getByText('10')).toBeInTheDocument();
      });
    });
  });
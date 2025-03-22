import MockAdapter from 'axios-mock-adapter';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import { WeatherForecast } from '../../models/WeatherForecast';
import { apiClient, getForecasts } from '../../services/ForecastsService';

describe('getForecasts', () => {
  let mockAxios: MockAdapter;

  beforeEach(() => {
    mockAxios = new MockAdapter(apiClient); 
  });

  afterEach(() => {
    mockAxios.restore();
  });
 
  it('should fetch forecasts successfully', async () => {
    const location = 'NewYork';
    const mockData: WeatherForecast[] = [
        { 
            temperature: 20,
            humidity: 20,
            windSpeed: 20,
            weatherIcon: "abc"
        }
    ];

    mockAxios.onGet(`/${location}`).reply(200, mockData);

    const result = await getForecasts(location);

    expect(result).toEqual(mockData);
  });

  it('should handle errors and return an empty array', async () => {
    const location = 'InvalidLocation';
    const consoleErrorMock = vi.spyOn(console, 'error').mockImplementation(() => {});
    
    mockAxios.onGet(`/${location}`).networkError();

    const result = await getForecasts(location);

    expect(result).toEqual([]);   
    
    expect(consoleErrorMock).toHaveBeenCalledWith(
      'Error fetching forecasts: ',
      expect.any(Error)
    );
  });
});
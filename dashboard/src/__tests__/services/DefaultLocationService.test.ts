import MockAdapter from 'axios-mock-adapter';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import { apiClient, getLocation, setLocation } from '../../services/DefaultLocationService';

describe('Location Service', () => {
  let mockAxios: MockAdapter;

  beforeEach(() => {
    mockAxios = new MockAdapter(apiClient); 
  });

  afterEach(() => {
    mockAxios.restore();
    vi.restoreAllMocks();
  });

  describe('setLocation', () => {
    it('should successfully set location', async () => {
      const location = 'London';
      mockAxios.onPut(`/${location}`).reply(200);

      await expect(setLocation(location)).resolves.not.toThrow();
      expect(mockAxios.history.put[0].url).toBe(`/${location}`);
    });

    it('should handle errors when setting location', async () => {
      const location = 'InvalidLocation';
      const consoleErrorMock = vi.spyOn(console, 'error').mockImplementation(() => {});
      
      mockAxios.onPut(`/${location}`).networkError();

      const result = await setLocation(location);
      
      expect(result).toEqual([]);
      expect(consoleErrorMock).toHaveBeenCalledWith(
        'Error setting location: ',
        expect.any(Error)
      );
    });
  });

  describe('getLocation', () => {
    it('should successfully fetch current location', async () => {
      const mockData = 'London';
      mockAxios.onGet('').reply(200, mockData);

      const result = await getLocation();
      expect(result).toBe(mockData);
    });

    it('should handle errors when fetching location', async () => {
      const consoleErrorMock = vi.spyOn(console, 'error').mockImplementation(() => {});
      mockAxios.onGet('').networkError();

      const result = await getLocation();
      
      expect(result).toEqual([]);
      expect(consoleErrorMock).toHaveBeenCalledWith(
        'Error getting location: ',
        expect.any(Error)
      );
    });
  });
});
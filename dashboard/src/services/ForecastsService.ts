import axios from "axios";
import { WeatherForecast } from "../models/WeatherForecast";

export const apiClient = axios.create({
    baseURL: "http://localhost:5002/api/forecast",                                    
    timeout: 10000, 
    headers: {
      "Content-Type": "application/json",
    },
})

export const getForecasts = async (location:string) => {
    try {
        const response = await apiClient.get<WeatherForecast[]>(`/${location}`)     
        
        return response.data;                     
    } catch (error) {
        console.error("Error fetching forecasts: ", error)
        return []
    }
}
import axios from "axios";

export const apiClient = axios.create({
    baseURL: "http://localhost:5002/api/DefaultLocation",                                    
    timeout: 10000, 
    headers: {
      "Content-Type": "application/json",
    },
})

export const setLocation = async (location: string) => {
    try {
        await apiClient.put(`/${location}`)                
    } catch (error) {
        console.error("Error setting location: ", error)
        return []
    }
}

export const getLocation = async () => {
    try {
        const response = await apiClient.get("")                
        return response.data
    } catch (error) {
        console.error("Error getting location: ", error)
        return []
    }
}
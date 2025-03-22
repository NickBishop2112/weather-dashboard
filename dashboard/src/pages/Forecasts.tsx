import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import "ag-grid-community/styles/ag-theme-alpine.css";
import { AgGridReact } from "ag-grid-react";
import { useEffect, useRef, useState } from "react";
import { WeatherForecast } from '../models/WeatherForecast';
import { getLocation } from '../services/DefaultLocationService';
import { getForecasts } from '../services/ForecastsService';
ModuleRegistry.registerModules([AllCommunityModule]);

const WeatherForecasts = () => {

    const locationRef = useRef<HTMLInputElement>(null);
    const [rowData, setRowData] = useState<WeatherForecast[]>([]); 
    const [refresh, setRefresh] = useState<boolean>(false);    
    
    const columnDefs: { field: keyof WeatherForecast; width?: number; headerName:string;filter: boolean;cellRenderer?:object}[] = [
        { field: "temperature", width: 120, headerName: "Temperature", filter: false },
        { field: "humidity", width: 100, headerName: "Humidity", filter: false },
        { field: "windSpeed", width: 120, headerName: "Wind Speed", filter: false  },
        { field: "weatherIcon", width: 80, headerName: "Icon",  filter: false,
            cellRenderer: (params: { value: string | number; }) => {
                    
                    const imageUrl = `src/assets/weather_icons/${params.value}.png`;

                    return (
                        <span style={{ fontSize: "24px" }}>
                        <img src={imageUrl} alt="" />                        
                        </span>
                    );  
                }
            }
    ];
    

    const gridOptions = {
        localeText: {
            noRowsToShow: "No forecasts found" 
        }
    };

    useEffect(() => {

        if (!refresh)
        {
            return;
        }

        const weatherForecast = getForecasts(locationRef.current?.value as string);   
                
        weatherForecast.then((data) => {
            setRowData(data);
            setRefresh(false)
        });
    }, [refresh]); 
    
    useEffect(() => {
        
        const location = getLocation();   
                
        location.then((data) => {            
            locationRef.current!.value = data;
        });
    }, []); 

    const onClick =  async () => {
            
            const location = locationRef.current?.value;  
            
            if (!location?.trim()) {
                alert("Location cannot be empty");
                locationRef.current?.focus();
                return;
            }
    
            setRefresh(true);    
        };

    return (
        <div>            
            <div className="">
                
                <input
                    type="text"                                
                    className="border border-gray-300 rounded-md p-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    placeholder='Enter location'
                    ref={locationRef} 
                />
                &nbsp;&nbsp;&nbsp;
                <button
                    onClick={onClick}
                    className="px-4 py-2 bg-blue-500 text-black rounded-md shadow hover:bg-blue-600 focus:ring-2 focus:ring-blue-300 focus:outline-none"
                >
                    Forecast
                </button>
            </div>

            <div
                style={{
                    display: "flex",
                    justifyContent: "center", 
                    alignItems: "flex-start", 
                    padding: "20px",
                }}>

                <div className="ag-theme-alpine w-full" style={{ height: "400px", width: "475px" }}>            
                    <AgGridReact
                        columnDefs={columnDefs}
                        rowData={rowData}   
                        gridOptions={gridOptions}
                    />
                </div>
            </div>
        </div>
    )
}

export default WeatherForecasts;
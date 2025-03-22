import { useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { setLocation } from '../services/DefaultLocationService';

export const UserSettings = () => {

    const navigate = useNavigate();
    const locationRef = useRef<HTMLInputElement>(null);

    const onClick =  async () => {
        
        const location = locationRef.current?.value;  
        
        if (!location?.trim()) {
            alert("Location cannot be empty");
            locationRef.current?.focus();
            return;
        }

        await setLocation(location);
        navigate("/forecasts"); 
    };

    return (
        <div className="flex flex-col space-y-3 p-4 w-full max-w-sm">
           
            <div className="flex flex-col w-full">                
                <input
                    id="location"
                    type="text"
                    className="w-full border border-gray-300 rounded-md p-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    placeholder="Enter location"
                    ref={locationRef}
                />
            </div>
            <div className="flex justify-end w-full">
                <button
                    onClick={onClick}
                    className="px-4 py-2 bg-blue-500 text-black rounded-md shadow hover:bg-blue-600 focus:ring-2 focus:ring-blue-300 focus:outline-none"
                >
                    Save location
                </button>
            </div>
        </div>
    );
};

export default UserSettings
import { BrowserRouter, Route, Routes } from "react-router-dom";
import './App.css';
import { Layout } from "./components/Layout";
import Forecasts from "./pages/Forecasts";
import { UserSettings } from './pages/UserSettings';

const App = () => {  
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<UserSettings />} />   
          <Route path="/forecasts" element={<Forecasts />} />   
        </Routes>
      </Layout>    
    </BrowserRouter>
  )
}

export default App
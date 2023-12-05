import { BrowserRouter, Route, Routes } from 'react-router-dom';
import HouseList from '../house/HouseList';
import './App.css';
import Header from './Header';
import HouseDetail from '../house/HouseDetail';

function App() {
  return (
    <BrowserRouter>
    <div className="container">
      <Header title='Providing houses all over the world!'/>
      <Routes>
        <Route path="/" element={<HouseList />} />
        <Route path="/house/:id" element={<HouseDetail />} />
      </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;

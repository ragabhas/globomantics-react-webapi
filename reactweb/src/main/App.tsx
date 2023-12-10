import { BrowserRouter, Route, Routes } from 'react-router-dom';
import HouseList from '../house/HouseList';
import './App.css';
import Header from './Header';
import HouseDetail from '../house/HouseDetail';
import HouseAdd from '../house/HouseAdd';
import HouseEdit from '../house/HouseEdit';

function App() {
  return (
    <BrowserRouter>
    <div className="container">
      <Header title='Providing houses all over the world!'/>
      <Routes>
        <Route path="/" element={<HouseList />} />
        <Route path="/house/:id" element={<HouseDetail />} />
        <Route path="/house/add" element={<HouseAdd />} />
        <Route path="/house/edit/:id" element={<HouseEdit />} />
      </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;

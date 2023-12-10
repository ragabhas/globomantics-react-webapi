import { useNavigate } from "react-router-dom";
import ApiStatus from "../ApiStatus";
import { currencyFormatter } from "../config";
import useFetchHouses from "../hooks/HouseHooks";
import { Link } from "react-router-dom";

const HouseList = () => {
    const {data, status, isSuccess} = useFetchHouses();
    const navigate = useNavigate();

    if(!isSuccess){
        return <ApiStatus status={status} />
    }

    return(
        <div>
            <div className="row mb-2">
                <h5 className="themeFontColor text-center">
                    Houses currently in the market
                </h5>
            </div>
            <table className="table table-hoover">
                <thead>
                    <tr>
                        <td>Address</td>
                        <td>Country</td>
                        <td>Price</td>
                    </tr>
                </thead>
                <tbody>
                    {data && data.map((house) => (
                        <tr key={house.id} onClick={()=> navigate(`house/${house.id}`)}>
                            <td>{house.address}</td>
                            <td>{house.country}</td>
                            <td>{currencyFormatter.format(house.price)}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <Link className="btn btn-primary" to={"/house/add"}>
                Add
            </Link>
        </div>
    );
};

 export default HouseList;
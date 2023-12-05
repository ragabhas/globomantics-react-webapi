import ApiStatus from "../ApiStatus";
import { currencyFormatter } from "../config";
import useFetchHouse from "../hooks/HouseHooks";

const HouseList = () => {
    const {data, status, isSuccess} = useFetchHouse();
    
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
                        <tr key={house.id}>
                            <td>{house.address}</td>
                            <td>{house.country}</td>
                            <td>{currencyFormatter.format(house.price)}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

 export default HouseList;
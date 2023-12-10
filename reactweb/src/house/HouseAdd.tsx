import ValidationSummary from "../VlidationSummary";
import { useAddHouse } from "../hooks/HouseHooks"
import { House } from "../types/house";
import HouseForm from "./HouseForm";

const HouseAdd = () => {
    const addHouse = useAddHouse();

    const house : House = {
        id: 0,
        address: "",
        country: "",
        description: "",
        price: 0,
        photo: ""
    };

    return (
        <>
        {addHouse.isError && (
            <ValidationSummary error={addHouse.error} />
        )}
                <HouseForm house={house} submitted={(h)=> addHouse.mutate(h)} />
        </>
        
    );
}

export default HouseAdd;
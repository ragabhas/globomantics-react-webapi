import { useParams } from "react-router-dom";
import { useFetchHouse, useUpdateHouse } from "../hooks/HouseHooks";
import ApiStatus from "../ApiStatus";
import HouseForm from "./HouseForm";

const HouseEdit = () => {
    const { id } = useParams();
    if(!id) throw Error("No id provided");

    const houseId = parseInt(id);
    const{data, status, isSuccess} = useFetchHouse(houseId);
    const updateHouse = useUpdateHouse();

    if(!isSuccess) return <ApiStatus status={status} />

    return(
        <HouseForm house={data} submitted={(h)=> updateHouse.mutate(h)} />
    );
}
export default HouseEdit;
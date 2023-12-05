import { House } from "../types/house";
import { config } from "../config";
import { useQuery } from "react-query";
import axios, { AxiosError } from "axios";

const useFetchHouses = () => {

    return useQuery<House[], AxiosError>("houses", ()=>
        axios.get(`${config.baseUrl}/houses`).then((res)=>res.data)
    );
}

const useFetchHouse = (id:number) => {

    return useQuery<House, AxiosError>(["houses", id], ()=>
        axios.get(`${config.baseUrl}/house/${id}`).then((res)=>res.data)
    );
}
export default useFetchHouses;
export { useFetchHouse };
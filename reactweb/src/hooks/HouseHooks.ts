import { House } from "../types/house";
import { config } from "../config";
import { useQuery } from "react-query";
import axios, { AxiosError } from "axios";

const useFetchHouse = () => {

    return useQuery<House[], AxiosError>("houses", ()=>
        axios.get(`${config.baseUrl}/houses`).then((res)=>res.data)
    );
}

export default useFetchHouse;

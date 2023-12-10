import { House } from "../types/house";
import { config } from "../config";
import { useMutation, useQuery, useQueryClient } from "react-query";
import axios, { AxiosError, AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";

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

const useAddHouse = () => {
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    return useMutation<AxiosResponse, AxiosError, House>((house)=>axios.post(`${config.baseUrl}/houses`, house), {
        onSuccess: () => {
            queryClient.invalidateQueries("houses");
            navigate("/");
        }
    });
}

const useUpdateHouse = () => {
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    return useMutation<AxiosResponse, AxiosError, House>((house)=>axios.put(`${config.baseUrl}/houses`, house), {
        onSuccess: (_, house) => {
            queryClient.invalidateQueries("houses");
            navigate(`/house/${house.id}`);
        }
    });
}

const useDeleteHouse = () => {
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    return useMutation<AxiosResponse, AxiosError, House>((house)=>axios.delete(`${config.baseUrl}/houses/${house.id}`), {
        onSuccess: () => {
            queryClient.invalidateQueries("houses");
            navigate("/");
        }
    });
}
export default useFetchHouses;
export { useFetchHouse, useAddHouse, useUpdateHouse, useDeleteHouse };
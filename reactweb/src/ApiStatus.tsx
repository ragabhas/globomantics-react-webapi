type Args = {
    status: "idle" | "loading" | "success" | "error";
}

const ApiStatus = ({ status }: Args) => {
    switch (status) {
        case "idle":
            return <div>Idle</div>;
        case "loading":
            return <div>Loading...</div>;
        case "error":
            return <div>Error communicating with backend.</div>;
        default:
            throw new Error("Unknown API status.");
    }

}

export default ApiStatus;
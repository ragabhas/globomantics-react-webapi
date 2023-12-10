import { useState } from "react";
import ApiStatus from "../ApiStatus";
import { useAddBid, useFetchBids } from "../hooks/BidHooks";
import { Bid } from "../types/bid";
import { House } from "../types/house";
import { currencyFormatter } from "../config";

type Arg = {
    house: House
};

const Bids = ({ house }: Arg) => {
    const {data, status, isSuccess} = useFetchBids(house.id);
    const addBid = useAddBid();

    const defaultBid: Bid = {
        id: 0,
        amount: 0,
        bidder: "",
        houseId: house.id
    };
    
    const [bid, setBid] = useState<Bid>(defaultBid);
    
    const submit = () => {
        addBid.mutate(bid);
        setBid(defaultBid);
    };

    if(!isSuccess){
        return <ApiStatus status={status} />
    }

    return(
        <>
        <div className="row mt-4">
          <div className="col-12">
            <table className="table table-sm">
              <thead>
                <tr>
                  <th>Bidder</th>
                  <th>Amount</th>
                </tr>
              </thead>
              <tbody>
                {data &&
                  data.map((b) => (
                    <tr key={b.id}>
                      <td>{b.bidder}</td>
                      <td>{currencyFormatter.format(b.amount)}</td>
                    </tr>
                  ))}
              </tbody>
            </table>
          </div>
        </div>
        <div className="row">
          <div className="col-4">
            <input
              id="bidder"
              className="h-100"
              type="text"
              value={bid.bidder}
              onChange={(e) => setBid({ ...bid, bidder: e.target.value })}
              placeholder="Bidder"
            ></input>
          </div>
          <div className="col-4">
            <input
              id="amount"
              className="h-100"
              type="number"
              value={bid.amount}
              onChange={(e) =>
                setBid({ ...bid, amount: parseInt(e.target.value) })
              }
              placeholder="Amount"
            ></input>
          </div>
          <div className="col-2">
            <button
              className="btn btn-primary"
              onClick={() => submit()}
            >
              Add
            </button>
          </div>
        </div>
      </>
    );

}

export default Bids;
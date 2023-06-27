import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Filter from "./Filter";

const Listing =  () => {
    const { listingid } = useParams();
    const [listingData, setListingData] = useState(null)

    useEffect(() => {
        const fetchListing = async () => {
            const response = await fetch(`https://localhost:7158/api/listings/${listingid}`)
            setListingData(await response.json())
        }

        fetchListing()
    }, [listingid])
    return(
        <div id="sidebar">
            {listingData ? (
                <>
                    <h2>Name</h2>
                    <p>{listingData.name}</p>

                    <h2>Description</h2>
                    <p>{listingData.amenities}</p>
                </>
            ) : (
                <p>No listing selected</p>
            )
            }
        </div>
    )
}

export default Listing;
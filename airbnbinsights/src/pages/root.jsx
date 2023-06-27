import React from "react";
import { Outlet } from "react-router-dom";

import Map from "../components/Map";
import Filter from "../components/Filter";

const Root = () => {
    return(
        <>
            <div id="container">
                <Outlet />
                <Map />
            </div>
            
        </>
    )   
}

export default Root;
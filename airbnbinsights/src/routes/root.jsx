import React from "react";
import { Outlet } from "react-router-dom";

import Map from "../components/Map";
import Charts from "./Charts";

const Root = () => {
    return(
        <>
            <div id="container">
                <Outlet />
                <Map />
            </div>
            <Charts />
        </>
    )   
}

export default Root;
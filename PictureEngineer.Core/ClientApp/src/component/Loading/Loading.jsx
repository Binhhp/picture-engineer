import "./loading.css";
import React from "react";

export default function Loading(){
    return(
        <div id="loader-wrapper">
            <div className="lds-ring">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <span>Loading...</span>
            </div>
        </div>
    )
}
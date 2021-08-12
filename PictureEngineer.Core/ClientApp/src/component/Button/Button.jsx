import { useState } from "react";
import React from "react";
export default function Button({ service, searchBlog, keyWord }){
    const [background, setBackground] = useState('#fff');
    const [colors, setColors] = useState('#212529');
    return (
        <button className='SeRt' 
            onMouseOver={() => {
                setBackground(service.Color);
                setColors('#fff');
            }} 

            onMouseLeave={() => {
                setBackground('#fff');
                setColors('#212529');
            }} 
            
            onClick={() => searchBlog(keyWord)}
            style={{background: background, border: '1px solid ' + service.Color, color: colors}}>
            <img src={service.Icon} alt={service.Name}/>
            <span>{service.Name}</span>
        </button>
    );
}
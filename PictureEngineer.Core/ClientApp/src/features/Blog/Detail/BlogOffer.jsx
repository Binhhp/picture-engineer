import React from "react";
import { Link } from "react-router-dom";
export default function BlogOffer({ ...props}){
    return(
        props.blogs.map((item, i) => (
            <div className="item" key={"blog-" + i}>
                <div className="item-img">
                    <Link to={`/bai-viet/${item.MetaTitle}`}> 
                        <img src={item.ImagePath} alt={item.Title} />
                    </Link>
                </div>
                <div className="item-title">
                    <Link to={`/bai-viet/${item.MetaTitle}`}>{item.Title}</Link>
                </div>
            </div>
        ))
    )
}
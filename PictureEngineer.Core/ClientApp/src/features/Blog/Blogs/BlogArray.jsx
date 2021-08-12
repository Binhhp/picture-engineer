import React from "react"
import { useSelector } from "react-redux";
import formatDate from "../../../utils/round-number/formatDateTime.js";
import { Link } from "react-router-dom";

function BlogArray(){
    
    const result = useSelector(state => state.blogs);

    return (
        <div className="main-blog">
            <div className="Y-h">
                {
                    result.data.map((item, i) => (
                        <div key={`blog-${i}`} className="YI MIsw Hb7 fadeOutUp">
                            <div className="blog-item">
                                <div className="isy-Hyu">
                                    <Link to={`/bai-viet/${item.MetaTitle}`} className="blog-img">
                                        <img className="MIw" src={item.ImagePath} alt={item.Title} />
                                    </Link>
                                </div>
                                <div className="blog-title">
                                    <Link to={`/bai-viet/${item.MetaTitle}`} className="blog-link">
                                        {item.Title}
                                    </Link>
                                    <div className="time">
                                        <i className="far fa-clock"></i>&nbsp;&nbsp;
                                        <span>{formatDate(item.DateCreated)}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    ))
                }
            </div>
        </div>
    )
}

export default React.memo(BlogArray)
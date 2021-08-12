import React from "react";
import "./loading.css";

export default function Loading({color}){
    return(
        <section className="picture-engineer__upload" style={{backgroundColor: color }}> 
            <div className="uploader">
                <div className="upload-file dropzone">
                    <div className="loading-process">
                        <div className="loader"><div></div><div></div><div></div><div></div></div>
                        <span>Đang xử lý...</span>
                    </div>
                </div>
            </div>
        </section>
    )
}
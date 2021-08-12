import React from "react";
export default function Download({ download, color }){
    return (
        <section className="picture-engineer__upload" style={{backgroundColor: color }}>
            <div className="uploader">
                <div onClick={() => download()} className="upload-file dropzone">
                    <i className="icon-download2"></i>
                    <span>Tải file xuống ở đây</span>
                    <em>hoặc</em>
                    <div>
                        <button style={{color: color}}>Download file</button>
                    </div>
                </div>
            </div>
        </section>
    )
}
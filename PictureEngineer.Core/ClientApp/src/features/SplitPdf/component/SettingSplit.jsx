import React from "react";

export default function SettingSplit({ 
    colorService, 
    startPdfViewer, 
    startPage, 
    endPdfViewer, 
    endPage, 
    minus, 
    plus 
}){
    return (
        <React.Fragment>
        {/* View pdf */}
        <div className="viewer-pdf">
            <object
                style={{border: `1px solid ${colorService}`}}
                type="application/pdf" 
                data={startPdfViewer} 
                aria-label="start-pdf-viewer"></object>
            <span><i className="fas fa-long-arrow-alt-right"></i></span>
            <object 
                style={{border: `1px solid ${colorService}`}}
                type="application/pdf" 
                data={endPdfViewer} 
                aria-label="start-pdf-viewer"></object>

        </div>
        <div className="setting-split">
            <label>Từ trang</label>
            <div className="start">
                <span className="minus" 
                    onClick={() => minus(true)} 
                    style={{color: colorService}}>

                    <i className="fas fa-minus"></i>
                </span>
                <p style={{border: `1px solid ${colorService}`}}>{startPage}</p> 
                <span className="plus" 
                    onClick={() => plus(true)}
                    style={{color: colorService}}>

                    <i className="fas fa-plus"></i>
                </span>
            </div>  
            <label>Đến trang</label>
            <div className="end">
                <span className="minus" 
                    onClick={() => minus(false)}
                    style={{color: colorService}}>

                    <i className="fas fa-minus"></i>
                </span>
                <p style={{border: `1px solid ${colorService}`}}>{endPage}</p>
                <span className="plus" 
                    onClick={() => plus(false)}
                    style={{color: colorService}}>

                    <i className="fas fa-plus"></i>
                </span>
            </div>
            </div>
        </React.Fragment>
    )
}
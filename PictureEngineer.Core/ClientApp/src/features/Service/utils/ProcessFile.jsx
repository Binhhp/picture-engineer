import React from "react";
import File from "../../../themes/file";

export default function ProcessFile({ ...props }){
    return (
            <section className="picture-engineer__process" style={{ border: '1px solid ' + props.colorService }}>
                <div className="processing__main">
                    <div className="upload__view">
                        <File></File>
                        <div className="name-File">
                            <span>{props.file.fileName}</span>
                        </div>
                        <div className="size-File">
                            <span>{props.file.fileSize + ' MB'}</span>
                        </div>
                    </div>
                    {props.children}
                    <div className="process-File">
                        <span className="btn-back" onClick={() => props.back()}>
                            Quay lại
                        </span>
                        <span className="btn-process" onClick={() => props.processFile()} 
                            style={{ background: `${props.colorService}`, border: `1px solid ${props.colorService}` }}>
                            Xử lý
                        </span>
                    </div>
                </div>
            </section>
    )
}
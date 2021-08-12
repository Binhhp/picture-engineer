import { useRef } from "react";
import { roundNumberScale } from "../../../utils/round-number/roundNumberScale";
import './upload.css';
import React from "react";

export default function UploadFile({ 
    color, 
    selectedFile,
    changeHanlder
}){
     //style css processing bar
     const style = {
        width: '0%',
        backgroundImage: 'linear-gradient(45deg, rgb(255, 165, 51) 25%, rgb(212, 143, 56) 25%, rgb(212, 143, 56) 50%, rgb(255, 165, 51) 50%, rgb(255, 165, 51) 75%, rgb(212, 143, 56) 75%, rgb(212, 143, 56) 100%)'
    };

    const inputFile = useRef(null);
    //upload file
    const uploadFile = function(){
        inputFile.current.click();
    };

    return (
        <React.Fragment>
        <section className="picture-engineer__upload" style={{backgroundColor: color }}>
                <div className="uploader">
                    <input onChange={changeHanlder} id="upload" ref={inputFile} 
                        multiple="" type="file" autoComplete="off" tabIndex="-1" style={{display: "none"}} />

            { selectedFile === "" 
                ? <div onClick={uploadFile} className="upload-file dropzone">
                    <i className="fas fa-cloud-upload-alt"></i>
                    <span>Tải file ở đây</span>
                    <em>hoặc</em>
                    <div>
                        <button style={{color: color}}>Tải file lên</button>
                    </div>
                </div>
                : <div className="process-uploading dropzone">
                    <div className="uploadingFile">
                        <div className="loadingBar" role="progressbar">
                            <span id="process-bar" style={style}>0%</span>
                        </div>
                        <div>
                            <span style={{lineHeight: '1.1'}}>Đang tải lên 1 file </span>
                            <em style={{overflowWrap: 'anywhere', padding: '5px'}}>
                                {selectedFile.name + ' | ' + roundNumberScale(selectedFile.size / 1000000, 1) + " MB"}
                            </em>
                        </div>
                    </div>
                </div>
            }
            </div>
        </section>

        </React.Fragment>
    )
}
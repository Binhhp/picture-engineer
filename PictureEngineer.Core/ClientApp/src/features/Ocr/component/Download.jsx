import React, { useEffect, useState } from "react";
import { Fancybox } from "@fancyapps/ui/src/Fancybox/Fancybox";
import "@fancyapps/ui/dist/fancybox.css";
import { base64ToArrayBuffer, saveByteArray } from "../../../utils/binaryStringToArrayBuffer/base64ToArrayBuffer";

export default function Download({ color, image, dataOCR }){

    const [value, setValue] = useState(dataOCR.Text);
    const handleChange = (event) => {
        setValue(event.target.value);
    };

    const images = dataOCR.ImageDetector;
    const tables = images.filter(x => x.ClassLabels === "table");
    const imagesDetector = images.filter(x => x.ClassLabels === "natural image");
    const math = images.filter(x => x.ClassLabels === "math expression");

    const dataDownload = dataOCR.Result;

    const download = function () {
        var sampleArr = base64ToArrayBuffer(dataDownload);
        saveByteArray(`ocr.docx`, sampleArr, 'docx');
    };

    useEffect(() => {
        Fancybox.bind("[data-fancybox]", {
            showClass: "fancybox-fadeIn",
            showLoading: true,
            animated: true,
            hideScrollbar: true,
            closeButton: 'inside',
          });
    });

    return (
        <section className="picture-engineer__upload" style={{ backgroundColor: color }}>
      
            <div className="download-file">
                <div className="orgin">
                    Ảnh gốc
                </div>
                <div className="result">
                    <div className="infomation">
                        {tables.length > 0 ? <span>Bảng: {tables[0].Images.length}</span> : ""}
                        {imagesDetector.length > 0 ? <span>Ảnh: {imagesDetector[0].Images.length}</span> : ""}
                        {math.length > 0 ? <span>Biểu thức: {math[0].Images.length}</span> : ""}
                    </div>
                    <div className="down-file">
                        <button onClick={() => download()} className="btn-download" style={{color: color}}>
                            <i className="fas fa-download"></i>&nbsp;Tải xuống
                        </button>
                    </div>
                </div>
            </div>
            <div className="picture-engineer_ocr">
                <div data-fancybox="gallery"
                    data-src={image}
                    className="zoom-wrapper">
                    <img src={image} alt="Ảnh gốc"/>
                </div>
                <textarea value={value} onChange={handleChange}></textarea>
            </div>
            <div className="result-processed">
                <div className="d-flex">
                    {
                        images.map((item, i) => (
                            item.Images.map((image, i) => (
                                <div key={`image-${i}`} className="result-image-ocr  text-center">
                                    <div className="ocr-labels">{item.ClassLabels }</div>
                                    <div
                                        data-fancybox="gallery"
                                        data-src= {`data:image/png;base64, ${image}`}
                                        className="zoom-wrapper">
                                        <img src={`data:image/png;base64, ${image}`} alt={item.ClassLabels} />
                                    </div>
                                </div>
                            ))
                        ))
                    }
                </div>
            </div>
        </section>
    )
}
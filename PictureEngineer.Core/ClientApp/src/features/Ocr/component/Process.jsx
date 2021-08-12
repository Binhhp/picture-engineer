import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import ocrImageScan from "../../../redux/apis/OCR/ocrImageScan";
import ProcessFile from "../../Service/utils/ProcessFile";
import Loading from "../../Service/Loading/Loading";
import React from "react";
import { downloadFileStorage } from "../../../apis/upload-file/upload";
import Download from "./Download";

export default function Process({ back, file, colorService }) {

    const ocr = useSelector(state => state.ocr);
    const [localImage, setLocalImage] = useState("");

    const dispatch = useDispatch();

    const processFile = async function () {

        if (file.fileSize > 25) return alert("Dung lượng file cần nhỏ hơn 20 MB")
        const result = await downloadFileStorage(file.localPath);
        if (result.success) {
            var language = document.getElementById('language');

            dispatch(ocrImageScan(result.data, language.value, file.token));
            setLocalImage(result.data);
        }
        else {
            return;
        }
    };

    return Object.keys(ocr.data).length > 0
        ? <Download image={localImage} dataOCR={ocr.data} color={colorService} />
        : ocr.loading
            ? <Loading color={colorService} />
            : <ProcessFile 
                    file={file}
                    back={back}
                    colorService={colorService}
                    processFile={processFile}>

                    <div className="process-content">
                        <span>Chọn ngôn ngữ:</span>
                        <select className="form-select language" id="language">
                            <option value="eng">English</option>
                            <option value="vie">Vietnamese</option>
                        </select>
                    </div>

                </ProcessFile>
}



import { useDispatch, useSelector } from "react-redux";
import { toast } from "react-toastify";
import { downloadFileStorage } from "../../../apis/upload-file/upload";
import { base64ToArrayBuffer, saveByteArray } from "../../../utils/binaryStringToArrayBuffer/base64ToArrayBuffer";
import Download from "./Download";
import ProcessFile from "../../Service/utils/ProcessFile";
import Loading from "../../Service/Loading/Loading";
import React from "react";
import { docxToPdf } from "../../../redux/apis/PDF/docxToPdf";

export default function Process({ back, file, colorService }) {

    const docx = useSelector(state => state.docxToPdf);

    const dispatch = useDispatch();

    const processFile = async function () {

        const result = await downloadFileStorage(file.localPath);

        if (result.success) {
            dispatch(docxToPdf(result.data));
        }
        else {
            return toast.error("Internal server error");
        }
    };

    const download = function () {
        var sampleArr = base64ToArrayBuffer(docx.data);
        var mimeType = "pdf";
        saveByteArray(`${file.fileName.split('.docx')}.${mimeType}`, sampleArr, mimeType);
    }

    return docx.data !== ""
        ? <Download download={download} color={colorService} />
        : docx.loading
            ? <Loading color={colorService} />
            : <ProcessFile
                    file={file}
                    colorService={colorService}
                    processFile={processFile}
                    back={back}>
                </ProcessFile>
}



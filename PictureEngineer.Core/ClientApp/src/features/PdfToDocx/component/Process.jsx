
import { useDispatch, useSelector } from "react-redux";
import { toast } from "react-toastify";
import { downloadFileStorage } from "../../../apis/upload-file/upload";
import { pdfConvertFormat } from "../../../redux/apis/PDF/pdfConvert";
import { base64ToArrayBuffer, saveByteArray } from "../../../utils/binaryStringToArrayBuffer/base64ToArrayBuffer";
import ProcessFile from "../../Service/utils/ProcessFile";
import Loading from "../../Service/Loading/Loading";
import React from "react";
import Download from "./Download";

export default function Process({ back, file, colorService }) {

    const pdf = useSelector(state => state.pdfFormat);

   /* const [isCheck, setCheck] = useState({ excel: false, docx: true })*/

    //const toggle = function () {
    //    setCheck({
    //        excel: !isCheck.excel,
    //        docx: !isCheck.docx
    //    });
    //};

    const dispatch = useDispatch();

    const processFile = async function () {

        if (file.fileSize > 25) return toast.error("Dung lượng file cần nhỏ hơn 20 MB");

        const result = await downloadFileStorage(file.localPath);

        if (result.success) {
            dispatch(pdfConvertFormat(result.data, 'docx'));
        }
        else {
            return toast.error("Internal server error");
        }
    };

    const download = function () {
        var sampleArr = base64ToArrayBuffer(pdf.data);
        var mimeType = "docx";
        /*if (isCheck.excel === true) mimeType = "xls";*/
        saveByteArray(`${file.fileName.split('.pdf')}.${mimeType}`, sampleArr, mimeType);
    }

    return pdf.data !== ""
        ? <Download download={download} color={colorService} />
        : pdf.loading
            ? <Loading color={colorService} />
            : <ProcessFile
                    file={file}
                    colorService={colorService}
                    processFile={processFile}
                    back={back}>
                        {/*<div className="convert-pdf">*/}
                        {/*        <div className="checkbox-pdf" onClick={() => toggle()}>*/}
                        {/*            <input type="checkbox" checked={isCheck.excel} onChange={() => toggle} />*/}
                        {/*            <label>Excel</label>*/}
                        {/*        </div>*/}
                        {/*        <div className="checkbox-pdf" onClick={() => toggle()}>*/}
                        {/*            <input type="checkbox" checked={isCheck.docx} onChange={() => toggle} />*/}
                        {/*            <label>Docx</label>*/}
                        {/*        </div>*/}
                        {/*    </div>*/}
                </ProcessFile>
}



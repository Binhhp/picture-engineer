import { useDispatch, useSelector } from 'react-redux';
import { downloadFileStorage } from '../../../apis/upload-file/upload';
import { splitPdf } from '../../../redux/apis/PDF/splitPdf';
import { base64ToArrayBuffer, saveByteArray } from '../../../utils/binaryStringToArrayBuffer/base64ToArrayBuffer';
import Download from './Download';
import { useEffect, useState } from 'react';
import { requestPost } from '../../../apis/axiosSetting/axiosClient';
import { getURLBlobLocalPdf } from '../hook/useSplitPdf';
import SettingSplit from './SettingSplit';
import React from 'react';
import { toast } from 'react-toastify';
import ProcessFile from "../../Service/utils/ProcessFile";
import Loading from "../../Service/Loading/Loading";

export default function Process({ back, file, colorService}){

    const pdf = useSelector(state => state.splitPdf);
    //split start number page to end number page
    const [startPage, setStartPage] = useState(1);
    const [endPage, setEndPage] = useState(1);
    //Get local path in storage
    const [localPath, setLocalPath] = useState("");
    //Get size page pdf
    const [pageSizePDF, setPageSizePDF] = useState(1);
    // View pdf 
    const [startPdfViewer, setStartPdfViewer] = useState("");
    const [endPdfViewer, setEndPdfViewer] = useState("");
    
    const dispatch = useDispatch();
    //process file - split pdf from start page to end page
    const processFile = async function(){
        if (localPath) {

            if (startPage > endPage) return toast.error("Chọn lại số trang!");

            else return dispatch(splitPdf(localPath, startPage, endPage, file.token));
        }
        else{  
            return toast.error("Internal server error");
        }
    };
    //get pageSize condition uploaded file in storage
    const pageNumber = async function () {

        if (Object.keys(file).length > 0) {

            const result = await downloadFileStorage(file.localPath);
            if (result.success) {
                await setLocalPath(result.data);
   
                const filePath = {
                    "FilePath": result.data
                };
                //get page size
                const response = await requestPost('/api/pdfs', filePath);
                let size = 0;
                if (response.code <= 204) {
                    size = response.data;
                }

                await setPageSizePDF(size);

                if (startPdfViewer === "" && endPdfViewer === "") {
                    //set loading view pdf with page start 1 and page end 1
                    var urlLocalPathPdf = await getURLBlobLocalPdf(result.data, 1);

                    await setStartPdfViewer(urlLocalPathPdf);
                    await setEndPdfViewer(urlLocalPathPdf);
                }
                return;
            }
            else {
                return;
            }
        }
    };

    useEffect(pageNumber, [pageNumber, setPageSizePDF, setLocalPath])

    //download file pdf splited
    const download = function(){
        var sampleArr = base64ToArrayBuffer(pdf.data);
        saveByteArray(`${file.fileName.split('.pdf')}-copy.pdf`, sampleArr, 'pdf');
    }

    //button minus in page start
    const minus = async function (isStartOrEnd) {

        if (isStartOrEnd === true) {

            const start = startPage - 1 < 1 ? 1 : startPage - 1;
            await setStartPage(start);

            //show view pdf in page start
            var urlLocalStartPage = await getURLBlobLocalPdf(localPath, start);
            await setStartPdfViewer(urlLocalStartPage);
        }
        else {

            const end = endPage - 1 < 1 ? 1 : endPage - 1;
            await setEndPage(end);

            //show view pdf in page end
            var urlLocalEndPage = await getURLBlobLocalPdf(localPath, end);
            await setEndPdfViewer(urlLocalEndPage);
        }
    }

    //button plus in page end
    const plus = async function (isStartOrEnd) {

        if (isStartOrEnd === true) {

            const start = startPage + 1 > pageSizePDF ? pageSizePDF : startPage + 1;
            await setStartPage(start);

            //show view pdf in page start
            var urlLocalStartPage = await getURLBlobLocalPdf(localPath, start);
            await setStartPdfViewer(urlLocalStartPage);
        }
        else {

            const end = endPage + 1 > pageSizePDF ? pageSizePDF : endPage + 1;
            await setEndPage(end);

            //show view pdf in page end
            var urlLocalEndPage = await getURLBlobLocalPdf(localPath, end);
            await setEndPdfViewer(urlLocalEndPage);
        }
    }

    return pdf.data !== ""
        // download new pdf splited
        ? <Download download={download} color={colorService} />
        // Process split pdf from start page to end page
        : pdf.loading ? <Loading color={colorService} />
            : <ProcessFile
                file={file}
                colorService={colorService}
                back={back}
                processFile={processFile}>

                <div className="process-content-split">
                    <SettingSplit colorService={colorService}
                        startPdfViewer={startPdfViewer}
                        startPage={startPage}
                        endPdfViewer={endPdfViewer}
                        endPage={endPage}
                        minus={minus}
                        plus={plus} />
                </div>
            </ProcessFile>
}




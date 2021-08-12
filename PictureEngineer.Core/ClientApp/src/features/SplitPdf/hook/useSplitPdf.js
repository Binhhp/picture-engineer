import { toast } from "react-toastify";
import { requestPost } from "../../../apis/axiosSetting/axiosClient";
import { base64ToArrayBuffer, blobMimeArrayBuffer } from "../../../utils/binaryStringToArrayBuffer/base64ToArrayBuffer";

async function getURLBlobLocalPdf (url, pageNumber){

    const page = [pageNumber];
    const input = {
        "PageNumber": page,
        "FilePath": url
    };

    const response = await requestPost('/api/pdfs/pages', input);
    if (response.code > 204) {
        toast.error("Error load pdf.");
        return "";
    }

    const pdf = response.data;

    var sampleArr = base64ToArrayBuffer(pdf);

    var blob = blobMimeArrayBuffer(sampleArr, 'pdf');
    var urlViewerData = window.URL.createObjectURL(blob);
    return urlViewerData;
};

export { getURLBlobLocalPdf }
import {
    fetchPdfConvertRequest,
    fetchPdfConvertSuccess,
    fetchPdfConvertFailure
} from "../../actions/PDF/PdfConvertAction";

import { toast } from 'react-toastify';
import { requestPost } from "../../../apis/axiosSetting/axiosClient";

export const pdfConvertFormat = (filePath, format) => {
    return async dispatch => {
        dispatch(fetchPdfConvertRequest());

        var input = {
            'FilePath': filePath,
            'FileFormatConvert': format
        };

        const request = await requestPost('/api/pdfs/convert', input);

        if (request.code <= 204) {
            return dispatch(fetchPdfConvertSuccess(request.data));
        }
        else {
            toast.error(request.message);
            dispatch(fetchPdfConvertFailure(request.message));
            return;
        }
    }
}
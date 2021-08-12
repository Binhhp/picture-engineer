import {
    fetchDocxToPdfRequest,
    fetchDocxToPdfSuccess,
    fetchDocxToPdfFailure
} from "../../actions/PDF/DocxToPdfAction";

import { toast } from 'react-toastify';
import { requestPost } from "../../../apis/axiosSetting/axiosClient";

export const docxToPdf = (filePath) => {
    return async dispatch => {
        dispatch(fetchDocxToPdfRequest());

        var input = {
            'FilePath': filePath
        };

        const request = await requestPost('/api/pdfs/docx', input);

        if (request.code <= 204) {
            return dispatch(fetchDocxToPdfSuccess(request.data));
        }
        else {
            toast.error(request.message);
            dispatch(fetchDocxToPdfFailure(request.message));
            return;
        }
    }
}
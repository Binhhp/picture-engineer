import {
    fetchSplitPdfRequest,
    fetchSplitPdfSuccess,
    fetchSplitPdfFailure
} from "../../actions/PDF/SplitPdfAction";
import { toast } from 'react-toastify';
import { requestPost } from "../../../apis/axiosSetting/axiosClient";

export const splitPdf = (filePath, startPage, endPage) => {
    return async dispatch => {
        dispatch(fetchSplitPdfRequest());

        var input = {
            'FilePath': filePath,
            'StartPage': startPage,
            'EndPage': endPage
        };

        const request = await requestPost('/api/pdfs/split', input);

        if (request.code <= 204) {

            return dispatch(fetchSplitPdfSuccess(request.data));

        }
        else {
           
            toast.error(request.message);
            dispatch(fetchSplitPdfFailure(request.message));
            return;
        }
    }
}
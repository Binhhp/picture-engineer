import { 
    fetchOcrRequest, 
    fetchOcrSuccess, 
    fetchOcrFailure
} from "../../actions/OCR/OcrAction"

import { requestPost } from "../../../apis/axiosSetting/axiosClient";
import { toast } from "react-toastify";
//Ocr image scanned
export default function ocrImageScan(filePath, language) {
    return async (dispatch) => {

        dispatch(fetchOcrRequest());

        var ocr = {
            'FilePath': filePath,
            'Language': language
        };

        const request = await requestPost('/api/ocr', ocr);

        if (request.code <= 204) {

            if (request.data === "") {

                dispatch(fetchOcrFailure(request.message));
                toast.error("😭 Sorrry! Quá tải server 😢 Bạn hãy thử lại sau.");
                return;

            }
            return dispatch(fetchOcrSuccess(request.data));
            
        }
        else {
            toast.error(request.message);
            dispatch(fetchOcrFailure(request.message));
            return;
        }
    }
}

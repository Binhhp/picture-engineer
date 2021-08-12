export const FETCH_DOCX_TO_PDF_REQUEST = "FETCH_DOCX_TO_PDF_REQUEST";
export const FETCH_DOCX_TO_PDF_SUCCESS = "FETCH_DOCX_TO_PDF_SUCCESS";
export const FETCH_DOCX_TO_PDF_FAILURE = "FETCH_DOCX_TO_PDF_FAILURE";
export const FETCH_DOCX_TO_PDF_CLEAR_DATA = "FETCH_DOCX_TO_PDF_CLEAR_DATA";

export const fetchDocxToPdfRequest = () => {
    return{
        type: FETCH_DOCX_TO_PDF_REQUEST
    }
}
export const fetchDocxToPdfClearData = () => {
    return{
        type: FETCH_DOCX_TO_PDF_CLEAR_DATA
    }
}

export const fetchDocxToPdfSuccess = (file) => {
    
    return{
        type: FETCH_DOCX_TO_PDF_SUCCESS,
        payload: file
    }
}

export const fetchDocxToPdfFailure = (error) => {
    return{
        type: FETCH_DOCX_TO_PDF_FAILURE,
        payload: error
    }
}
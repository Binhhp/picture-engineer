export const FETCH_PDF_CONVERT_REQUEST = "FETCH_PDF_CONVERT_REQUEST"
export const FETCH_PDF_CONVERT_SUCCESS = "FETCH_PDF_CONVERT_SUCCESS"
export const FETCH_PDF_CONVERT_FAILURE = "FETCH_PDF_CONVERT_FAILURE"
export const FETCH_PDF_CONVERT_CLEAR_DATA = "FETCH_PDF_CONVERT_CLEAR_DATA"
export const fetchPdfConvertRequest = () => {
    return{
        type: FETCH_PDF_CONVERT_REQUEST
    }
}
export const fetchPdfConvertClearData = () => {
    return{
        type: FETCH_PDF_CONVERT_CLEAR_DATA
    }
}

export const fetchPdfConvertSuccess = (file) => {
    
    return{
        type: FETCH_PDF_CONVERT_SUCCESS,
        payload: file
    }
}

export const fetchPdfConvertFailure = (error) => {
    return{
        type: FETCH_PDF_CONVERT_FAILURE,
        payload: error
    }
}
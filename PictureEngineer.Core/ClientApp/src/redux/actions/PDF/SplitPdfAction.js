export const FETCH_SPLIT_PDF_REQUEST = "FETCH_SPLIT_PDF_REQUEST"
export const FETCH_SPLIT_PDF_SUCCESS = "FETCH_SPLIT_PDF_SUCCESS"
export const FETCH_SPLIT_PDF_FAILURE = "FETCH_SPLIT_PDF_FAILURE"
export const FETCH_SPLIT_PDF_CLEAR_DATA = "FETCH_SPLIT_PDF_CLEAR_DATA"
export const fetchSplitPdfRequest = () => {
    return{
        type: FETCH_SPLIT_PDF_REQUEST
    }
}
export const fetchSplitPdfClearData = () => {
    return{
        type: FETCH_SPLIT_PDF_CLEAR_DATA
    }
}
export const fetchSplitPdfSuccess = (file) => {
    
    return{
        type: FETCH_SPLIT_PDF_SUCCESS,
        payload: file
    }
}

export const fetchSplitPdfFailure = (error) => {
    return{
        type: FETCH_SPLIT_PDF_FAILURE,
        payload: error
    }
}
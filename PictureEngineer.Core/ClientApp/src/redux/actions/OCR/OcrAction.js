export const FETCH_OCR_REQUEST = "FETCH_OCR_REQUEST"
export const FETCH_OCR_SUCCESS = "FETCH_OCR_SUCCESS"
export const FETCH_OCR_FAILURE = "FETCH_OCR_FAILURE"
export const FETCH_OCR_CLEAR_DATA = "FETCH_OCR_CLEAR_DATA"
export const fetchOcrRequest = () => {
    return{
        type: FETCH_OCR_REQUEST
    }
}

export const fetchOcrClearData = () => {
    return{
        type: FETCH_OCR_CLEAR_DATA
    }
}

export const fetchOcrSuccess = (file) => {
    
    return{
        type: FETCH_OCR_SUCCESS,
        payload: file
    }
}

export const fetchOcrFailure = (error) => {
    return{
        type: FETCH_OCR_FAILURE,
        payload: error
    }
}
import {
    FETCH_OCR_REQUEST,
    FETCH_OCR_SUCCESS,
    FETCH_OCR_FAILURE,
    FETCH_OCR_CLEAR_DATA
} from '../../actions/OCR/OcrAction'

const initialState = {
    loading: false,
    error: '',
    data: {}
}

export function ocrReducer(state = initialState, action){

    switch(action.type)
    {
        case FETCH_OCR_REQUEST:
            return{
                ...state,
                loading: true
            }
        case FETCH_OCR_CLEAR_DATA:
            return{
               ...initialState
            }


        case FETCH_OCR_SUCCESS:

            return {
                loading: false,
                data: action.payload,
                error: ''
            };

        case FETCH_OCR_FAILURE:
            return{
                loading: false,
                error: action.payload,
                data: ''
            }

        default:
            return state
    }
}
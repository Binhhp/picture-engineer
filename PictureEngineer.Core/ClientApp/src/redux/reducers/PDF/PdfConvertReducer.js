import {
    FETCH_PDF_CONVERT_REQUEST,
    FETCH_PDF_CONVERT_SUCCESS,
    FETCH_PDF_CONVERT_FAILURE,
    FETCH_PDF_CONVERT_CLEAR_DATA
} from '../../actions/PDF/PdfConvertAction'

const initialState = {
    loading: false,
    error: '',
    data: ''
}

export const PdfConvertReducer = (state = initialState, action) => {

    switch(action.type)
    {
        case FETCH_PDF_CONVERT_REQUEST:
            return{
                ...state,
                loading: true
            }
            case FETCH_PDF_CONVERT_CLEAR_DATA:
                return{
                    ...initialState
                }
        case FETCH_PDF_CONVERT_SUCCESS:

            return {
                loading: false,
                data: action.payload,
                error: ''
            };

        case FETCH_PDF_CONVERT_FAILURE:
            return{
                loading: false,
                error: action.payload,
                data: ''
            }

        default:
            return state
    }
}
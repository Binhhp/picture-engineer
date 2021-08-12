import {
    FETCH_SPLIT_PDF_REQUEST,
    FETCH_SPLIT_PDF_SUCCESS,
    FETCH_SPLIT_PDF_FAILURE,
    FETCH_SPLIT_PDF_CLEAR_DATA
} from '../../actions/PDF/SplitPdfAction'

const initialState = {
    loading: false,
    error: '',
    data: ''
}

export const splitPdfReducer = (state = initialState, action) => {

    switch(action.type)
    {
        case FETCH_SPLIT_PDF_REQUEST:
            return{
                ...state,
                loading: true
            }
        case FETCH_SPLIT_PDF_CLEAR_DATA:
            return{
                ...initialState
            }
        case FETCH_SPLIT_PDF_SUCCESS:

            return {
                loading: false,
                data: action.payload,
                error: ''
            };

        case FETCH_SPLIT_PDF_FAILURE:
            return{
                loading: false,
                error: action.payload,
                data: ''
            }

        default:
            return state
    }
}
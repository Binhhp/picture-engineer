import {
    FETCH_DOCX_TO_PDF_REQUEST,
    FETCH_DOCX_TO_PDF_SUCCESS,
    FETCH_DOCX_TO_PDF_FAILURE,
    FETCH_DOCX_TO_PDF_CLEAR_DATA
} from '../../actions/PDF/DocxToPdfAction'

const initialState = {
    loading: false,
    error: '',
    data: ''
}

export const DocxToPdfReducer = (state = initialState, action) => {

    switch(action.type)
    {
        case FETCH_DOCX_TO_PDF_REQUEST:
            return{
                ...state,
                loading: true
            }
            case FETCH_DOCX_TO_PDF_CLEAR_DATA:
                return{
                    ...initialState
                }
        case FETCH_DOCX_TO_PDF_SUCCESS:

            return {
                loading: false,
                data: action.payload,
                error: ''
            };

        case FETCH_DOCX_TO_PDF_FAILURE:
            return{
                loading: false,
                error: action.payload,
                data: ''
            }

        default:
            return state
    }
}
import {
    FETCH_FAQs_REQUEST,
    FETCH_FAQs_SUCCESS,
    FETCH_FAQs_FAILURE
} from '../../actions/Services/FAQsType'

const initialState = {
    success: false,
    error: '',
    data: []
}

export const faqsReducer = (state = initialState, action) => {

    switch(action.type)
    {
        case FETCH_FAQs_REQUEST:
            return{
                ...state
            }

        case FETCH_FAQs_SUCCESS:
            return {
                success: true,
                data: action.payload,
                error: ''
            };

        case FETCH_FAQs_FAILURE:
            return{
                success: false,
                error: action.payload,
                data: []
            }

        default:
            return state
    }
}
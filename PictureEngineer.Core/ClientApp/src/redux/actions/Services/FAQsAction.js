import { 
    FETCH_FAQs_REQUEST, 
    FETCH_FAQs_SUCCESS, 
    FETCH_FAQs_FAILURE
} from './FAQsType'

export const fetchFAQsRequest = () => {
    return{
        type: FETCH_FAQs_REQUEST
    }
}

export const fetchFAQsSuccess = (FAQss) => {
    return{
        type: FETCH_FAQs_SUCCESS,
        payload: FAQss
    }
}

export const fetchFAQsError = (error) => {
    return{
        type: FETCH_FAQs_FAILURE,
        payload: error
    }
}

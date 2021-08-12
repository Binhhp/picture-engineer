import {
    FETCH_SERVICE_DETAIL_REQUEST,
    FETCH_SERVICE_DETAIL_SUCCESS,
    FETCH_SERVICE_DETAIL_FAILURE
} from '../../actions/Services/ServiceTypes'

const initialState = {
    success: false,
    data: {
        FAQs: [],
        UserGuide: ""
    },
    error: ''
}

export const serviceDetailReducer = (state = initialState, action) => {

    switch(action.type)
    {
        case FETCH_SERVICE_DETAIL_REQUEST:
            return{
                success: false,
                data: {
                    FAQs: [],
                    UserGuide: ""
                },
                error: ''
            }

        case FETCH_SERVICE_DETAIL_SUCCESS:
            return{
                success: true,
                data: action.payload,
                error: ''
            }

        case FETCH_SERVICE_DETAIL_FAILURE:
            return{
                success: false,
                data: {},
                error: action.payload
            }

        default:
            return state
    }
}
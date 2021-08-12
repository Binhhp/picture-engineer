import {
    FETCH_SERVICE_REQUEST,
    FETCH_SERVICE_SUCCESS,
    FETCH_SERVICE_FAILURE
} from '../../actions/Services/ServiceTypes'

const initialState = {
    success: false,
    error: '',
    data: []
}

export const serviceReducer = (state = initialState, action) => {

    switch(action.type)
    {
        case FETCH_SERVICE_REQUEST:
            return{
                ...state
            }

        case FETCH_SERVICE_SUCCESS:
            return {
                success: true,
                data: action.payload,
                error: ''
            };

        case FETCH_SERVICE_FAILURE:
            return{
                success: false,
                error: action.payload,
                data: []
            }

        default:
            return state
    }
}
import { 
    FETCH_SERVICE_REQUEST, 
    FETCH_SERVICE_SUCCESS, 
    FETCH_SERVICE_FAILURE, 
    FETCH_SERVICE_DETAIL_REQUEST,
    FETCH_SERVICE_DETAIL_SUCCESS, 
    FETCH_SERVICE_DETAIL_FAILURE 
} from './ServiceTypes'

export const fetchServiceRequest = () => {
    return{
        type: FETCH_SERVICE_REQUEST
    }
}

export const fetchServiceSuccess = (services) => {
    return{
        type: FETCH_SERVICE_SUCCESS,
        payload: services
    }
}

export const fetchServiceError = (error) => {
    return{
        type: FETCH_SERVICE_FAILURE,
        payload: error
    }
}

export const fetchServiceDetailRequest = () => {
    return{
        type: FETCH_SERVICE_DETAIL_REQUEST
    }
}

export const fetchServiceDetailSuccess = (service) => {
    return{
        type: FETCH_SERVICE_DETAIL_SUCCESS,
        payload: service
    }
}

export const fetchServiceDetailError = (error) => {
    return{
        type: FETCH_SERVICE_DETAIL_FAILURE,
        payload: error
    }
}
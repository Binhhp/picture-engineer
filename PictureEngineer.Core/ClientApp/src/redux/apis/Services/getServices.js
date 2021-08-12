
import { requestGet } from '../../../apis/axiosSetting/axiosClient';
import {
    fetchFAQsError,
    fetchFAQsRequest,
    fetchFAQsSuccess
} from '../../actions/Services/FAQsAction';

import {
    fetchServiceRequest,
    fetchServiceSuccess,
    fetchServiceError,
    fetchServiceDetailRequest,
    fetchServiceDetailSuccess,
    fetchServiceDetailError
} from '../../actions/Services/ServiceAction'

const getServices = function() {
    return async dispatch => {
        dispatch(fetchServiceRequest());
        const response = await requestGet('/api/services');
        if (response.code === 200) {
            dispatch(fetchServiceSuccess(response.data));
        }
        else {
            dispatch(fetchServiceError(response.error));
        }
    }
}

const getDetailService = function (meta){
    return async dispatch => {
        dispatch(fetchServiceDetailRequest());
        const response = await requestGet('/api/services/' + meta);
        if (response.code === 200) {
            dispatch(fetchServiceDetailSuccess(response.data));
        }
        else {
            dispatch(fetchServiceDetailError(response.error));
        }
    }
}

const getFAQs = function () {
    return async dispatch => {
        dispatch(fetchFAQsRequest());
        const response = await requestGet('/api/faqs');
        if (response.code === 200) {
            dispatch(fetchFAQsSuccess(response.data));
        }
        else {
            dispatch(fetchFAQsError(response.error));
        }
    }
}

export { getServices, getDetailService, getFAQs }
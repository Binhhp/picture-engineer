import {
    FETCH_BLOG_REQUEST,
    FETCH_BLOG_FAILURE,
    FETCH_BLOG_SUCCESS,
    FETCH_BLOG_DETAIL_FAILURE,
    FETCH_BLOG_DETAIL_SUCCESS,
    FETCH_BLOG_DETAIL_REQUEST
} from '../../actions/Blogs/BlogType'

const initialState = {
    loading: false,
    data: [],
    error: ''
}

export const blogReducer = (state = initialState, action) => {
    switch(action.type){
        case FETCH_BLOG_REQUEST:
            return{
                ...state,
                loading: true
            }
        
        case FETCH_BLOG_SUCCESS:
            return{
                loading: false,
                data: action.payload,
                error: ''
            }

        case FETCH_BLOG_FAILURE:
            return{
                loading: false,
                data: [],
                error: action.payload
            }

        default: 
            return state;
    }
}

const initDetail = {
    loading: false,
    data: {},
    error: ""
};

export const blogDetailReducer = (state = initDetail, action) => {
    switch (action.type) {
        case FETCH_BLOG_DETAIL_REQUEST:
            return {
                ...state,
                loading: true
            }

        case FETCH_BLOG_DETAIL_SUCCESS:
            return {
                loading: false,
                data: action.payload,
                error: ''
            }

        case FETCH_BLOG_DETAIL_FAILURE:
            return {
                loading: false,
                data: {},
                error: action.payload
            }

        default:
            return state;
    }
}
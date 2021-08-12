import {
    FETCH_BLOG_REQUEST,
    FETCH_BLOG_SUCCESS,
    FETCH_BLOG_FAILURE,
    FETCH_BLOG_DETAIL_REQUEST,
    FETCH_BLOG_DETAIL_SUCCESS,
    FETCH_BLOG_DETAIL_FAILURE
} from './BlogType';

export const fetchBlogRequest = () => {
    return{
        type: FETCH_BLOG_REQUEST
    }
}

export const fetchBlogSuccess = (blogs) => {
    return{
        type: FETCH_BLOG_SUCCESS,
        payload: blogs
    }
}

export const fetchBlogError = (errors) => {
    return{
        type: FETCH_BLOG_FAILURE,
        payload: errors
    }
}

export const fetchBlogDetailRequest = () => {
    return {
        type: FETCH_BLOG_DETAIL_REQUEST
    }
}

export const fetchBlogDetailSuccess = (blog) => {
    return {
        type: FETCH_BLOG_DETAIL_SUCCESS,
        payload: blog
    }
}

export const fetchBlogDetailError = (errors) => {
    return {
        type: FETCH_BLOG_DETAIL_FAILURE,
        payload: errors
    }
}


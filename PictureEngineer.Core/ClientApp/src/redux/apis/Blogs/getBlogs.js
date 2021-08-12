import { fetchBlogDetailError, fetchBlogDetailRequest, fetchBlogDetailSuccess, fetchBlogError, fetchBlogRequest, fetchBlogSuccess } from '../../actions/Blogs/BlogAction'
import { requestGet } from '../../../apis/axiosSetting/axiosClient';
import axios from 'axios';

export const getBlogs = (serviceID) => {
    var path = '/api/blogs';

    if(serviceID !== null && serviceID !== undefined) {
        path += `/${serviceID}`
    }

    return async dispatch => {

        dispatch(fetchBlogRequest());

        const blogs = await requestGet(path);
        if (blogs.code === 200) {
            dispatch(fetchBlogSuccess(blogs.data));
        }
        else {
            dispatch(fetchBlogError(blogs.error));
        }
    }
}

export const getBlogDetail = (meta) => {
    var path = '/api/blogs';
    if (meta !== null && meta !== undefined) {
        path += `/${meta}/detail`
    }

    return async dispatch => {

        dispatch(fetchBlogDetailRequest());

        const options = {
            "Accept": "application/json"
        };
    
        await axios.get(path, {
            headers: options
        }).then(response => {
            
            dispatch(fetchBlogDetailSuccess(response.data))
        })
        .catch(error => {
           dispatch(fetchBlogDetailError(error.message))
        });
    }
}
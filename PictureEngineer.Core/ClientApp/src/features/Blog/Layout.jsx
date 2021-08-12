import './css/blog.css';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { getBlogs } from '../../redux/apis/Blogs/getBlogs';

export default function Layout({ children }){
    const dispatch = useDispatch();

    useEffect(function(){
        dispatch(getBlogs());
    }, [dispatch])

    return (
        <div className="picture-engineer__blog">
            <div className="main-container" id="main-B">
                {children}
            </div>
        </div>
    );
}

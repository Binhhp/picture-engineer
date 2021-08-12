import './detail.css';
import React from "react";
import { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { getBlogDetail } from '../../../redux/apis/Blogs/getBlogs';
import Layout from '../Layout';
import BlogOffer from './BlogOffer';

export default function Detail() {

    const { meta } = useParams();
    const blog = useSelector(state => state.blogDetail.data);
    const blogList = useSelector(state => state.blogs.data);
    const dispatch = useDispatch();

    const setContent = (content) => {
        const ele = document.querySelector(".blog .blog-content");
        if (ele !== undefined && ele !== null) {
            ele.innerHTML = content;
        }
    }
    useEffect(function () {
        dispatch(getBlogDetail(meta));
    }, [dispatch, meta]);

    return (
        <Layout>
            <div className="blog">
                <div className="blog-d">
                    <div className="d-flew">
                        <div className="blog-detail">
                            <img src={blog.ImagePath} alt={blog.Title} className="blog-img" />
                            <h5 className="blog-title">{blog.Title}</h5>
                            <p className="blog-description">{blog.Description}</p>
                            <p className="blog-content">{setContent(blog.Contents)}</p>
                        </div>
                        <div className="list-blog">
                            <h5>Bài viết khác</h5>
                            <BlogOffer blogs={blogList.filter(x => x.MetaTitle !== meta).slice(0, 10)}></BlogOffer>
                        </div>
                    </div>
                </div>
            </div>
        </Layout>
    )
}

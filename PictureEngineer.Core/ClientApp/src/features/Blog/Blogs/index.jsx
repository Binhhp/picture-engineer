
import BlogArray from './BlogArray.jsx';
import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { getBlogs } from '../../../redux/apis/Blogs/getBlogs';
import Layout from '../Layout';

export default function Blog(){
    const dispatch = useDispatch();
    const services = useSelector(state => state.services.data);
    const [isHiddenSort, setHiddenSort] = useState(true);

    const searchBlog = (id) => {
        dispatch(getBlogs(id))
    };

    return (
       <Layout>
            <div className="main-header">
                <div className="heading"><span>Bài viết</span></div>
                <div className="sort">
                    <span onClick={() => setHiddenSort(!isHiddenSort)}>
                        Sắp xếp&nbsp;<i className="fas fa-sort-down"></i>
                    </span>
                    <ul className={isHiddenSort ? "sort-content hidden" : "sort-content"}>
                        {
                            services.map((item, i) => (
                                <li key={i} className="sort-item" onClick={() => searchBlog(item.Id)}>{item.Name}</li>
                            ))
                        }
                        <li className="sort-item" onClick={() => searchBlog(null)}>Tất cả</li>
                    </ul>
                </div>
            </div>
            <div className="Mu_B">
                <div className="XRw">
                    <BlogArray />
                </div> 
            </div>
       </Layout>
    );
}

import { Link } from "react-router-dom";
import React from "react";
import Error404 from "../../assets/images/error404.png";
import "./no-match.css";

export default function NoMatch(){

    return(
        <div className="no-match text-center">
            <div className="no-match-img"><img src={Error404} alt="Error 404 No Find Page"/></div>
            <div className="no-match-medium">Ồ men, Xin lỗi chúng tôi không tìm thấy trang này!</div>
            <div className="no-match-small">Đã xảy ra sự cố hoặc trang không tồn tại nữa.</div>
            <div className="no-match-button">
                <Link to="/" className="btn btn-radius btn-focus">
                    <i className="fa fa-chevron-circle-left mr-2"></i>Về trang chủ thôi
                </Link>
            </div>
        </div>
    )
}
import { Link } from 'react-router-dom';
import './css/header.css';
import logo from '../../assets/logo/logo.png';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import * as React from 'react';
export default function Header(){

    const services = useSelector(state => state.services.data);
    const [isHidden, setHidden] = useState(true);

    return (
        <div className="header__wrappter">
        <div className="logo">
            <Link to="/">
                <img src={logo} alt="Logo"/>
            </Link>
        </div>
        <div className="picture-engineer__menu">
            <ul className="menu">
                <li className="menu-item">
                    <Link to="/nhan-dien" className="link-menu">
                        <i className="fa fa-bars"></i> Nhận diện ảnh
                    </Link>
                </li>
                <li className="menu-item" onClick={() => setHidden(!isHidden)}>
                    <span className="link-menu menu-dropdown">
                        <i className="fa fa-bars"></i> PDF
                    </span>
                    <ul className={`menu-child ${isHidden ? "hidden" : "" }`}>
                        {
                            services.filter(x => x.ParentID === 2).map((child, i) => (
                                <li className="menu-child-item" key={'service-' + i}>
                                    <img alt={child.Name} src={child.Icon} />
                                    <Link to={`/${child.Meta}`}>{child.Name}</Link>
                                </li>
                            ))
                        }
                    </ul>
                </li>
                <li className="menu-item">
                    <Link to="/bai-viet" className="link-menu">
                        Bài viết
                    </Link>
                </li>
                <li className="menu-item">
                    <Link to="/dieu-khoan" className="link-menu">
                        Điều khoản và bảo mật
                    </Link>
                </li>
            </ul>
        </div>
    </div>
    )
}
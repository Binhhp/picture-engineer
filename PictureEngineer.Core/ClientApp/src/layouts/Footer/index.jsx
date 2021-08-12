import { Link } from "react-router-dom"
import './footer.css'
import React from "react";
export default function Footer(){

    return (
        <div className="picture-engineer__footer">
            <div className="container">
                <Link to="/" className="logo__footer">Picture Engineer</Link>
                <div className="d-flex contact">
                    <a href="/facebook.com"><i className="fab fa-facebook-f"></i></a>
                    <a href="/facebook.com"><i className="fab fa-instagram"></i></a>
                    <a href="/facebook.com"><i className="fab fa-linkedin-in"></i></a>
                </div>
            </div>
            <footer><p>Copy @ 2021 - Built by binhhp</p></footer>
        </div>
    )
}
import Questions from "../../../component/Questions/component/Questions.jsx";
import '../css/main.css';
import React from "react";
export default function MainContent({ title, description, questions, children }) {
    return (
        <div className="css-picture-engineer">
            <div className="picture-engineer__banner">
                <div className="container banner_engineer">
                    <div className="hero">
                        <div className="title">
                            <h1>{title}</h1>
                            <h2>{description}</h2>
                        </div>
                    </div>
                </div>
            </div>
            {children}
            <Questions questions={questions} />
        </div>
    )
}
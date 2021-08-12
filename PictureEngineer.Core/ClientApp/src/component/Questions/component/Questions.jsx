
import '../css/question.css';
import React from "react";

export default function Questions({questions}){
    const isHiddenQuestion = (key) =>{
        var keyWord = 'faq-' + key;
        var x = document.getElementById(keyWord);
        var y = x.querySelector('.content-question');
        if(y.classList.contains('hidden')){
            x.querySelector('.pick-up').innerHTML = '<i className="fas fa-chevron-down"></i>';
            y.classList.remove('hidden');
        }
        else{
            x.querySelector('.pick-up').innerHTML = '<i className="fas fa-chevron-up"></i>';
            y.classList.add('hidden');
        }
    };

    return(
        <div className="picture-engineer__questions">
            <div className="container">
                <div className="faq-questions_header">
                    <h2>Các câu hỏi thường gặp</h2>
                </div>
                <div className="faq-questions__main">
                    <ul>
                        {
                            questions.length === 0 ? ""
                                : questions.map((q, i) => (
                                    <li id={'faq-' + i} className="close__item" onClick={() => isHiddenQuestion(i)} key={i}>
                                        <div className="question">
                                            <h3>
                                                {q.Question}
                                            </h3>
                                            <div></div>
                                            <div className="pick-up"><i className="fas fa-chevron-up"></i></div>
                                        </div>
                                        <div className="content-question hidden">
                                            <p>{q.Answer}</p>
                                        </div>
                                    </li>  
                                ))
                        }
                    </ul>
                </div>
            </div>
        </div>
    )
}
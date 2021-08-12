import MainContent from '../../layouts/MainLayout/component/MainContent';
import './home.css';
import { useSelector } from 'react-redux';
import React from 'react';
import { Link } from 'react-router-dom';

export default function Home(){
    const services = useSelector(state => state.services.data);
    const faqs = useSelector(state => state.faqs.data);

    return (
        <MainContent title="Nhận diện ảnh chụp tài liệu và xử lý PDF" description="" questions={faqs.filter(x => x.ServiceID === 0)}>
            <div className="picture-engineer__services">
                <div className="container">
                    <div className="main-services">
                        <div className="col-md-12">
                            <div className="row">
                            {
                                services.map((item, i) => (
                                    <div className="col-md-3 main-services__list" key={i}>
                                        <div className="main-services__item"
                                            style={{ background: `${item.Meta === 'ocr' ? 'rgb(238 239 218)' : ''}` }}>
                                            <Link className="service-item" to={'/' + item.Meta} >
                                                <img alt={item.Name} src={item.ImgPath} />
                                                <h3>{item.Name}</h3>
                                                <p>{item.Description}</p>
                                            </Link>
                                        </div>
                                    </div>
                                ))
                            }
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </MainContent>
    );
}

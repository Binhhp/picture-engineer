import UploadFile from "../Service/UploadFile/UploadFile";
import MainContent from "../../layouts/MainLayout/component/MainContent";
import { useDispatch, useSelector } from "react-redux";
import { getDetailService } from "../../redux/apis/Services/getServices";
import { useEffect, useState } from "react";
import { useData } from "../Service/hooks/useData";
import Process from "./component/Process";
import "./css/ocr.css";
import { toast } from "react-toastify";
import { Modal } from "react-bootstrap";
import React from "react";
import "../Service/utils/service.css";
import { fetchOcrClearData } from "../../redux/actions/OCR/OcrAction";

export default function Ocr() {
    const service = useSelector(state => state.serviceDetail);

    const dispatch = useDispatch();
    useEffect(function () {
        dispatch(getDetailService("nhan-dien"));
        dispatch(fetchOcrClearData());
    }, [dispatch])

    const {
        setFile,
        setSelectedFile,
        selectedFile,
        changeHanlder,
        file
    } = useData();

    const setHtmlUsage = function (usage) {
        var html = document.getElementById('userGuide');
        if (html !== null && html !== undefined) {
            html.innerHTML = usage
        }
        return;
    }

    const back = () => {
        setSelectedFile("");
        setFile({});
    };
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const serviceIndex = function () {
        var extension = file.fileName.split(".").pop();
        if (extension !== "jpg" && extension !== "png" && extension !== "jpeg") {
            toast.warn("Định dạng file không hợp lệ.");
            setSelectedFile("");
            return setFile({});
        }
        else {
            return <Process back={back} file={file} colorService={service.data.Color}></Process>
        }
    }

    return (
        <MainContent 
            title={service.data.Name} 
            meta={"ocr"} 
            description={service.data.Description} 
            questions={service.data.FAQs}>
            
            <div className="picture-engineer__uploadfile">
                <div className="container">
                    {
                        Object.keys(file).length === 0
                        ? <UploadFile changeHanlder={changeHanlder}
                                    color={service.data.Color} selectedFile={selectedFile} />

                        : serviceIndex()
                    }
                </div>
            </div>

            <div className="picture-engineer__usage">
                <div className="container">
                    <div id="userGuide">
                        {
                            setHtmlUsage(service.data.UserGuide)
                        }
                    </div>
                    <p>Chi tiết ví dụ minh họa về ảnh chụp tài liệu xem tại&nbsp;
                        <span onClick={() => handleShow()} style={{cursor: `pointer`}}>
                            <strong>đây</strong>
                        </span>
                    </p>
                </div>
            </div>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        <h6>Ảnh chụp tài liệu văn bản</h6>
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="modal-excent">
                        <img src="https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/files%2Fvalidation-014.jpg?alt=media&token=70784ae1-b9da-43a0-8cb1-763a0b2b0cbe"
                            alt="Example" />
                    </div>
                </Modal.Body>
            </Modal>
        </MainContent>
    )
}
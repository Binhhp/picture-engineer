import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import MainContent from "../../layouts/MainLayout/component/MainContent";
import { getDetailService } from "../../redux/apis/Services/getServices";
import UploadFile from "../Service/UploadFile/UploadFile";
import { useData } from "../Service/hooks/useData";
import Process from "./component/Process";
import React from "react";
import "../Service/utils/service.css";
import { fetchDocxToPdfClearData } from "../../redux/actions/PDF/DocxToPdfAction";
import { toast } from "react-toastify";

export default function DocxToPdf() {

    const service = useSelector(state => state.serviceDetail);

    const dispatch = useDispatch();
    useEffect(function () {
        dispatch(getDetailService("chuyen-doi-docx"));
        dispatch(fetchDocxToPdfClearData());
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

    const serviceIndex = function () {
        var extension = file.fileName.split(".").pop();
        if (extension !== "docx") {
            toast.warn("Định dạng file không hợp lệ.");
            return back();
        }
        else {
            return <Process back={back} file={file} colorService={service.data.Color}></Process>
        }
    };

    return (
        <MainContent 
            title={service.data.Name} 
            meta={"convert-pdf"} 
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
                </div>
            </div>
        </MainContent>
    )
}
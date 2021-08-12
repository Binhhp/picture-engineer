import React from "react";
import { Route, Switch } from "react-router";
const Blog = React.lazy(() => import('../features/Blog/Blogs/index'));
const Home = React.lazy(() =>  import('../features/Home'));
const Term = React.lazy(() => import('../features/Term&Condition'));
const NoMatch = React.lazy(() => import('../features/NoMatch/NoMatch'));
const Detail = React.lazy(() => import('../features/Blog/Detail/Detail'));
const Ocr = React.lazy(() => import("../features/Ocr/Ocr"));
const PdfToDocx = React.lazy(() => import("../features/PdfToDocx/PdfToDocx"));
const DocxToPdf = React.lazy(() => import("../features/DocxToPdf/DocxToPdf"));
const SplitPdf = React.lazy(() => import("../features/SplitPdf/SplitPdf"));

export default function route(){

    return (
        <Switch>
            <Route exact path="/"><Home /></Route>
            <Route exact path="/nhan-dien"><Ocr /></Route>
            <Route exact path="/chuyen-doi-pdf-sang-docx"><PdfToDocx /></Route>
            <Route exact path="/chuyen-doi-docx"><DocxToPdf /></Route>
            <Route exact path="/tach-pdf"><SplitPdf /></Route>
            <Route exact path="/bai-viet"><Blog /></Route>
            <Route exact path="/bai-viet/:meta"><Detail /></Route>
            <Route exact path="/dieu-khoan"><Term /></Route>
            <Route exact path="*"><NoMatch /></Route>
        </Switch>
    )
}


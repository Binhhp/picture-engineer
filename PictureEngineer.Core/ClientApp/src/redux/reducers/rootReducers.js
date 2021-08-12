import { combineReducers } from 'redux';
import { serviceReducer } from './Services/ServiceReducer';
import { serviceDetailReducer } from './Services/ServiceDetailReducer';
import { blogDetailReducer, blogReducer } from './Blogs/BlogReducer';
import { PdfConvertReducer } from './PDF/PdfConvertReducer';
import { splitPdfReducer } from './PDF/SplitPdfReducer';
import { faqsReducer } from './Services/FAQsReducer';
import { ocrReducer } from './OCR/OcrReducer';
import { DocxToPdfReducer } from './PDF/DocxToPdfReducer';

const rootReducers = combineReducers({
    faqs: faqsReducer,
    services: serviceReducer,
    serviceDetail: serviceDetailReducer,
    blogs: blogReducer,
    blogDetail: blogDetailReducer,
    pdfFormat: PdfConvertReducer,
    docxToPdf: DocxToPdfReducer,
    splitPdf: splitPdfReducer,
    ocr: ocrReducer
});

export default rootReducers;
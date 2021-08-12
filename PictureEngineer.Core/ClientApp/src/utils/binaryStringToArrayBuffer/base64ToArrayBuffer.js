import { MimeTypesMap } from "./MimeTypesMap";

function base64ToArrayBuffer(base64) {
    var binaryString = window.atob(base64);
    var binaryLen = binaryString.length;
    var bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
       var ascii = binaryString.charCodeAt(i);
       bytes[i] = ascii;
    }
    return bytes;
}

 //download file
 function saveByteArray(reportName, data, extension) {
    var blob = blobMimeArrayBuffer(data, extension);
    var link = document.createElement('a');
     var objectURL = window.URL.createObjectURL(blob);

    link.href = objectURL;
    var fileName = reportName;
    link.download = fileName;
    link.click();

    URL.revokeObjectURL(objectURL)
};

function blobMimeArrayBuffer(data, extension)
{
    const mimeType = MimeTypesMap[extension];
    var blob = new Blob([data], {type: mimeType});
    return blob
}

export { base64ToArrayBuffer, saveByteArray, blobMimeArrayBuffer }

import { useState } from "react";
import { toast } from "react-toastify";
import { requestGet } from "../../../apis/axiosSetting/axiosClient";
import { uploadFileStorage } from '../../../apis/upload-file/upload';
import { roundNumberScale } from '../../../utils/round-number/roundNumberScale';

export const useData = function () {

    const [selectedFile, setSelectedFile] = useState("");
    const [file, setFile] = useState({});

    //set file
    const changeHanlder = async function (event) {
        var file = await event.target.files[0];
        await setSelectedFile(file);
        await setFile({});

        await uploadFileStorage(file, 'process-bar').then(async function (msg) {
            if (msg.code === 201) {
                await requestGet("/api/files/" + msg.fileName);
                //get ip address
                setFile({
                    fileName: file.name,
                    localPath: msg.localPath,
                    fileSize: roundNumberScale(file.size / 1000000, 1)
                });
            }
        }).catch(error => {
                toast.error("Bad Request");
         })
    };


    return {
        setFile,
        setSelectedFile,
        selectedFile,
        changeHanlder,
        file
    };
}

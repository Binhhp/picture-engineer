import {storage, firebase} from '../../services/firebase-storage/storage';
import { roundNumberScale } from '../../utils/round-number/roundNumberScale';

const uploadFileStorage = (file, key) => {
    var task = document.getElementById(key);
    var date = (new Date()).getTime();
    const storageRef = storage.ref();
    const fileName = date + '_' + file.name;
    const uploadTask = storageRef.child(`files/${fileName}`).put(file);

    const promiseTask = function(resolve, reject){
        uploadTask.on(
            firebase.storage.TaskEvent.STATE_CHANGED,
    
            (snapshot) => {
                var processd = roundNumberScale((snapshot.bytesTransferred/snapshot.totalBytes) * 100 , 0);
                task.innerHTML = `${processd}%`;
                task.style.width = `${processd}%`;
            },
    
            (error) => {
                switch(error.code){
                    case 'storage/unauthorized':
                        return reject('Use is not permission');
                    case 'storage/canceled':
                        return reject('Upload was canceled');
                    case 'storage/unknown':
                        return reject('Unknown');
                    default:
                        break;
                }
                return;
            },
    
            () => {
                return resolve({
                    type: 'success',
                    code: 201,
                    localPath: `/files/${date + '_' + file.name}`,
                    fileName: fileName
                });
            }
        );
    };
    var core = new Promise(promiseTask)
    return core;
};

const downloadFileStorage = async (localPath) => {

    const storageRef = storage.ref();
    const downloadFile = await storageRef.child(localPath).getDownloadURL()
                            .then((url) => {
                                return {
                                    success: true,
                                    data: url,
                                    error: ''
                                }
                            })
                            .catch((error) => {
                                switch(error.code){
                                    case 'storage/unauthorized':
                                        return {
                                            success: false,
                                            error: 'Use is not permission',
                                            data: {}
                                        };
                                    case 'storage/canceled':
                                        return {
                                            success: false,
                                            error: 'Upload was canceled',
                                            data: {}
                                        };
                                    case 'storage/unknown':
                                        return {
                                            success: false,
                                            error: 'Unknown',
                                            data: {}
                                        };
                                    default:
                                        break;
                                }
                                return '';
                            })
    return downloadFile
}
export { uploadFileStorage, downloadFileStorage }
import firebase from 'firebase/app';
import 'firebase/storage';

const firebaseConfig = {
    apiKey: process.env.REACT_APP_FIREBASE_STORAGE_APIKEY,
    authDomain: process.env.REACT_APP_FIREBASE_STORAGE_AUTHDOMAIN,
    projectId: process.env.REACT_APP_FIREBASE_STORAGE_PROJECTID,
    storageBucket: process.env.REACT_APP_FIREBASE_STORAGE_STORAGEBUCKET,
    messagingSenderId: process.env.REACT_APP_FIREBASE_STORAGE_MESSAGESENDERID,
    appId: process.env.REACT_APP_FIREBASE_STORAGE_APIID,
    measurementId: process.env.REACT_APP_FIREBASE_STORAGE_MEASUREMENTID
};

firebase.initializeApp(firebaseConfig);
const storage = firebase.storage();

export { storage, firebase }


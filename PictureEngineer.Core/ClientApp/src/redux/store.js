import { createStore, applyMiddleware } from "redux"
import rootReducers from "./reducers/rootReducers"
import thunk from 'redux-thunk'
import { composeWithDevTools } from 'redux-devtools-extension'

const middleWare = [thunk];

const store = createStore(
    rootReducers, 
    composeWithDevTools(applyMiddleware(...middleWare))
);

export default store
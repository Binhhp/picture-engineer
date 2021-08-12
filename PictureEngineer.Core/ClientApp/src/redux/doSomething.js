
import { getFAQs, getServices } from "./apis/Services/getServices";

export default function doSomething() {
    return dispatch => Promise.all([
        dispatch(getServices()),
        dispatch(getFAQs())
    ])
}
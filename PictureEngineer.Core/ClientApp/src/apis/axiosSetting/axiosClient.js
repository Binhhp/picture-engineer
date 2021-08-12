import axios from "axios";
import responseStatus from "../response/response";

//Request Https Get
export async function requestGet(url) {
    const options = {
        "Accept": "application/json"
    };

    const response = await axios.get(url, {
        headers: options
    }).catch(error => {
        return {
            status: error.response.status,
            statusText: error.message,
            errors: error.response.data,
            location: url
        };
    });

    if (response.status <= 204 && response.status >= 200 && response.status !== undefined) {
        return responseStatus(response.status, response.statusText, response.data, "");
    }

    return responseStatus(response.status, response.statusText, "", response.errors);
};
//Request Https Post
export async function requestPost(url, data) {
    const options = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': '*'
    };

    const response = await axios.post(url, data, {
        headers: options
    }).catch(error => {
        return {
            status: error.response.status,
            statusText: error.message,
            errors: error.response.data,
            location: url
        };
    });

    if (response.status <= 204 && response.status >= 200 && response.status !== undefined) {

        return responseStatus(response.status, response.statusText, response.data, "");
    }

    return responseStatus(response.status, response.statusText, "", response.errors);
};

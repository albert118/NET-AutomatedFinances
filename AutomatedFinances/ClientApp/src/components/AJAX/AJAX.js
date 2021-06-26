// API source to request from, root url

import { User } from "oidc-client";

//const APIRootPath = "http://localhost:8000";
const APIRootPath = "";

// standard HTTP errors to check for
const HttpNotFound = 404;
const HttpUnauthorised = 401;
const HttpForbidden = 403
const HttpBadRequest = 400;
const HttpMovedPermanently = 301;

function APIError(message, data, status) {
    /* API Error wrapper. Attempts to parse the response
     * gracefully. Stringify's the result if successful or not!
     */
    let response = null;
    let isObject = false;

    // attempt to parse a response...
    try {
        response = JSON.parse(data);
        isObject = true;
    } catch (error) {
        response = data;
    }

    this.response = response;
    this.message = message;
    this.status = status;
    this.toString = function () {
        return (
            `${this.message}\nResponse:\n${
            isObject ? JSON.stringify(this.response, null, 2) : this.response
            }`
        );
    };
}

async function getCsrfToken(APIRootPath) {
    const response = await fetch(`${APIRootPath}/accounts/csrf/`, {
        credentials: 'include',
    });
    const data = await response.json();
    return data.csrfToken
}

export const fetchResource = (path, userOptions = {}) => {
    // build the query's url...
    const url = APIRootPath.length == 0 ? path : `${APIRootPath}/${path}`;

    // data added after method check
    var defaultOptions = {};
    var defaultHeaders = {};
    var body = null;

    //////////////////////////////
    // ENABLE ONCE ACCOUNTS ADDED
    //////////////////////////////
    if ("method" in userOptions) {
        if (userOptions.method === "POST") {
            // default query options for the backend PAPI.
            //defaultOptions = {
            //    mode: "cors",
            //    credentials: "include",
            //};
    
            defaultHeaders = {
                'Content-Type': 'application/json;charset=utf-8',
                'Access-Control-Allow-Origin': '*',
                // CSRF Token for backend requests...helper fetch function grabs it.
                //'x-csrftoken': getCsrfToken(APIRootPath),
            };
            body = JSON.stringify(userOptions.data);

        } else if (userOptions.method === "GET") {
            // default query options for the backend PAPI.
            //defaultOptions = {
            //    credentials: "include",
            //};
    
            defaultHeaders = {
                'Content-Type': 'application/json;charset=utf-8',
            };
        }
    } else {
        throw new APIError(
            `Request failed. No method set!!`,
            null,
            "NO METHOD SET."
        );
    }
    
    const options = {
        // union-combine the options,
        ...defaultOptions,
        ...userOptions,
        // union-combine header options,
        headers: {
            ...defaultHeaders,
            ...userOptions.headers,
        },
        body: body,
    };

    // stringify the data to upload...
    if (options.body && typeof options.body === 'object') {
        options.body = JSON.stringify(options.body);
    }

    var response = null;
    
    return fetch(url, options).then(responseObject => {
        response = responseObject;

        switch (response.status) {
            case HttpBadRequest:
                console.log(`Unauthorised request with options:${options}`);
                this.props.history.push('/HTTPerror/error400');
                break;
            case HttpUnauthorised:
                console.log(`Unauthorised request for resource at ${path}!\nThis has been logged.`);
                this.props.history.push('/HTTPerror/error401');
                break;
            case HttpForbidden:
                console.log(`Unauthorised request for resource at ${path}!\nThis has been logged.`);
                this.props.history.push('/HTTPerror/error403');
                break;
            case HttpNotFound:
                console.log(`Resource at: ${path} not found: 404`);
                this.props.history.push('/HTTPerror/error404');
                break;
            default:
                if (response.status < 200 || response.status >= 300) {
                    // return the response message as text
                    return response.text();
                } else {
                    // return the json response
                    return response.json();
                }
        }
    }).then(parsedResponse => {
        if (response.status === HttpMovedPermanently) {  
            return parsedResponse; // redirect!!
        } else if (response.status < 200 || response.status >= 300) {
            // throw the error if we get here.
            throw parsedResponse;
        }
        // success!!
        return parsedResponse;
    }).catch(error => {
            // utilise the custom API error function here.
            // response doesnt exist unless an HTTP error has occured.
            if (response) {
                throw new APIError(
                    `Request failed with status ${response.status}.`,
                    error,
                    response.data
                );
            } else {
                throw new APIError(error.toString(), null, "REQUEST FAILED TREMENDOUSLY");
            }
        });
}
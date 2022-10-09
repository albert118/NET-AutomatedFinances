/**
 * GET response from the URL
 */
 const getFn = async url => {
    const response = await fetch(url);
    return response.json();
};

/**
 * POST data to the URL and await a response
 */
const postFn = async (url, json='') => {
    const response = await fetch(url, {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: json
    });
    
    return response.json();
};

const server = {
    post: postFn,
    get: getFn
};

export default server;
import { API_BASE_URL } from '@/config'

const defaultHeaders = {
    'Content-Type': 'application/json'
}

const handleResponse = async (response) => {
    if (!response.ok) {
        const error = await response.json();
        return Promise.reject(error);
    }
    try {
        return await response.json();
    } catch (error) {
        return null;
    }
}

const get = async (url, customHeaders = {}) => {
    const headers = { ...defaultHeaders, ...customHeaders };
    const response = await fetch(`${API_BASE_URL}${url}`, {
        method: 'GET',
        headers: headers,
    });
    return handleResponse(response);
}

const post = async (url, data = {}, customHeaders = {}) => {
    const headers = { ...defaultHeaders, ...customHeaders };
    const response = await fetch(`${API_BASE_URL}${url}`, {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data),
    });
    return handleResponse(response);
}

const put = async (url, data = {}, customHeaders = {}) => {
    const headers = { ...defaultHeaders, ...customHeaders };
    const response = await fetch(`${API_BASE_URL}${url}`, {
        method: 'PUT',
        headers: headers,
        body: JSON.stringify(data),
    });
    return handleResponse(response);
}

const del = async (url, customHeaders = {}) => {
    const headers = { ...defaultHeaders, ...customHeaders };
    const response = await fetch(`${API_BASE_URL}${url}`, {
        method: 'DELETE',
        headers: headers
    });
    return handleResponse(response);
}

const postFormData = async (url, formData = {}, customHeaders = {}) => {
    const headers = { ...customHeaders };
    const response = await fetch(`${API_BASE_URL}${url}`, {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(formData),
    });
    return handleResponse(response);
}

export default {
    get,
    post,
    put,
    delete: del,
    postFormData
};
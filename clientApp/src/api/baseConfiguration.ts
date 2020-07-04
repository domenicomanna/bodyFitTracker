import axios from 'axios';
import tokenKey from '../constants/tokenKey';

axios.defaults.baseURL = 'https://localhost:5001/api/';

const token = localStorage.getItem(tokenKey);

axios.interceptors.request.use(
  config => {
    config.headers['Authorization'] = `Bearer ${token}`
    return config;
  },
  error => {
    return Promise.reject({ ...error });
  }
);

axios.interceptors.response.use(
  config => {
    return config;
  },
  error => {
    return Promise.reject(error.response);
  }
);

const { get, post, put, delete: destroy } = axios;

export { get, post, put, destroy };
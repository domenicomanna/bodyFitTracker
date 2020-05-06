import axios from 'axios';
axios.defaults.baseURL = 'https://localhost:5001/api/';

axios.interceptors.request.use(
  config => {
    config.headers['header'] = '...';
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
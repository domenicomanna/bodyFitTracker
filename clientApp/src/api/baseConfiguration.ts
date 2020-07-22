import axios from 'axios';
import history from '../utils/history';
import tokenKey from '../constants/tokenKey';
import routeUrls from '../constants/routeUrls';

axios.defaults.baseURL = 'https://localhost:5001/api/';

const token = localStorage.getItem(tokenKey);

export const setAuthorizationToken = (token: string) => {
  axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
};

if (token) setAuthorizationToken(token);

axios.interceptors.response.use(
  (config) => {
    return config;
  },
  (error) => {
    const responseStatus: number = error.response.status;
    console.log(responseStatus);
    if (responseStatus === 404) history.push(routeUrls.notFound);
    return Promise.reject(error.response);
  }
);

const { get, post, put, delete: destroy } = axios;

export { get, post, put, destroy };

import axios from 'axios';
import history from '../utils/history';
import tokenKey from '../constants/tokenKey';
import routeUrls from '../constants/routeUrls';

enum ResponseStatus {
  Unauthorized = 401,
  NotFound= 404,
}

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

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
    if (responseStatus === ResponseStatus.NotFound) history.push(routeUrls.notFound);
    else if (responseStatus === ResponseStatus.Unauthorized) history.push(routeUrls.login);
    return Promise.reject(error.response);
  }
);

const { get, post, put, delete: destroy } = axios;

export { get, post, put, destroy };

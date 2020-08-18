import axios from 'axios';
import history from '../utils/history';
import tokenKey from '../constants/tokenKey';
import routeUrls from '../constants/routeUrls';
import { toast } from 'react-toastify';

enum ResponseStatus {
  Unauthorized = 401,
  NotFound = 404,
  InternalServerError = 500,
}
const networkError = 'Network Error';

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
    if (error.message === networkError) toast.error('Network error. The server is not running');

    if (error.response && error.response.status) {
      const responseStatus: number = error.response.status;
      if (responseStatus === ResponseStatus.NotFound) history.push(routeUrls.notFound);
      else if (responseStatus === ResponseStatus.Unauthorized) history.push(routeUrls.login);
      else if (responseStatus === ResponseStatus.InternalServerError) history.push(routeUrls.serverError);
    }
    return Promise.reject(error);
  }
);

const { get, post, put, delete: destroy } = axios;

export { get, post, put, destroy };

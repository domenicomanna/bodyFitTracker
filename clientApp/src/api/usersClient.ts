import { get } from './baseConfiguration';
import { User } from '../types/userTypes';

const requests = {
  getUser: () => get('users').then((response) => response.data),
};

const usersClient = {
  getUser: (): Promise<User> => requests.getUser(),
};

export default usersClient;

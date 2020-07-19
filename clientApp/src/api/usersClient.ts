import { get } from './baseConfiguration';
import { UserSettings } from '../types/userTypes';

const requests = {
  getUser: () => get('users').then((response) => response.data),
};

const usersClient = {
  getUser: (): Promise<UserSettings> => requests.getUser(),
};

export default usersClient;

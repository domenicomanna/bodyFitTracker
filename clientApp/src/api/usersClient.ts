import { get, post } from './baseConfiguration';
import { User, CreateUserResult, CreateUserType } from '../types/userTypes';

const requests = {
  getUser: () => get('users').then((response) => response.data),
  createUser: (createUserType: CreateUserType) => post('users', createUserType).then((response) => response.data),
};

const usersClient = {
  getUser: (): Promise<User> => requests.getUser(),
  createUser: (createUserType: CreateUserType): Promise<CreateUserResult> => requests.createUser(createUserType),
};

export default usersClient;

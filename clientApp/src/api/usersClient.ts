import { get, post, put } from './baseConfiguration';
import { User, CreateUserResult, CreateUserType, ChangePasswordType, ChangePasswordResult } from '../types/userTypes';

const requests = {
  getUser: () => get('users').then((response) => response.data),
  createUser: (createUserType: CreateUserType) => post('users', createUserType).then((response) => response.data),
  changePassword: (changePasswordType: ChangePasswordType) =>
    put('users/change-password/', changePasswordType).then((response) => response.data),
};

const usersClient = {
  getUser: (): Promise<User> => requests.getUser(),
  createUser: (createUserType: CreateUserType): Promise<CreateUserResult> => requests.createUser(createUserType),
  changePassword: (changePasswordType: ChangePasswordType): Promise<ChangePasswordResult> =>
    requests.changePassword(changePasswordType),
};

export default usersClient;

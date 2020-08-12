import { get, post, put } from './baseConfiguration';
import {
  User,
  CreateUserResult,
  CreateUserType,
  ChangePasswordType,
  ChangePasswordResult,
  ResetPasswordStepOneType,
  ResetPasswordValidationResult,
  ResetPasswordStepTwoType,
  ResetPasswordResult,
  EditAccountType,
} from '../types/userTypes';
import { AxiosResponse } from 'axios';

const requests = {
  getUser: () => get('users').then((response) => response.data),
  createUser: (createUserType: CreateUserType) => post('users', createUserType).then((response) => response.data),
  changePassword: (changePasswordType: ChangePasswordType) =>
    put('users/change-password/', changePasswordType).then((response) => response.data),
  resetPasswordStepOne: (resetPasswordStepOneType: ResetPasswordStepOneType) =>
    post('users/reset-password-step-one', resetPasswordStepOneType),
  resetPasswordStepTwo: (resetPasswordStepTwoType: ResetPasswordStepTwoType) =>
    put('users/reset-password-step-two', resetPasswordStepTwoType).then((response) => response.data),
  validateResetPasswordToken: (token: string) =>
    get(`users/validate-reset-password-token/${token}`).then((response) => response.data),
  changeProfileSettings: (editAccountType: EditAccountType) =>
    put(`users/`, editAccountType).then((response) => response.data),
};

const usersClient = {
  getUser: (): Promise<User> => requests.getUser(),
  createUser: (createUserType: CreateUserType): Promise<CreateUserResult> => requests.createUser(createUserType),
  changePassword: (changePasswordType: ChangePasswordType): Promise<ChangePasswordResult> =>
    requests.changePassword(changePasswordType),
  resetPasswordStepOne: (resetPasswordStepOneType: ResetPasswordStepOneType) =>
    requests.resetPasswordStepOne(resetPasswordStepOneType),
  resetPasswordStepTwo: (resetPasswordStepTwoType: ResetPasswordStepTwoType): Promise<ResetPasswordResult> =>
    requests.resetPasswordStepTwo(resetPasswordStepTwoType),
  validateResetPasswordToken: (token: string): Promise<ResetPasswordValidationResult> =>
    requests.validateResetPasswordToken(token),
  changeProfileSettings: (editAccountType: EditAccountType): Promise<AxiosResponse> =>
    requests.changeProfileSettings(editAccountType),
};

export default usersClient;

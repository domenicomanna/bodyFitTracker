import { post } from './baseConfiguration';
import { SignInFormValues, SignInResult } from '../models/authenticationModels';

const requests = {
  signIn: (values: SignInFormValues) => post('authentication', values).then((response) => response.data),
};

const authenticationClient = {
  signIn: (values: SignInFormValues): Promise<SignInResult> => requests.signIn(values),
};

export default authenticationClient;

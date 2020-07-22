import React, { useState, useEffect, useRef, useContext } from 'react';
import PageTitle from '../../components/pageTitle/PageTitle';
import Form from '../../components/ui/form/Form';
import { useFormik } from 'formik';
import { object, string } from 'yup';
import Input from '../../components/ui/input/Input';
import ValidationError from '../../components/ui/validationError/ValidationError';
import Button from '../../components/ui/button/Button';
import { NavLink, useHistory } from 'react-router-dom';
import routeUrls from '../../constants/routeUrls';
import styles from './login.module.css';
import { SignInFormValues, SignInResult } from '../../types/authenticationTypes';
import authenticationClient from '../../api/authenticationClient';
import usersClient from '../../api/usersClient';
import { User } from '../../types/userTypes';
import tokenKey from '../../constants/tokenKey';
import { UserContext } from '../../contexts/UserContext';
import { setAuthorizationToken } from '../../api/baseConfiguration';

let validationSchema = object<SignInFormValues>({
  email: string().email('Invalid email').required('Required'),
  password: string().required('Required'),
});

const Login = () => {
  const { setEmail, setHeight, setMeasurementPreference, setGender } = useContext(UserContext);
  const [signInErrorMessage, setSignInErrorMessage] = useState('');
  const history = useHistory();

  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    } as SignInFormValues,

    onSubmit: async (formValues: SignInFormValues) => {
      const formIsValid = await validationSchema.isValid(formValues);
      if (!formIsValid) {
        setSignInErrorMessage('Invalid email or password');
        return;
      }
      setSignInErrorMessage('');
      const signInResult: SignInResult = await authenticationClient.signIn(formValues);
      if (!signInResult.signInWasSuccessful) {
        setSignInErrorMessage(signInResult.errorMessage);
        formik.setFieldValue('password', '');
      } else {
        localStorage.setItem(tokenKey, signInResult.token);
        setAuthorizationToken(signInResult.token);
        const user: User = await usersClient.getUser();
        setHeight(user.height);
        setEmail(user.email);
        setGender(user.gender);
        setMeasurementPreference(user.measurementPreference);
        history.push(routeUrls.home);
      }
    },
  });

  return (
    <>
      <PageTitle>Login</PageTitle>
      <Form style={{ maxWidth: '600px' }} onSubmit={formik.handleSubmit}>
        <label htmlFor='email'>Email</label>
        <div>
          <Input id='email' type='email' {...formik.getFieldProps('email')} />
        </div>
        <label htmlFor='password'>Password</label>
        <div>
          <Input id='password' type='password' {...formik.getFieldProps('password')} />
        </div>

        {signInErrorMessage && (
          <>
            <span></span>
            <ValidationError testId="signInErrorMessage" style={{ margin: '0' }}>{signInErrorMessage}</ValidationError>
          </>
        )}

        <span></span>
        <Button buttonClass='primary' disabled={formik.isSubmitting} type='submit' isSubmitting={formik.isSubmitting}>
          Sign in
        </Button>

        <span></span>
        <div className={styles.helpContent}>
          <span>
            <NavLink to={routeUrls.login}>Forgot your password?</NavLink>
          </span>
          <span>
            <NavLink to={routeUrls.login}>Create account</NavLink>
          </span>
        </div>
      </Form>
    </>
  );
};

export default Login;

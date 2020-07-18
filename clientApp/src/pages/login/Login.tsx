import React, { useState, useEffect, useRef } from 'react';
import PageTitle from '../../components/pageTitle/PageTitle';
import Form from '../../components/ui/form/Form';
import { useFormik } from 'formik';
import { object, string } from 'yup';
import Input from '../../components/ui/input/Input';
import ValidationError from '../../components/ui/fieldValidationError/FieldValidationError';
import Button from '../../components/ui/button/Button';
import { NavLink } from 'react-router-dom';
import routeUrls from '../../constants/routeUrls';
import styles from './login.module.css';
import { SignInFormValues, SignInResult } from '../../models/authenticationModels';
import authenticationClient from '../../api/authenticationClient';

let validationSchema = object<SignInFormValues>({
  email: string().email('Invalid email').required('Required'),
  password: string().required('Required'),
});

const Login = () => {
  const [signInErrorMessage, setSignInErrorMessage] = useState('');
  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    } as SignInFormValues,
    onSubmit: async (formValues: SignInFormValues) => {
      const formIsValid = await validationSchema.isValid(formValues);
      if (!formIsValid) {
        setSignInErrorMessage('Invalid username or password');
        return;
      }
      const signInResult: SignInResult = await authenticationClient.signIn(formValues);
      if (!signInResult.signInWasSuccessful) {
        setSignInErrorMessage(signInResult.errorMessage);
        formik.setFieldValue('password', '');
      } else {
        console.log("Signing in...");
      }
    },
  });

  return (
    <>
      <PageTitle>Log in</PageTitle>
      <Form style={{ maxWidth: '600px' }} onSubmit={formik.handleSubmit}>
        <label htmlFor='email'>Email</label>
        <div>
          <Input id='email' type='text' {...formik.getFieldProps('email')} />
        </div>
        <label htmlFor='password'>Password</label>
        <div>
          <Input id='password' type='password' {...formik.getFieldProps('password')}/>
        </div>

        {signInErrorMessage && (
          <>
            <span></span>
            <ValidationError style={{ margin: '0' }}>{signInErrorMessage}</ValidationError>
          </>
        )}

        <span></span>
        <Button buttonClass='primary' disabled={formik.isSubmitting} type='submit' isSubmitting={formik.isSubmitting}>
          Submit
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

import React from 'react';
import PageTitle from '../../components/pageTitle/PageTitle';
import Form from '../../components/ui/form/Form';
import { useFormik } from 'formik';
import { object, string } from 'yup';
import Input from '../../components/ui/input/Input';
import FieldValidationError from '../../components/ui/fieldValidationError/FieldValidationError';
import Button from '../../components/ui/button/Button';
import { NavLink } from 'react-router-dom';
import routeUrls from '../../constants/routeUrls';
import styles from './login.module.css';

const CreateValidationSchema = () => {
  let validationSchema = object({
    email: string().email('Invalid email').required('Required'),
    password: string().required('Required'),
  });

  return validationSchema;
};

const Login = () => {
  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    },
    validationSchema: CreateValidationSchema(),
    onSubmit: async (formValues) => {

    },
  });

  return (
    <>
      <PageTitle>Log in</PageTitle>
      <Form style={{ maxWidth: '600px' }} onSubmit={formik.handleSubmit}>
        <label htmlFor='email'>Email</label>
        <div>
          <Input id='email' type='text' {...formik.getFieldProps('email')} />
          {formik.touched.email && formik.errors.email ? (
            <FieldValidationError>{formik.errors.email}</FieldValidationError>
          ) : null}
        </div>

        <label htmlFor='password'>Password</label>
        <div>
          <Input id='password' type='password' {...formik.getFieldProps('password')} />
          {formik.touched.password && formik.errors.password ? (
            <FieldValidationError>{formik.errors.password}</FieldValidationError>
          ) : null}
        </div>

        <span></span>
        <Button
          buttonClass='primary'
          disabled={!formik.isValid || formik.isSubmitting || !formik.dirty}
          type='submit'
          isSubmitting={formik.isSubmitting}
        >
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

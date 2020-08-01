import React from 'react';
import PageTitle from '../../../components/pageTitle/PageTitle';
import { Helmet } from 'react-helmet';
import siteTitle from '../../../constants/siteTitle';
import Form from '../../../components/ui/form/Form';
import Input from '../../../components/ui/input/Input';
import Button from '../../../components/ui/button/Button';
import { useFormik } from 'formik';
import { ResetPasswordStepOneType } from '../../../types/userTypes';
import { string, object } from 'yup';
import ValidationError from '../../../components/ui/validationError/ValidationError';
import { useHistory } from 'react-router-dom';
import routeUrls from '../../../constants/routeUrls';
import usersClient from '../../../api/usersClient';

let validationSchema = object<ResetPasswordStepOneType>({
  email: string().email('Invalid email').required('Required'),
});

export const ResetPasswordStepOne = () => {
	const history = useHistory();
  const formik = useFormik({
    initialValues: {
      email: '',
    } as ResetPasswordStepOneType,
		validationSchema: validationSchema,
    onSubmit: async (formValues: ResetPasswordStepOneType) => {
			await usersClient.resetPasswordStepOne(formValues);
			history.push(routeUrls.resetPassword.stepOneSuccess);
    },
  });
  return (
    <>
      <Helmet>
        <title> {siteTitle} | Reset Password</title>
      </Helmet>
      <PageTitle>Find Your Account</PageTitle>
      <Form style={{ maxWidth: '600px' }} onSubmit={formik.handleSubmit}>
        <label htmlFor='email'>Email</label>
        <div>
          <Input id='email' type='email' {...formik.getFieldProps('email')} />
					{formik.touched.email && formik.errors.email && (
            <ValidationError> {formik.errors.email} </ValidationError>
          )}
        </div>

        <span></span>
        <Button buttonClass='primary' disabled={!formik.isValid || !formik.dirty || formik.isSubmitting} type='submit' isSubmitting={formik.isSubmitting}>
          Find Account
        </Button>
      </Form>
    </>
  );
};

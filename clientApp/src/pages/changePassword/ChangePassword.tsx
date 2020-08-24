import React from 'react';
import { object, string } from 'yup';
import password from '../../validationRules/password';
import { ChangePasswordType } from '../../types/userTypes';
import { LoginLocationState } from '../login/Login';
import Form from '../../components/ui/form/Form';
import Input from '../../components/ui/input/Input';
import PageTitle from '../../components/pageTitle/PageTitle';
import Button from '../../components/ui/button/Button';
import { useFormik } from 'formik';
import ValidationError from '../../components/ui/validationError/ValidationError';
import usersClient from '../../api/usersClient';
import tokenKey from '../../constants/tokenKey';
import routeUrls from '../../constants/routeUrls';
import { useHistory } from 'react-router-dom';
import { setAuthorizationToken } from '../../api/baseConfiguration';
import { Helmet } from 'react-helmet';
import brandName from '../../constants/brandName';

let validationSchema = object<ChangePasswordType>({
  currentPassword: string().required('Required'),
  newPassword: password,
  confirmedNewPassword: string()
    .required('Required')
    .test('Password match', 'The passwords do not match', function (value) {
      return this.parent.newPassword === value;
    }),
});

export const ChangePassword = () => {
  const history = useHistory();

  const formik = useFormik({
    initialValues: {
      currentPassword: '',
      newPassword: '',
      confirmedNewPassword: '',
    } as ChangePasswordType,
    validationSchema: validationSchema,
    onSubmit: async (formValues: ChangePasswordType) => {
      const changePasswordResult = await usersClient.changePassword(formValues);
      if (!changePasswordResult.succeeded) {
        if (changePasswordResult.errors.currentPassword) {
          formik.setFieldError('currentPassword', changePasswordResult.errors.currentPassword);
        }
      } else {
        setAuthorizationToken('');
        localStorage.removeItem(tokenKey);
        const loginLocationState: LoginLocationState = { passwordWasChanged: true };
        history.push(routeUrls.login, loginLocationState);
      }
    },
  });

  return (
    <>
      <Helmet>
        <title>{brandName} | Change Password</title>
      </Helmet>
      <PageTitle>Change Password</PageTitle>
      <Form onSubmit={formik.handleSubmit}>
        <label htmlFor='currentPassword'>Current Password</label>
        <div>
          <Input id='currentPassword' type='password' {...formik.getFieldProps('currentPassword')} />
          {formik.touched.currentPassword && formik.errors.currentPassword && (
            <ValidationError> {formik.errors.currentPassword} </ValidationError>
          )}
        </div>

        <label htmlFor='newPassword'>New Password</label>
        <div>
          <Input id='newPassword' type='password' {...formik.getFieldProps('newPassword')} />
          {formik.touched.newPassword && formik.errors.newPassword && (
            <ValidationError> {formik.errors.newPassword} </ValidationError>
          )}
        </div>

        <label htmlFor='confirmedNewPassword'>Confirm New Password</label>
        <div>
          <Input id='confirmedNewPassword' type='password' {...formik.getFieldProps('confirmedNewPassword')} />
          {formik.touched.confirmedNewPassword && formik.errors.confirmedNewPassword && (
            <ValidationError> {formik.errors.confirmedNewPassword} </ValidationError>
          )}
        </div>

        <span></span>
        <Button
          buttonClass='primary'
          disabled={formik.isSubmitting || !formik.isValid || !formik.dirty}
          type='submit'
          isSubmitting={formik.isSubmitting}
        >
          Change Password
        </Button>
      </Form>
    </>
  );
};

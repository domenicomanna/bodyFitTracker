import React, { FunctionComponent, useEffect, useState } from 'react';
import { Helmet } from 'react-helmet';
import siteTitle from '../../../constants/siteTitle';
import PageTitle from '../../../components/pageTitle/PageTitle';
import { useFormik } from 'formik';
import Input from '../../../components/ui/input/Input';
import Button from '../../../components/ui/button/Button';
import { useHistory, RouteComponentProps } from 'react-router-dom';
import { ResetPasswordStepTwoType, ResetPasswordResult } from '../../../types/userTypes';
import { object, string } from 'yup';
import password from '../../../validationRules/password';
import ValidationError from '../../../components/ui/validationError/ValidationError';
import Form from '../../../components/ui/form/Form';
import usersClient from '../../../api/usersClient';
import routeUrls from '../../../constants/routeUrls';
import { PageLoader } from '../../../components/ui/pageLoader/PageLoader';
import { LoginLocationState } from '../../login/Login';

let validationSchema = object<ResetPasswordStepTwoType>({
  resetPasswordToken: string().required(),
  newPassword: password,
  confirmedNewPassword: string()
    .required('Required')
    .test('Password match', 'The passwords do not match', function (value) {
      return this.parent.newPassword === value;
    }),
});

type TokenParameter = { token: string };

const ResetPasswordStepTwo: FunctionComponent<RouteComponentProps<TokenParameter>> = ({ match }) => {
	const [tokenIsValidated, setTokenIsValidated] = useState(false);
  const history = useHistory();
  const formik = useFormik({
    initialValues: {
      resetPasswordToken: match.params.token,
      newPassword: '',
      confirmedNewPassword: '',
    } as ResetPasswordStepTwoType,
    validationSchema: validationSchema,
    onSubmit: async (formValues: ResetPasswordStepTwoType) => {
      const resetPasswordResult: ResetPasswordResult = await usersClient.resetPasswordStepTwo(formValues);
      if (!resetPasswordResult.succeeded) history.push(routeUrls.resetPassword.invalidToken);
      else{
        const loginLocationState: LoginLocationState = { passswordWasReset: true };
        history.push(routeUrls.login, loginLocationState);
      }
    },
  });

  useEffect(() => {
    const handleTokenValidation = async () => {
      const tokenValidationResult = await usersClient.validateResetPasswordToken(match.params.token);
			if (!tokenValidationResult.succeeded) history.push(routeUrls.resetPassword.invalidToken);
      else setTokenIsValidated(true);
    };
    handleTokenValidation();
	}, []);
	
  const formContent = (
    <Form style={{ maxWidth: '660px' }} onSubmit={formik.handleSubmit} testId="formContent">
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
        disabled={!formik.isValid || !formik.dirty || formik.isSubmitting}
        type='submit'
        isSubmitting={formik.isSubmitting}
      >
        Reset Password
      </Button>
    </Form>
	);
	
  return (
    <>
      <Helmet>
        <title> {siteTitle} | Reset Password</title>
      </Helmet>
      <PageTitle>Reset Your Password</PageTitle>

			{tokenIsValidated ? formContent : <PageLoader testId="pageLoader"/>}
    </>
  );
};

export default ResetPasswordStepTwo;

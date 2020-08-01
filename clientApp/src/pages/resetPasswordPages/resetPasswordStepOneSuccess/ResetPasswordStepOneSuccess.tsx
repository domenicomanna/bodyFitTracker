import React from 'react';
import PageTitle from '../../../components/pageTitle/PageTitle';
import { Helmet } from 'react-helmet';
import siteTitle from '../../../constants/siteTitle';

export const ResetPasswordStepOneSuccess = () => {
  return (
    <>
    <Helmet>
  <title>{siteTitle} | Reset Password</title>
    </Helmet>
      <div style={{maxWidth:"650px", margin:"auto"}}>
        <PageTitle>Check Your Email</PageTitle>
        <p>An email has been sent to the email address provided. Click the link in the email to reset your password.</p>
      </div>
    </>
  );
};

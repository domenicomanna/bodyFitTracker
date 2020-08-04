import React from 'react';
import PageTitle from '../../../components/pageTitle/PageTitle';
import { Helmet } from 'react-helmet';
import siteTitle from '../../../constants/siteTitle';
import Container from '../../../components/container/Container';

export const ResetPasswordStepOneSuccess = () => {
  return (
    <>
      <Helmet>
        <title>{siteTitle} | Reset Password</title>
      </Helmet>
      <Container style={{ maxWidth: '650px' }}>
        <PageTitle>Check Your Email</PageTitle>
        <p>An email has been sent to the email address provided. Click the link in the email to reset your password.</p>
      </Container>
    </>
  );
};

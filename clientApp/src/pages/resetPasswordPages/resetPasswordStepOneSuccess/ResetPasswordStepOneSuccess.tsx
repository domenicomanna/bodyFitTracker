import React from 'react';
import PageTitle from '../../../components/pageTitle/PageTitle';
import { Helmet } from 'react-helmet';
import brandName from '../../../constants/brandName';
import Container from '../../../components/container/Container';

export const ResetPasswordStepOneSuccess = () => {
  return (
    <>
      <Helmet>
        <title>{brandName} | Reset Password</title>
      </Helmet>
      <Container style={{ maxWidth: '650px' }}>
        <PageTitle>Check Your Email</PageTitle>
        <p>An email has been sent to the email address provided. Click the link in the email to reset your password.</p>
      </Container>
    </>
  );
};

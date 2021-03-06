import React from 'react';
import { Helmet } from 'react-helmet';
import PageTitle from '../../../components/pageTitle/PageTitle';
import brandName from '../../../constants/brandName';
import { NavLink } from 'react-router-dom';
import routeUrls from '../../../constants/routeUrls';
import Container from '../../../components/container/Container';

export const InvalidToken = () => {
  return (
    <>
      <Helmet>
        <title> {brandName} | Invalid Token</title>
      </Helmet>
      <PageTitle>Invalid Token </PageTitle>
      <Container style={{ maxWidth: '600px' }}>
        <p>The token is either expired or was not found. </p>
        <p>
          <NavLink to={routeUrls.resetPassword.stepOne}>Are you trying to reset your password?</NavLink>
        </p>
      </Container>
    </>
  );
};

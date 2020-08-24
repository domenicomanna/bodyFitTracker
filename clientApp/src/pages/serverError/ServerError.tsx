import React from 'react';
import PageTitle from '../../components/pageTitle/PageTitle';
import { Link } from 'react-router-dom';
import brandName from '../../constants/brandName';
import { Helmet } from 'react-helmet';
import { WrappingLayout } from '../../components/wrappingLayout/WrappingLayout';
import routeUrls from '../../constants/routeUrls';

const ServerError = () => {
  return (
    <WrappingLayout>
      <>
        <Helmet>
          <title>{brandName} | Server Error</title>
        </Helmet>
        <PageTitle>500 Server Error</PageTitle>
        <p>An error on the server occurred.</p>
        <p>
          Return to <Link to={routeUrls.home}>home</Link>
        </p>
      </>
    </WrappingLayout>
  );
};

export default ServerError;

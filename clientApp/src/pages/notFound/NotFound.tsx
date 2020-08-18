import React, { FunctionComponent, useContext } from 'react';
import PageTitle from '../../components/pageTitle/PageTitle';
import { Link } from 'react-router-dom';
import routeUrls from '../../constants/routeUrls';
import siteTitle from '../../constants/siteTitle';
import { Helmet } from 'react-helmet';
import { WrappingLayout } from '../../components/wrappingLayout/WrappingLayout';

type Props = {
  message?: string;
};

const defaultMessage = 'Oh no! The page you are looking for could not be found.';

const NotFound: FunctionComponent<Props> = ({ message = defaultMessage }) => {
  return (
    <WrappingLayout>
          <>
      <Helmet>
        <title>{siteTitle} | Not Found</title>
      </Helmet>
      <PageTitle>404 NOT FOUND</PageTitle>
      <p>{defaultMessage}</p>
      <p>
        Return to <Link to={routeUrls.home}>home</Link>
      </p>
    </>
    </WrappingLayout>
  )
};

export default NotFound;

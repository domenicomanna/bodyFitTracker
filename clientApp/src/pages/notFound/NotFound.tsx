import React, { FunctionComponent, useContext } from 'react';
import PageTitle from '../../components/pageTitle/PageTitle';
import { Link } from 'react-router-dom';
import routeUrls from '../../constants/routeUrls';
import { UserContext } from '../../contexts/UserContext';
import AuthenticatedLayout from '../../components/authenticatedLayout/AuthenticatedLayout';
import UnauthenticatedLayout from '../../components/unauthenticatedLayout/UnauthenticatedLayout';

type Props = {
  message?: string;
};

const defaultMessage = 'Oh no! The page you are looking for could not be found.';

const NotFound: FunctionComponent<Props> = ({ message = defaultMessage }) => {
  const { isAuthenticated } = useContext(UserContext);

  const notFoundContent = (
    <>
      <PageTitle style={{ marginTop: '1rem' }}>404 NOT FOUND</PageTitle>
      <p>{defaultMessage}</p>
      <p>
        Return to <Link to={routeUrls.home}>home</Link>
      </p>
    </>
  );

  if (isAuthenticated()) return <AuthenticatedLayout>{notFoundContent}</AuthenticatedLayout>
  return <UnauthenticatedLayout>{notFoundContent}</UnauthenticatedLayout>
};

export default NotFound;

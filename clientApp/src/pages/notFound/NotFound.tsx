import React, { FunctionComponent } from 'react';
import PageTitle from '../../components/pageTitle/PageTitle';
import Container from '../../components/container/Container';
import { Link } from 'react-router-dom';
import routeUrls from '../../constants/routeUrls';

type Props = {
  message?: string;
};

const defaultMessage = 'Oh no! The page you are looking for could not be found.';

const NotFound: FunctionComponent<Props> = ({ message = defaultMessage }) => {
  return (
    <>
      <Container>
        <PageTitle style={{marginTop: "1rem"}}>404 NOT FOUND</PageTitle>
        <p>{defaultMessage}</p>
        <p>Return to <Link to={routeUrls.home}>home</Link></p>
      </Container>
    </>
  );
};

export default NotFound;

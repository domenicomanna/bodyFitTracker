import React from 'react';
import Footer from '../footer/Footer';
import Container from '../container/Container';
import Header from './header/Header';

const UnauthenticatedLayout: React.FunctionComponent = ({ children }) => {
  return (
    <>
      <Header />
      <main>
        <Container>{children}</Container>
      </main>
      <Footer />
    </>
  );
};

export default UnauthenticatedLayout;

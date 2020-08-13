import React, { FunctionComponent } from 'react';
import Container from '../container/Container';
import Header from './header/Header';
import Footer from '../footer/Footer';

const AuthenticatedLayout: FunctionComponent = ({ children }) => {
  return (
    <>
      <Header />
      <main style={{marginBottom:"2rem"}}>
        <Container>{children}</Container>
      </main>
      <Footer />
    </>
  );
};

export default AuthenticatedLayout;

import React, { FunctionComponent } from 'react';
import Container from '../container/Container';
import Header from './header/Header';

const Layout: FunctionComponent = ({ children }) => {
  return (
    <>
      <Header />
      <main>
        <Container>{children}</Container>
      </main>

      <footer style={{ marginTop: '5rem' }}>
        <Container>footer...</Container>
      </footer>
    </>
  );
};

export default Layout;

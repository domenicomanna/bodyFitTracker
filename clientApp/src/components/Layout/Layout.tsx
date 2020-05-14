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

      <footer style={{ padding: '1rem 0' }}>
        <Container>&copy; {new Date().getFullYear()} BodyFitTracker</Container>
      </footer>
    </>
  );
};

export default Layout;

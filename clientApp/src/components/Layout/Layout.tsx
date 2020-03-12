import React, { FunctionComponent } from 'react';
import Container from '../Container/Container';
import Header from './Header/Header';

export const Layout: FunctionComponent = ({ children }) => {
  return (
    <>
      <Header />
      <main>
        <Container>main</Container>
      </main>

      <footer style={{marginTop: "5rem"}}>
        <Container>footer...</Container>
      </footer>
    </>
  );
};

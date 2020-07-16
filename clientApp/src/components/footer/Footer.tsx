import React from 'react';
import Container from '../container/Container';

const Footer = () => {
  return (
    <footer style={{ padding: '1rem 0' }}>
      <Container>&copy; {new Date().getFullYear()} BodyFitTracker</Container>
    </footer>
  );
};

export default Footer;

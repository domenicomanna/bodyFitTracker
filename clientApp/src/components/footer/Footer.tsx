import React from 'react';
import Container from '../container/Container';
import brandName from '../../constants/brandName';

const Footer = () => {
  return (
    <footer style={{ padding: '1rem 0' }}>
      <Container>&copy; {new Date().getFullYear()} {brandName} </Container>
    </footer>
  );
};

export default Footer;

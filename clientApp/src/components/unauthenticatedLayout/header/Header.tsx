import React from 'react';
import { NavLink } from 'react-router-dom';
import Container from '../../container/Container';
import styles from './header.module.css';

const Header = () => {
  return (
    <header className={styles.header}>
      <Container>
        <nav className={styles.navigation}>
          <h2 style={{ margin: '0' }}>
            <NavLink to='/' className={styles.brand}>
              BodyFitTracker
            </NavLink>
          </h2>
          <ul className={styles.navList}>
            <li className={styles.navListItem}>
              <NavLink to='/'>Login</NavLink>
            </li>
            <li className={styles.navListItem}>
              <NavLink to='/'>About</NavLink>
            </li>
          </ul>
        </nav>
      </Container>
    </header>
  );
};

export default Header;

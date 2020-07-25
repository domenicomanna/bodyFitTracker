import React from 'react';
import { NavLink } from 'react-router-dom';
import Container from '../../container/Container';
import styles from './header.module.css';
import routeUrls from '../../../constants/routeUrls';

const Header = () => {
  return (
    <header className={styles.header}>
      <Container>
        <nav className={styles.navigation}>
          <h2 style={{ margin: '0' }}>
            <NavLink to={routeUrls.about} className={styles.brand}>
              BodyFitTracker
            </NavLink>
          </h2>
          <ul className={styles.navList}>
            <li className={styles.navListItem}>
              <NavLink to={routeUrls.login} activeClassName={styles.active}>
                Login
              </NavLink>
            </li>
            <li className={styles.navListItem}>
              <NavLink to={routeUrls.signUp} activeClassName={styles.active}>
                Sign Up
              </NavLink>
            </li>
            <li className={styles.navListItem}>
              <NavLink to={routeUrls.about} activeClassName={styles.active}>
                About
              </NavLink>
            </li>
          </ul>
        </nav>
      </Container>
    </header>
  );
};

export default Header;

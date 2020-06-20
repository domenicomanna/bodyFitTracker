import React, { useState } from 'react';
import Container from '../../container/Container';
import styles from './header.module.css';

const Header = () => {
  const [hamburgerLinksShouldShow, toggleHamburgerLinks] = useState(false);

  const navListClasses = `${styles.navListItems} ${hamburgerLinksShouldShow ? styles.showHamburgerLinks : ''}`;

  return (
    <header className={styles.header}>
      <Container>
        <nav className={styles.nav}>
          <span
            className={styles.hamburger}
            onClick={() => toggleHamburgerLinks((prevHamburgerLinksShouldShow) => !prevHamburgerLinksShouldShow)}
          >
            &#9776; {/* html code for hamburger icon */}
          </span>

          <ul className={navListClasses}>
            <li className={styles.navListItem}>
              <a href=''>Home</a>
            </li>
            <li className={styles.navListItem}>
              <a href=''>All Measurements</a>
            </li>
            <li className={styles.navListItem}>
              <a href=''>Add Measurement</a>
            </li>
          </ul>

          <ul className={navListClasses}>
            <li className={styles.navListItem}>
              <a href=''>My Profile</a>
            </li>
            <li className={styles.navListItem}>
              <a href=''>Sign Out</a>
            </li>
          </ul>
        </nav>
      </Container>
    </header>
  );
};

export default Header;

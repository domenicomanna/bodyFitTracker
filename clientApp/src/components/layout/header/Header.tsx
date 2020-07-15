import React, { useState } from 'react';
import Container from '../../container/Container';
import styles from './header.module.css';
import { NavLink } from 'react-router-dom';
import routeUrls from '../../../constants/routeUrls';

const Header = () => {
  const [hamburgerLinksShouldShow, toggleHamburgerLinks] = useState(false);
  const [profileDropDownMenuShouldShow, toggleProfileDropDownMenu] = useState(false);

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
              <NavLink to={routeUrls.home} activeClassName={styles.active} exact>
                Home
              </NavLink>
            </li>
            <li className={styles.navListItem}>
              <NavLink to={routeUrls.createMeasurement} activeClassName={styles.active} exact>
                Add Measurement
              </NavLink>
            </li>
          </ul>

          <ul className={navListClasses}>
            <li style={{ position: 'relative' }} className={styles.navListItem}>
              <button
                style={profileDropDownMenuShouldShow ? { color: 'black' } : undefined}
                type='button'
                onClick={() => toggleProfileDropDownMenu((prev) => !prev)}
              >
                My Profile
              </button>
              {profileDropDownMenuShouldShow && (
                <ul className={styles.dropDownMenu}>
                  <li>
                    <NavLink to={routeUrls.createMeasurement} activeClassName={styles.active} exact>
                      Settings
                    </NavLink>
                  </li>
                  <li>
                    <NavLink to={routeUrls.createMeasurement} activeClassName={styles.active} exact>
                      Change Password
                    </NavLink>
                  </li>
                  <li>
                    <NavLink to={routeUrls.createMeasurement} activeClassName={styles.active} exact>
                      Sign Out
                    </NavLink>
                  </li>
                </ul>
              )}
            </li>
          </ul>
        </nav>
      </Container>
    </header>
  );
};

export default Header;

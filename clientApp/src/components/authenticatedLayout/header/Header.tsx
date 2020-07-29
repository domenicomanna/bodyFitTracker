import React, { useState, useEffect, useRef } from 'react';
import Container from '../../container/Container';
import styles from './header.module.css';
import { NavLink } from 'react-router-dom';
import routeUrls from '../../../constants/routeUrls';
import tokenKey from '../../../constants/tokenKey';
import { setAuthorizationToken } from '../../../api/baseConfiguration';

const Header = () => {
  const [hamburgerMenuIsOpen, setHamburgerMenuOpen] = useState(false);
  const [profileDropDownMenuShouldShow, setProfileDropDownMenuOpen] = useState(false);
  const profileDropDownMenuParentNodeRef = useRef<HTMLLIElement>(null); // the list item that contains the drop-down menu
  const headerRef = useRef<HTMLHeadElement>(null);

  const handleClick = (e: MouseEvent) => {
    const clickedElement: Node = e.target as Node;
    if (profileDropDownMenuParentNodeRef && !profileDropDownMenuParentNodeRef.current?.contains(clickedElement)) {
      setProfileDropDownMenuOpen(false);
    }

    if (headerRef && !headerRef.current?.contains(clickedElement)){
      setHamburgerMenuOpen(false);
    }
  };
  useEffect(() => {
    document.addEventListener('click', handleClick);
    return () => {
      document.removeEventListener('click', handleClick);
    };
  });

  const signUserOut = () => {
    localStorage.removeItem(tokenKey);
    setAuthorizationToken('');
  }

  const navListClasses = `${styles.navListItems} ${hamburgerMenuIsOpen ? styles.showHamburgerLinks : ''}`;

  return (
    <header className={styles.header} ref={headerRef} data-testid="header">
      <Container>
        <nav className={styles.nav}>
          <span
            data-testid="hamburger"
            className={styles.hamburger}
            onClick={() => setHamburgerMenuOpen((isOpen) => !isOpen)}
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
            <li style={{ position: 'relative' }} className={styles.navListItem} ref={profileDropDownMenuParentNodeRef}>
              <button
                type='button'
                onClick={() => setProfileDropDownMenuOpen((isOpen) => !isOpen)}
              >
                My Profile
              </button>
              {profileDropDownMenuShouldShow && (
                <ul data-testid="dropDownMenu" className={styles.dropDownMenu}>
                  <li>
                    <NavLink to={routeUrls.createMeasurement} exact>
                      Settings
                    </NavLink>
                  </li>
                  <li>
                    <NavLink to={routeUrls.changePassword} exact>
                      Change Password
                    </NavLink>
                  </li>
                  <li>
                    <NavLink to={routeUrls.login} onClick={signUserOut} exact>
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

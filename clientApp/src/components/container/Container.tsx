import React, { FunctionComponent } from 'react';
import styles from './container.module.css';

const Container: FunctionComponent = ({ children }) => {
  return (
    <div className={styles.container}>
      {children}
    </div>
  );
};

export default Container;

import React from 'react';
import ScaleLoader from 'react-spinners/ScaleLoader';
import styles from './pageLoader.module.css';

export const PageLoader = () => {
  return (
    <div data-testid="pageLoader" className={styles.loader}>
      <ScaleLoader height='50px' width='5px' color={styles.loaderColor} />
    </div>
  );
};

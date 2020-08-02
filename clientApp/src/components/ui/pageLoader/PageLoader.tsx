import React, { FunctionComponent } from 'react';
import ScaleLoader from 'react-spinners/ScaleLoader';
import styles from './pageLoader.module.css';

type Props = {
  testId?: string
}

export const PageLoader: FunctionComponent<Props> = ({testId}) => {
  return (
    <div data-testid={testId} className={styles.loader}>
      <ScaleLoader height='50px' width='5px' color={styles.loaderColor} />
    </div>
  );
};

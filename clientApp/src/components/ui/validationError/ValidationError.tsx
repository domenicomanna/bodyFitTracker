import React, { FunctionComponent } from 'react';
import styles from './validationError.module.css';

type Props = {
  testId?: string;
  style?: React.CSSProperties;
};

const ValidationError: FunctionComponent<Props> = ({ testId, style, children }) => {
  return (
    <span style={{ ...style }} data-testid={testId} className={styles.fieldError}>
      {children}
    </span>
  );
};

export default ValidationError;

import React, { FunctionComponent } from 'react';
import styles from './fieldValidationError.module.css';

type Props = {
  testId?: string;
  style?: React.CSSProperties;
};

const ValidationError: FunctionComponent<Props> = ({ testId, style, children }) => {
  return (
    <span style={{ ...style }} data-testid={`${testId}FieldValidationError`} className={styles.fieldError}>
      {children}
    </span>
  );
};

export default ValidationError;

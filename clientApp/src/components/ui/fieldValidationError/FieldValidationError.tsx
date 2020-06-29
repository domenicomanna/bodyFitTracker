import React, { FunctionComponent, HTMLAttributes } from 'react';
import styles from './fieldValidationError.module.css';

type Props = {
  testId?: string;
};

const FieldValidationError: FunctionComponent<Props> = ({ testId, children }) => {
  return (
    <span data-testid={`${testId}FieldValidationError`} className={styles.fieldError}>
      {children}
    </span>
  );
};

export default FieldValidationError;

import React, { FunctionComponent } from 'react';
import styles from './fieldValidationError.module.css';

const FieldValidationError: FunctionComponent = ({ children }) => {
  return <span className={styles.fieldError}>{children}</span>;
};

export default FieldValidationError;

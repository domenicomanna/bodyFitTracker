import React, { FunctionComponent } from 'react';
import styles from './radioButtonGroup.module.css';

const RadioButtonGroup: FunctionComponent = ({ children }) => {
  return <div className={styles.radioButtonGroup}>{children}</div>;
};

export default RadioButtonGroup;

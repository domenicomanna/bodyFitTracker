import React, { FunctionComponent, ChangeEvent, FocusEvent, CSSProperties } from 'react';
import styles from './input.module.css';

type Props = {
  id: string;
  type: string;
  value: string | number;
  onChange: (event: ChangeEvent<HTMLInputElement>) => void | undefined;
  onBlur: (event: FocusEvent<HTMLInputElement>) => void | undefined;
  style?: CSSProperties;
};

const Input: FunctionComponent<Props> = ({ ...rest }) => {
  return <input className={styles.input} {...rest}></input>;
};

export default Input;

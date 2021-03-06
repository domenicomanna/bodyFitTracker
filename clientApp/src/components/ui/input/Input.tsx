import React, { FunctionComponent, ChangeEvent, FocusEvent, CSSProperties } from 'react';
import styles from './input.module.css';

type Props = {
  id: string;
  type: 'number' | 'text' | 'date' | 'password' | 'email';
  value: string | number;
  onChange: (event: ChangeEvent<HTMLInputElement>) => void | undefined;
  onBlur: (event: FocusEvent<HTMLInputElement>) => void | undefined;
  style?: CSSProperties;
};

const Input: FunctionComponent<Props> = ({ ...rest }) => {
  const step = rest.type === 'number' ? 'any' : undefined;
  return <input className={styles.input} {...rest} step={step}></input>;
};

export default Input;

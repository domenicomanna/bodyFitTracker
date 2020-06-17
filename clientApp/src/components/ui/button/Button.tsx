import React, { FunctionComponent, CSSProperties } from 'react';
import styles from './button.module.css';

type Props = {
  type: 'button' | 'submit';
  buttonType: 'primary'
  style?: CSSProperties;
  disabled?: boolean;
};

const Button: FunctionComponent<Props> = ({ buttonType, children, ...rest }) => {
  return (
    <button className={`${styles.button} ${styles[buttonType]}`} {...rest}>
      {children}
    </button>
  );
};

export default Button;

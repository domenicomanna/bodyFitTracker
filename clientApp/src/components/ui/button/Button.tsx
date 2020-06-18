import React, { FunctionComponent, CSSProperties } from 'react';
import BarLoader from 'react-spinners/BarLoader';
import styles from './button.module.css';

type Props = {
  type: 'button' | 'submit';
  buttonClass: 'primary';
  style?: CSSProperties;
  disabled?: boolean;
  isSubmitting?: boolean;
};

const Button: FunctionComponent<Props> = ({ buttonClass, isSubmitting, style, children, ...rest }) => {
  const styleToHideText: CSSProperties | undefined = isSubmitting ? { color: 'transparent' } : undefined;
  const loader = isSubmitting ? (
    <BarLoader css={'position: absolute'} color={'#fff'} loading={true} height={10} width={120} />
  ) : null;
  const allButtonStyles: CSSProperties | undefined = { ...style, ...styleToHideText };

  return (
    <button style={allButtonStyles} className={`${styles.button} ${styles[buttonClass]}`} {...rest}>
      {children}
      {loader}
    </button>
  );
};

export default Button;

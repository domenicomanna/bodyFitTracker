import React, { FunctionComponent, CSSProperties } from 'react';
import { BeatLoader } from 'react-spinners';
import styles from './button.module.css';

type Props = {
  type: 'button' | 'submit';
  buttonClass: 'primary' | 'icon';
  style?: CSSProperties;
  disabled?: boolean;
  isSubmitting?: boolean;
};

const Button: FunctionComponent<Props> = ({ buttonClass, isSubmitting, style, children, ...rest }) => {
  const styleToHideText: CSSProperties | undefined = isSubmitting ? { color: 'transparent' } : undefined;
  const loader = isSubmitting ? (
    <span data-testid='loader' style={{ position: 'absolute' }}>
      <BeatLoader color={'#fff'} />
    </span>
  ) : null;
  const buttonClasses = [styles.button, styles[buttonClass]];
  if (rest.disabled && !isSubmitting) buttonClasses.push(styles[`${buttonClass}Disabled`]);
  if (isSubmitting) buttonClasses.push(styles[`${buttonClass}DisabledSubmitting`]);

  return (
    <button style={{ ...style, ...styleToHideText }} className={buttonClasses.join(' ')} {...rest}>
      {children}
      {loader}
    </button>
  );
};

export default Button;

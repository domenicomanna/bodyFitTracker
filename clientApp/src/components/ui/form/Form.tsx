import React, { FunctionComponent } from 'react';
import styles from './form.module.css';

type Props = {
  onSubmit: (e: React.FormEvent<HTMLFormElement>) => void | Promise<any>;
  style?: React.CSSProperties,
};

const Form: FunctionComponent<Props> = ({ children, onSubmit, style }) => {
  return (
    <form style={style} className={styles.form} onSubmit={onSubmit}>
      {children}
    </form>
  );
};

export default Form;

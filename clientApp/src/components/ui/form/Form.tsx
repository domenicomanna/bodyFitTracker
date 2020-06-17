import React, { FunctionComponent } from 'react';
import styles from './form.module.css';

type Props = {
  onSubmit: (e: React.FormEvent<HTMLFormElement>) => void | Promise<any>;
};

const Form: FunctionComponent<Props> = ({ children, onSubmit }) => {
  return (
    <form className={styles.form} onSubmit={onSubmit}>
      {children}
    </form>
  );
};

export default Form;

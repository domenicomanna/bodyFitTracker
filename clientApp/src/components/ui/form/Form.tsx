import React, { FunctionComponent } from 'react';
import styles from './form.module.css';

type Props = {
  onSubmit: (e: React.FormEvent<HTMLFormElement>) => void | Promise<any>;
  style?: React.CSSProperties;
  testId?: string;
};

const Form: FunctionComponent<Props> = ({ children, onSubmit, style, testId }) => {
  return (
    <form data-testid={testId} style={style} className={styles.form} onSubmit={onSubmit}>
      {children}
    </form>
  );
};

export default Form;

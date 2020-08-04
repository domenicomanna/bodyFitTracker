import React, { FunctionComponent } from 'react';
import styles from './container.module.css';

type Props = {
  style?: React.CSSProperties
}

const Container: FunctionComponent<Props> = ({ children, style }) => {
  return (
    <div style={style} className={styles.container}>
      {children}
    </div>
  );
};

export default Container;

import React, { FunctionComponent } from 'react';
import styles from './pageTitle.module.css';

type Props = {
  style?: React.CSSProperties
}

const PageTitle: FunctionComponent<Props> = ({ children, style }) => {
  return <h1 className={styles.title} style={{ ...style }}>{children}</h1>;
};

export default PageTitle;

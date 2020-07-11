import React, { FunctionComponent } from 'react';

type Props = {
  style?: React.CSSProperties
}

const PageTitle: FunctionComponent<Props> = ({ children, style }) => {
  return <h1 style={{ textAlign: 'center', marginTop: 0, ...style }}>{children}</h1>;
};

export default PageTitle;

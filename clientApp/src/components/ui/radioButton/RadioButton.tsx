import React, { FunctionComponent } from 'react';

type Props = {
  id?: string;
  name: string;
  displayText: string;
  checked: boolean;
  style?: React.CSSProperties;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
};

const RadioButton: FunctionComponent<Props> = ({ displayText, ...rest }) => {
  return (
    <label>
      <input type='radio' {...rest} />
      {displayText}
    </label>
  );
};

export default RadioButton;

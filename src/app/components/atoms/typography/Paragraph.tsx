import React from 'react';
import './paragraph.scss';

export interface ParagraphProps {
  lead?: boolean;
  children: React.ReactNode
}

export const Paragraph: React.FC<ParagraphProps> = ({
  ...props
}) => {
  const classNames = [
      props.lead ? 'lead' : ''
  ];

  return (
      <p className={classNames.join(' ')}>{props.children}</p>
  );
};
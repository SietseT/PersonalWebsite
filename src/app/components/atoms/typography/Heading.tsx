import React from 'react';
import './headings.scss';

export interface HeadingProps {
  level: number;
  text: string;
}

/**
 * Primary UI component for user interaction
 */
export const Heading: React.FC<HeadingProps> = ({
  ...props
}) => {

  const HeadingTag = `h${props.level}` as keyof JSX.IntrinsicElements;

  return (
    <HeadingTag>{props.text}</HeadingTag>
  );
};
import React from 'react';
import './button.scss';

export interface ButtonProps {
  /**
   * Is this the principal call to action on the page?
   */
  primary?: boolean;

  /**
   * Button contents
   */
  label: string;
  /**
   * Optional click handler
   */
  onClick?: () => void;
}

/**
 * Primary UI component for user interaction
 */
export const Button: React.FC<ButtonProps> = ({
  primary = false,
  label,
  ...props
}) => {
  const mode = primary ? 'btn--primary' : 'btn--secondary';
  return (
    <a href="#" target="_blank" className={['btn', mode].join(' ')} {...props}>{label}</a>
  );
};

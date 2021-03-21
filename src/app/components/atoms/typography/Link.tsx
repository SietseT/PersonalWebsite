import React from 'react';
import './link.scss';

export interface LinkProps {
    href: string,
    target?: string,
    children: React.ReactNode
}

export const Link: React.FC<LinkProps> = ({...props}) => {
    return (
        <a {...props}>{props.children}</a>
    );
};
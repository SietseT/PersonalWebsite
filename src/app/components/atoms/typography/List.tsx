import React from 'react';
import './list.scss';

export enum ListType {
    Ordered,
    Unordered,
}

export interface ListProps {
    type: ListType
    items: string[]
}

export const List: React.FC<ListProps> = ({...props}) => {

    let tagName = ``;

    if(props.type == ListType.Ordered) {
        tagName = `ol`;
    }

    if(props.type == ListType.Unordered) {
        tagName = `ul`;
    }

    const ListTag = tagName as keyof JSX.IntrinsicElements;

    return (
        <ListTag>
            {props.items.map(item => (
                <li>{item}</li>
            ))}
        </ListTag>
    );
};
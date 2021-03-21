import React from 'react';
import { Story, Meta } from '@storybook/react';
import {ListProps, List, ListType} from "./List";

export default {
    title: 'Atoms/Typography/List',
    component: List
} as Meta;


const Template: Story<ListProps> = (args) => <List type={args.type} items={['Item 1', 'Item 2', 'Item 3']}></List>;

export const Unordered = Template.bind({});
Unordered.args = {
    type: ListType.Unordered
};

export const Ordered = Template.bind({});
Ordered.args = {
    type: ListType.Ordered
};


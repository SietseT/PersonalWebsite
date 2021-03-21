import React from 'react';
import { Story, Meta } from '@storybook/react';
import {Link, LinkProps} from "./Link";

export default {
    title: 'Atoms/Typography/Link',
    component: Link
} as Meta;


const Template: Story<LinkProps> = (args) => <Link {...args}></Link>;

const text = "The quick brown fox jumps over the lazy dog";
export const Default = Template.bind({});
Default.args = {
    href: '#',
    children: text
};


import React from 'react';
import { Story, Meta } from '@storybook/react';
import {Paragraph, ParagraphProps} from "./Paragraph";

export default {
 title: 'Atoms/Typography/Paragraph',
 component: Paragraph
} as Meta;


const Template: Story<ParagraphProps> = (args) => <Paragraph {...args}></Paragraph>;

const text = "The quick brown fox jumps over the lazy dog";
export const Default = Template.bind({});
Default.args = {
 lead: false,
 children: text
};

export const Lead = Template.bind({});
Lead.args = {
 lead: true,
 children: text
};


import React, {Component} from 'react';
import { Story, Meta } from '@storybook/react';

import { Heading, HeadingProps } from './Heading';

export default {
  title: 'Atoms/Typography/Headings',
  component: Component,
} as Meta;

const text = "The quick brown fox jumps over the lazy dog";

export const Headings = () => (
    <div>
      <Heading level={1} text={text}></Heading>
      <Heading level={2} text={text}></Heading>
      <Heading level={3} text={text}></Heading>
      <Heading level={4} text={text}></Heading>
      <Heading level={5} text={text}></Heading>
      <Heading level={6} text={text}></Heading>
    </div>
)



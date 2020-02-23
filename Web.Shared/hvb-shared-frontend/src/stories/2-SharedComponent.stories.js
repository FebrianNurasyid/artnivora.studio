// src/stories/Task.stories.js

import React from 'react';
import { action } from '@storybook/addon-actions';

import SharedComponent from '../components/SharedComponent';

export default {
  component: SharedComponent,
  title: 'SharedComponent',
  // Our exports that end in "Data" are not stories.
  excludeStories: /.*Data$/,
};

export const taskData = {
  id: '1',
  title: 'Test Task',
  state: 'TASK_INBOX',
  updatedAt: new Date(2018, 0, 1, 9, 0),
};

export const actionsData = {
  onPinTask: action('onPinTask'),
  onArchiveTask: action('onArchiveTask'),
};

export const Default = () => {
  return <SharedComponent 
    color={'red'}
  />;
};

export const Blue = () => {
  return <SharedComponent 
    color={'blue'}
  />;
};

export const Green = () => {
  return <SharedComponent 
    color={'green'}
  />;
};

export const Pink = () => {
    return <SharedComponent
        color={'pink'}
    />;
};
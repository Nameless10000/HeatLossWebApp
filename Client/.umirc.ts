import { defineConfig } from '@umijs/max';
import React from 'react';

export default defineConfig({
  antd: {
    dark: true
  },
  access: {},
  model: {},
  initialState: {},
  request: {},
  locale: {
    antd: true,
    default: 'ru-RU'
  },
  layout: {
  },
  routes: [
    {
      path: '/',
      redirect: '/home',
    },
    {
      name: 'Главная',
      path: '/home',
      component: 'Home/index'
    },
    {
      name: 'Отчеты',
      path: '/previous',
      component: 'Previous/index'
    },
    {
      name: 'Материалы',
      path: '/materials',
      component: 'Materials/index'
    }
  ],
  npmClient: 'npm',
});


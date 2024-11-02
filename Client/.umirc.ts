import { defineConfig } from '@umijs/max';
import React from 'react';

export default defineConfig({
  antd: {
    dark: true
  },
  proxy: {
    '/api/': {
      target: 'http://127.0.0.1:5143',
      changeOrigin: true,
      pathRewrite: { '^': '' }
    }
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
      name: 'Справочник материалов',
      path: '/materials',
      component: 'Materials/index'
    },
    {
      name: 'Методические указания',
      path: '/methodics',
      component: 'Methodics/index'
    }
  ],
  npmClient: 'npm',
});


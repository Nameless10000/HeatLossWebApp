import { defineConfig } from '@umijs/max';

export default defineConfig({
  antd: {},
  access: {},
  model: {},
  initialState: {},
  request: {},
  locale: {
    antd: true,
    default: 'ru-RU'
  },
  layout: {
    title: 'Cylindrical multilayer wall calculator',
  },
  routes: [
    {
      path: '/',
      redirect: '/home',
    },
    {
      path: '/home',
      component: 'Home/index'
    }
  ],
  npmClient: 'npm',
});


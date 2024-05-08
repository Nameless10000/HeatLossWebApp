import { defineConfig } from '@umijs/max';

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
    title: 'Cylindrical multilayer wall calculator',
  },
  routes: [
    {
      path: '/',
      redirect: '/home',
    },
    {
      title: 'Home',
      path: '/home',
      component: 'Home/index'
    },
    {
      title: 'Previous',
      path: '/previous',
      component: 'Previous/index'
    }
  ],
  npmClient: 'npm',
});


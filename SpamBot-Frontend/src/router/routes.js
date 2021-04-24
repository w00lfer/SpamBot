
const routes = [
  {
    path: '/',
    component: () => import('pages/RegisterLogin.vue')
  },
  {
    path: '/login',
    component: () => import('pages/RegisterLogin.vue')
  },
  {
    path: '/home',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/AddSpam.vue') }
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '*',
    component: () => import('pages/Error404.vue')
  }
]

export default routes

import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ArticlesView from '../views/ArticlesView.vue'
import AdminRoot from '../views/admin/Index.vue'
import LoginView from '../views/Login.vue'
import AdminHomeView from '../views/admin/HomeView.vue'
import AdminThemesView from '../views/admin/ThemesView.vue'
import AdminRootUsersView from '../views/admin/RootUsers.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/posts',
      name: 'Posts',
      component: ArticlesView
    },
    {
      path: '/admin',
      name: 'Admin',
      component: AdminRoot,
      beforeEnter: (to, from, next) => {
        if (localStorage.getItem('hyper-token')) next()
        else next({ name: 'Login' })
      },
      children: [
        {
          path: '',
          component: AdminHomeView
        },
        {
          path: 'themes',
          component: AdminThemesView
        },
        {
          path: 'root-users',
          component: AdminRootUsersView
        }
      ]
    },
    {
      path: '/auth',
      name: 'Login',
      component: LoginView
    }
  ]
})

export default router

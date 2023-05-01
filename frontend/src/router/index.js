import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ArticlesView from '../views/ArticlesView.vue'
import ArticleView from '../views/ArticleView.vue'
import AdminRoot from '../views/admin/Index.vue'
import LoginView from '../views/Login.vue'
import AdminHomeView from '../views/admin/HomeView.vue'
import AdminThemesView from '../views/admin/ThemesView.vue'
import AdminRootUsersView from '../views/admin/RootUsers.vue'
import MFAView from '../views/MfaView.vue'
import AdminBlogView from '../views/admin/BlogsView.vue'
import BlogEditorView from '../views/admin/BlogEditor.vue'
import AdminCommentsView from '../views/admin/CommentsView.vue'

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
      path: '/post/:id',
      name: 'Post',
      component: ArticleView
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
        },
        {
          path: 'blogs',
          component: AdminBlogView
        },
        {
          path: 'blogs/new',
          component: BlogEditorView,
          props: {
            isEditMode: false
          }
        },
        {
          path: 'blog/edit/:id',
          component: BlogEditorView,
          props: {
            isEditMode: true
          }
        },
        {
          path: 'comments',
          component: AdminCommentsView
        }
      ]
    },
    {
      path: '/auth',
      name: 'Login',
      component: LoginView
    },
    {
      path: '/mfa/:id',
      name: 'MFA Authentication',
      component: MFAView
    }
  ]
})

export default router

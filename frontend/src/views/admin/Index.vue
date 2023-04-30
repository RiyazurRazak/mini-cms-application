<script setup>
import { RouterView } from 'vue-router'
import { ref } from 'vue'
import PanelMenu from 'primevue/panelmenu'
import Divider from 'primevue/divider'

const items = ref([
  {
    key: 0,
    label: 'Home',
    to: '/admin/'
  },
  {
    key: 1,
    label: 'Pages',
    items: [
      {
        label: 'All Pages',
        to: '/admin/pages'
      },
      {
        label: 'New Page',
        to: '/admin/pages/new'
      }
    ]
  },
  {
    key: 2,
    label: 'Blogs',
    items: [
      {
        label: 'All Blogs',
        to: '/admin//blogs'
      },
      {
        label: 'New Blog',
        to: '/admin/blogs/new'
      }
    ]
  },
  {
    key: 3,
    label: 'Comments',
    to: '/admin/comments'
  },
  {
    key: 4,
    label: 'Themes',
    to: '/admin/themes'
  },
  {
    key: 5,
    label: 'Users',
    items: [
      {
        label: 'All Users',
        to: '/admin/users'
      },
      {
        label: 'Add Root Users',
        to: '/admin/users/new'
      }
    ]
  }
])
</script>

<template>
  <div class="panel">
    <div class="side-bar">
      <PanelMenu :model="items" />
    </div>
    <div class="main-panel">
      <RouterView />
      <Divider />
      <footer class="footer">
        <p>Crafted By Riyazur Razak | Made With Vue Js & .Net Core</p>
      </footer>
    </div>
  </div>
</template>

<style scoped>
.panel {
  display: flex;
}
.side-bar {
  width: 20vw;
  background-color: var(--highlight-bg);
  min-height: 100vh;
  padding: 20px;
}
.main-panel {
  width: 80vw;
  padding: 20px;
  min-height: 100vh;
}
.footer {
  color: #808080;
}
</style>

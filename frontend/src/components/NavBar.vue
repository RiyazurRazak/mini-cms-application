<script setup>
import MenuBar from 'primevue/menubar'
import { ref, onMounted } from 'vue'
import { useMetaStore } from '../stores/store'

const metaStore = useMetaStore()

const items = ref([
  {
    label: 'Home',
    to: '/'
  },
  {
    label: 'Pages',
    items: [
      {
        label: 'Vue Website',
        icon: 'pi pi-external-link',
        url: 'https://vuejs.org/'
      },
      {
        label: 'Router',
        icon: 'pi pi-upload',
        to: '/fileupload'
      }
    ]
  }
])

onMounted(() => {
  items.value = [
    {
      label: 'Home',
      to: '/'
    },
    {
      label: 'Blogs',
      to: '/posts'
    },
    {
      label: 'Pages',
      items: [
        {
          label: 'Vue Website',
          icon: 'pi pi-external-link',
          url: 'https://vuejs.org/'
        },
        {
          label: 'Router',
          icon: 'pi pi-upload',
          to: '/fileupload'
        },
        {
          label: 'Router 1',
          icon: 'pi pi-upload',
          to: '/fileupload'
        }
      ]
    }
  ]
})
</script>

<template>
  <MenuBar :model="items">
    <template #start>
      <h3 class="brand">{{ metaStore.data?.brandDetails?.brandName }}</h3>
    </template>
  </MenuBar>
</template>

<style scoped>
.brand {
  margin-right: 24px;
  font-family: 'Satisfy', cursive !important;
  letter-spacing: 3px;
}
</style>

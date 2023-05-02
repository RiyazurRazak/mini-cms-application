<script setup>
import MenuBar from 'primevue/menubar'
import { ref, watch } from 'vue'
import { useMetaStore } from '../stores/store'

const metaStore = useMetaStore()

const items = ref([
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
    items:
      metaStore?.data?.pages?.map((page) => ({ to: `/page/${page?.slug}`, label: page?.title })) ||
      []
  }
])

watch(
  metaStore,
  (state) => {
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
        items:
          state?.data?.pages?.map((page) => ({
            to: `/page/${page?.slug}`,
            label: page?.title
          })) || []
      }
    ]
  },
  { deep: true }
)
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

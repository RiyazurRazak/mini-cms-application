<script setup>
import { RouterView } from 'vue-router'
import { usePrimeVue } from 'primevue/config'
import { onMounted } from 'vue'
import axios from 'axios'
import { getActiveTheme, getMetaDetails } from './service/meta'
import { useMetaStore } from './stores/store'

const primeVue = usePrimeVue()

const metaStore = useMetaStore()

onMounted(async () => {
  const theme = await getActiveTheme()
  changeTheme(theme.data?.name)
  const meta = await getMetaDetails()
  metaStore.update(meta.data)
})

const changeTheme = (theme) => {
  if (theme === 'Nebula Dark Theme') {
    primeVue.changeTheme('bootstrap-light', 'bootstrap-dark', 'theme-link', () => {})
  } else {
    primeVue.changeTheme('bootstrap-dark', 'bootstrap-light', 'theme-link', () => {})
  }
}
axios.defaults.headers.Authorization = `Bearer ${localStorage.getItem('hyper-token')}`
</script>

<template>
  <RouterView />
</template>

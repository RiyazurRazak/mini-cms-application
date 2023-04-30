<script setup>
import { RouterView } from 'vue-router'
import { usePrimeVue } from 'primevue/config'
import { onMounted } from 'vue'
import axios from 'axios'
import { getActiveTheme } from './service/meta'

const primeVue = usePrimeVue()

onMounted(async () => {
  const res = await getActiveTheme()
  changeTheme(res.data?.name)
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

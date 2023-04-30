<script setup>
import { RouterView } from 'vue-router'
import { usePrimeVue } from 'primevue/config';
const PrimeVue = usePrimeVue();

const changeTheme = ()=>{
  PrimeVue.changeTheme('bootstrap-light', 'bootstrap-dark', 'theme-link', () => {});
}
</script>

<template>
  <RouterView />
</template>



<script setup>
import { ref, watch } from 'vue'
import Message from 'primevue/message'
import RadioButton from 'primevue/radiobutton'
import { usePrimeVue } from 'primevue/config'
import { updateTheme } from '../../service/admin/index'
import { getActiveTheme } from '../../service/meta'
import { useQuery } from '@tanstack/vue-query'
import ProgressSpinner from 'primevue/progressspinner'
import { useToast } from 'primevue/usetoast'
import Toast from 'primevue/toast'
const toast = useToast()

const primeVue = usePrimeVue()
const selectedTheme = ref(null)

const { data, isLoading, isError } = useQuery({
  queryKey: ['admin-theme'],
  queryFn: getActiveTheme,
  refetchOnWindowFocus: false,
  refetchOnMount: false
})

const updateThemeHandller = async (newTheme) => {
  try {
    if (newTheme === 'Nebula Dark Theme') {
      primeVue.changeTheme('bootstrap-light', 'bootstrap-dark', 'theme-link', () => {})
    } else {
      primeVue.changeTheme('bootstrap-dark', 'bootstrap-light', 'theme-link', () => {})
    }
    const res = await updateTheme({ name: newTheme })
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong Try Again' })
  }
}

watch(selectedTheme, (newData, oldData) => {
  if (newData !== oldData) updateThemeHandller(newData)
})

watch(data, (newData, _) => {
  if (newData !== undefined) selectedTheme.value = newData.data?.name || 'Milky White Theme'
})
</script>

<template>
  <Message :closable="false" severity="info">You can manage your Site Themes Here</Message>
  <h3>Available Themes</h3>
  <Toast />
  <ProgressSpinner v-if="isLoading || isError" />
  <div class="panel">
    <div class="panel panel-child">
      <RadioButton
        v-model="selectedTheme"
        inputId="theme1"
        name="pizza"
        value="Nebula Dark Theme"
      />
      <label for="theme1" class="ml-2">Nebula Dark Theme</label>
    </div>
    <div class="panel panel-child">
      <RadioButton
        v-model="selectedTheme"
        inputId="theme2"
        name="pizza"
        value="Milky White Theme"
      />
      <label for="theme2" class="ml-2">Milky White Theme</label>
    </div>
  </div>
</template>

<style scoped>
.panel {
  display: flex;
  align-items: center;
}
.panel-child {
  margin: 0 30px;
}
.panel-child label {
  margin-left: 18px;
}
</style>

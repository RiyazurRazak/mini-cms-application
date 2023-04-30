<script setup>
import { ref } from 'vue'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'

const username = ref(null)
const password = ref(null)

const loginHandller = () => {
  alert(username.value)
}
</script>

<template>
  <div class="panel">
    <h1 class="title">Login To Access Admin Panel</h1>
    <InputText v-model="username" type="text" placeholder="username" style="width: 30vw" />
    <br />
    <InputText v-model="password" type="password" placeholder="password" style="width: 30vw" />
    <br />
    <Button raised rounded size="large" @click="loginHandller">Login</Button>
  </div>
</template>

<style scoped>
.panel {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
}
</style>

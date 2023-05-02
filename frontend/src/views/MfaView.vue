<script setup>
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
import axios from 'axios'
import { baseUrl } from '../constants/constants'

const router = useRouter()
const route = useRoute()
const userId = route.params.id
const mfaCode = ref(null)

const verifyMfaHandller = async () => {
  try {
    const res = await axios.post(`${baseUrl}/auth/verify`, {
      code: mfaCode.value,
      id: userId
    })
    const user = res.data
    axios.defaults.headers.Authorization = `Bearer ${user.token}`
    localStorage.setItem('hyper-token', user.token)
    localStorage.setItem('hyper-mail', user.email)
    router.replace('/admin')
  } catch (err) {
    alert('Something Went Wrong Try Again')
  }
}
</script>

<template>
  <div class="panel">
    <p>Powered By</p>
    <img
      src="https://mobiletrans.wondershare.com/images/article/transfer-google-authenticator-to-iphone-1.jpg"
      alt="logo"
      class="logo"
    />
    <h3>Enter Your 6 Digit Mfa code</h3>
    <br />
    <InputText v-model="mfaCode" placeholder="******" type="number" />
    <br />
    <br />
    <Button rounded @click="verifyMfaHandller">Verify</Button>
  </div>
</template>

<style scoped>
.panel {
  width: 50vw;
  height: 50vh;
  margin: auto auto;
  text-align: center;
  margin-top: 10%;
}
.logo {
  width: 300px;
}
</style>

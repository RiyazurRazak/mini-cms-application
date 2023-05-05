<script setup>
import Message from 'primevue/message'
import { ref, watch, toRaw, isProxy } from 'vue'
import InputText from 'primevue/inputtext'
import Password from 'primevue/password'
import InputNumber from 'primevue/inputnumber'
import Toast from 'primevue/toast'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery, useQueryClient } from '@tanstack/vue-query'
import { getRootUsers, addUser, deleteUser, requestMfa, verifyMfa } from '../../service/admin/index'
import Button from 'primevue/button'
import { useToast } from 'primevue/usetoast'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Divider from 'primevue/divider'
import Dialog from 'primevue/dialog'
import Dropdown from 'primevue/dropdown'

const toast = useToast()
const queryClient = useQueryClient()
const { data, isLoading, isError } = useQuery({
  queryKey: ['admin-rootusers'],
  queryFn: getRootUsers
})

const isDialogOpen = ref(false)
const emailAddress = ref(null)
const username = ref(null)
const password = ref(null)
const role = ref(null)
const mfaData = ref({ isLoaded: false })
const mfaCode = ref(null)
const isActiveUserMfa = ref(false)

const roles = [
  { name: 'Select User', value: null },
  { name: 'Hyper', value: 'hyper' },
  { name: 'Content Creator', value: 'content-creator' }
]

watch(data, (newState, _) => {
  if (newState !== undefined) {
    const loggedMail = localStorage.getItem('hyper-mail')
    if (isProxy(newState)) newState = toRaw(newState)
    const isLoggedUser = newState.data.filter((user) => user.email === loggedMail)
    if (isLoggedUser) {
      isActiveUserMfa.value = isLoggedUser[0]?.isMfaEnabled
    }
  }
})

const openDialogHandller = () => (isDialogOpen.value = true)

const addUserHandller = async () => {
  try {
    if (
      emailAddress.value === null ||
      password.value === null ||
      username.value === null ||
      role.value === null
    ) {
      toast.add({ severity: 'warn', detail: 'All fields are required' })
      return
    }
    const payload = {
      email: emailAddress.value,
      password: password.value,
      username: username.value,
      role: role.value
    }
    await addUser(payload)
    queryClient.invalidateQueries(['admin-rootusers'])
    toast.add({ severity: 'success', detail: 'New User Added!!' })
    isDialogOpen.value = false
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}
const deleteHandller = async (id) => {
  try {
    await deleteUser(id)
    queryClient.invalidateQueries(['admin-rootusers'])
    toast.add({ severity: 'warn', detail: 'User Removed Successfully' })
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}

const addMfaHandller = async () => {
  try {
    const res = await requestMfa()
    mfaData.value = { ...res.data, isLoaded: true }
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}

const verifyMfaHandller = async () => {
  try {
    const res = await verifyMfa(mfaCode.value)
    if (res.data?.status) {
      toast.add({
        severity: 'success',
        detail: 'MFA is added to this account !!\n Addditional security added!! great'
      })
      mfaData.value = { isLoaded: false }
    }
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}
</script>

<template>
  <Message :closable="false" severity="info">You can manage Root Users Here</Message>
  <Toast />
  <ProgressSpinner v-if="isLoading || isError" />
  <template v-else>
    <Button @click="openDialogHandller">Create New User</Button>
    <Divider />
    <DataTable :value="data?.data">
      <Column field="email" header="Email Address"></Column>
      <Column field="role" header="Role"></Column>
      <Column field="isMfaEnabled" header="Mfa Enabled"></Column>
      <Column field="id" header="Actions">
        <template #body="slotprops">
          <Button rounded severity="danger" @click="deleteHandller(slotprops.data.id)"
            >Delete</Button
          >
        </template>
      </Column>
    </DataTable>
    <Divider />
    <h3>Advance Settings</h3>
    <Button v-if="isActiveUserMfa" disabled severity="success">Mfa Already Added</Button>
    <Button v-else rounded @click="addMfaHandller" :disabled="isActiveUserMfa || mfaData.isLoaded">
      Enable 2FA For Me</Button
    >
    <div v-if="mfaData.isLoaded">
      <h4>Scan The Qr Code In Your MFA Client (eg: Google Authenticator)</h4>
      <img :src="mfaData.qrCodeImageUrl" alt="mfa qr code" class="qr-code" />
      <p>Enter Your Code To Finish The Configuration</p>
      <InputNumber
        v-model="mfaCode"
        inputId="integeronly"
        style="margin-bottom: 16px; width: 50%"
        placeholder="enter your security code here"
      />
      <br />

      <Button rounded severity="help" @click="verifyMfaHandller">Verify MFA Client</Button>
    </div>
    <Dialog
      v-model:visible="isDialogOpen"
      modal
      header="Add New Root User"
      :style="{ width: '50vw' }"
    >
      <InputText placeholder="email address" type="email" v-model="emailAddress" class="input" />
      <InputText placeholder="username" type="text" v-model="username" class="input" />
      <Password placeholder="****" v-model="password" class="input" input-style="width: 100%" />
      <Dropdown
        v-model="role"
        :options="roles"
        optionLabel="name"
        placeholder="Select a Role"
        class="w-full input"
        option-value="value"
      />
      <Button rounded @click="addUserHandller">Create User</Button>
    </Dialog>
  </template>
</template>

<style scoped>
.input {
  width: 100%;
  margin: 10px 0;
}
.qr-code {
  width: 300px;
  height: 300px;
  margin-bottom: 20px;
}
</style>

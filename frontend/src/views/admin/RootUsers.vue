<script setup>
import Message from 'primevue/message'
import { ref, watch } from 'vue'
import InputText from 'primevue/inputtext'
import Password from 'primevue/password'
import Toast from 'primevue/toast'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery, useQueryClient } from '@tanstack/vue-query'
import { getRootUsers, addUser, deleteUser } from '../../service/admin/index'
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

const roles = [
  { name: 'Select User', value: null },
  { name: 'Hyper', value: 'hyper' },
  { name: 'Content Creator', value: 'content-creator' }
]

const openDialogHandller = () => (isDialogOpen.value = true)

const addUserHandller = async () => {
  try {
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
      <Column field="id" header="Actions">
        <template #body="slotprops">
          <Button rounded severity="danger" @click="deleteHandller(slotprops.data.id)"
            >Delete</Button
          >
        </template>
      </Column>
    </DataTable>
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
</style>

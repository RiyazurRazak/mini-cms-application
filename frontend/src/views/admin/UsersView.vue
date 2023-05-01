<script setup>
import Message from 'primevue/message'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery } from '@tanstack/vue-query'
import { getUsers } from '../../service/admin/index'
import Avatar from 'primevue/avatar'
import md5 from 'md5'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Toast from 'primevue/toast'

const { data, isLoading, isError } = useQuery({
  queryKey: ['admin-comment-users'],
  queryFn: getUsers
})
</script>

<template>
  <Toast />
  <Message :closable="false" severity="info"
    >You can view the users interact with the page here</Message
  >
  <br />
  <ProgressSpinner v-if="isLoading || isError" />
  <template v-else>
    <DataTable :value="data?.data">
      <Column field="name" header="Name"></Column>
      <Column field="emailAddress" header="Email"></Column>
      <Column field="emailAddress" header="Avatar">
        <template #body="slotprops">
          <Avatar
            :image="`https://www.gravatar.com/avatar/${md5(slotprops.data?.emailAddress)}?s=200`"
            size="large"
            shape="square"
          />
        </template>
      </Column>
    </DataTable>
  </template>
</template>

<style scoped></style>

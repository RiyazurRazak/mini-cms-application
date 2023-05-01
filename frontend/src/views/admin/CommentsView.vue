<script setup>
import Message from 'primevue/message'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery, useQueryClient } from '@tanstack/vue-query'
import { getComments, deleteComment } from '../../service/admin/index'
import Button from 'primevue/button'
import { useToast } from 'primevue/usetoast'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Toast from 'primevue/toast'

const queryClient = useQueryClient()
const toast = useToast()

const { data, isLoading, isError } = useQuery({
  queryKey: ['admin-users'],
  queryFn: getComments
})

const deleteHandller = async (id) => {
  try {
    const res = await deleteComment(id)
    if (res.data?.status) {
      toast.add({ severity: 'success', detail: 'comment removed' })
      queryClient.invalidateQueries(['admin-comments'])
    }
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}
</script>

<template>
  <Toast />
  <Message :closable="false" severity="info">You can manage all comment of blogs here</Message>
  <br />
  <ProgressSpinner v-if="isLoading || isError" />
  <template v-else>
    <DataTable :value="data?.data">
      <Column field="message" header="Comment"></Column>
      <Column field="author" header="Author"></Column>
      <Column field="article" header="Blog Title"></Column>
      <Column field="id" header="Actions">
        <template #body="slotprops">
          <Button rounded severity="danger" @click="deleteHandller(slotprops.data.id)"
            >Delete</Button
          >
        </template>
      </Column>
    </DataTable>
  </template>
</template>

<style scoped></style>

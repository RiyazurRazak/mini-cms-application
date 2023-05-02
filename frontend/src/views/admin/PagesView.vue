<script setup>
import { ref, watch } from 'vue'
import Message from 'primevue/message'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery, useQueryClient } from '@tanstack/vue-query'
import { getPages, changePageVisibility, deletePage } from '../../service/admin/index'
import Button from 'primevue/button'
import { useToast } from 'primevue/usetoast'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Toast from 'primevue/toast'

const queryClient = useQueryClient()
const toast = useToast()

const { data, isLoading, isError } = useQuery({
  queryKey: ['admin-pages'],
  queryFn: getPages
})

const changeVisibilityHandller = async (id) => {
  try {
    const res = await changePageVisibility(id)
    if (res.data?.status) {
      toast.add({ severity: 'success', detail: 'Page Visibility Updated' })
      queryClient.invalidateQueries(['admin-pages'])
    }
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}

const deleteBlogHandller = async (id) => {
  try {
    const res = await deletePage(id)
    if (res.data?.status) {
      toast.add({ severity: 'success', detail: 'Page Deleted' })
      queryClient.invalidateQueries(['admin-pages'])
    }
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}
</script>

<template>
  <Toast />
  <Message :closable="false" severity="info">You can manage your pages visibility here</Message>
  <br />
  <ProgressSpinner v-if="isLoading || isError" />
  <template v-else>
    <DataTable :value="data?.data">
      <Column field="title" header="Title"></Column>
      <Column field="slug" header="Slug"></Column>
      <Column field="isPublic" header="Public Visible"></Column>
      <Column field="id" header="Actions">
        <template #body="slotprops">
          <span class="p-buttonset">
            <Button
              rounded
              severity="secondary"
              @click="changeVisibilityHandller(slotprops.data.id)"
              >Change Visibility</Button
            >
            <Button rounded severity="danger" @click="deleteBlogHandller(slotprops.data.id)"
              >Delete</Button
            >
          </span>
        </template>
      </Column>
    </DataTable>
  </template>
</template>

<style scoped></style>

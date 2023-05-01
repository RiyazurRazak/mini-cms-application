<script setup>
import { ref, watch } from 'vue'
import Message from 'primevue/message'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery, useQueryClient } from '@tanstack/vue-query'
import { getBlogs, changeBlogVisibility, deleteBlog } from '../../service/admin/index'
import Button from 'primevue/button'
import { useToast } from 'primevue/usetoast'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Toast from 'primevue/toast'
import { useRouter } from 'vue-router'

const router = useRouter()

const queryClient = useQueryClient()
const toast = useToast()

const { data, isLoading, isError } = useQuery({
  queryKey: ['admin-blogs'],
  queryFn: getBlogs
})

const changeVisibilityHandller = async (id) => {
  try {
    const res = await changeBlogVisibility(id)
    if (res.data?.status) {
      toast.add({ severity: 'success', detail: 'Blog Visibility Updated' })
      queryClient.invalidateQueries(['admin-blogs'])
    }
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}

const redirectToEdit = (id) => {
  router.replace(`/admin/blog/edit/${id}`)
}

const deleteBlogHandller = async (id) => {
  try {
    const res = await deleteBlog(id)
    if (res.data?.status) {
      toast.add({ severity: 'success', detail: 'Blog Deleted' })
      queryClient.invalidateQueries(['admin-blogs'])
    }
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}
</script>

<template>
  <Toast />
  <Message :closable="false" severity="info">You can manage your blogs visibility here</Message>
  <br />
  <ProgressSpinner v-if="isLoading || isError" />
  <template v-else>
    <DataTable :value="data?.data">
      <Column field="title" header="Title"></Column>
      <Column field="author" header="Author"></Column>
      <Column field="status" header="Public Visible"></Column>
      <Column field="likes" header="Likes"></Column>
      <Column field="id" header="Actions">
        <template #body="slotprops">
          <span class="p-buttonset">
            <Button
              rounded
              severity="secondary"
              @click="changeVisibilityHandller(slotprops.data.id)"
              >Change Visibility</Button
            >
            <Button rounded severity="warning" @click="redirectToEdit(slotprops.data.id)"
              >Edit</Button
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

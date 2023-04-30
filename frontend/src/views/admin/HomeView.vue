<script setup>
import { ref, watch } from 'vue'
import Message from 'primevue/message'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import Toast from 'primevue/toast'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery } from '@tanstack/vue-query'
import { getBrandDetails, updateBrandDetails } from '../../service/admin/index'
import Button from 'primevue/button'
import { useToast } from 'primevue/usetoast'
const toast = useToast()

const brandName = ref(null)
const brandDescription = ref(null)
const { data, isLoading, isError } = useQuery({
  queryKey: ['admin-home'],
  queryFn: getBrandDetails
})

watch(data, (newData, _) => {
  if (newData !== undefined) {
    brandName.value = newData.data?.brandName
    brandDescription.value = newData.data?.brandDescription
  }
})

const updateHandller = async () => {
  try {
    const res = await updateBrandDetails({
      brandName: brandName.value,
      brandDescription: brandDescription.value
    })
    if (res.data) {
      brandName.value = res.data?.brandName
      brandDescription.value = res.data?.brandDescription
      toast.add({ severity: 'success', detail: 'Details Updated Succesfully' })
    }
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'something went wrong' })
  }
}
</script>

<template>
  <h3>Welcome Admin</h3>
  <Toast />
  <Message :closable="false" severity="info">You can manage your home page contents here</Message>
  <br />
  <ProgressSpinner v-if="isLoading || isError" />
  <template v-else>
    <p>Brand Name</p>
    <InputText
      type="text"
      placeholder="Brand Name"
      v-model="brandName"
      style="width: 50vw; margin-bottom: 20px"
    />
    <p>Brand Description</p>
    <Textarea
      type="text"
      placeholder="Brand Description"
      v-model="brandDescription"
      style="width: 50vw; margin-bottom: 20px"
      rows="3"
    />
    <br />
    <Button rounded @click="updateHandller">Update Content</Button>
  </template>
</template>

<style scoped></style>

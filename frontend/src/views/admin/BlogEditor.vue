<script setup>
import { ref } from 'vue'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import Editor from 'primevue/editor'
import Button from 'primevue/button'
import Message from 'primevue/message'
import Toast from 'primevue/toast'
import { useToast } from 'primevue/usetoast'
import { createBlog, getBlog, editBlog } from '../../service/admin/index'
import { useRoute } from 'vue-router'

const route = useRoute()
const props = defineProps({
  isEditMode: {
    type: Boolean,
    required: true
  }
})

const toast = useToast()
const title = ref(null)
const description = ref(null)
const body = ref(null)

const getBlogDetails = async (id) => {
  try {
    const res = await getBlog(id)
    title.value = res.data?.title
    description.value = res.data?.description
    body.value = res.data?.body
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went wrong! try again' })
  }
}

if (props.isEditMode) {
  getBlogDetails(route.params.id)
} else {
  title.value = null
  description.value = null
  body.value = null
}

const publishHandller = async () => {
  try {
    if (props.isEditMode) {
      const res = editBlog(route.params.id, {
        title: title.value,
        description: description.value,
        body: body.value
      })
      toast.add({ severity: 'success', detail: 'Blog Updated' })
    } else {
      if (title.value === null || description.value === null || body.value === null) {
        toast.add({ severity: 'warn', detail: 'all fields are required' })
        return
      }
      const res = await createBlog({
        title: title.value,
        description: description.value,
        body: body.value
      })
      toast.add({ severity: 'success', detail: 'Blog Published' })
    }
    title.value = null
    description.value = null
    body.value = null
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'Something Went Wrong' })
  }
}
</script>

<template>
  <Message v-if="isEditMode" :closable="false" severity="info"
    >You can Update/ Edit Your Article Here</Message
  >
  <Message v-else :closable="false" severity="info">You can create your article here</Message>
  <Toast />
  <InputText placeholder="Your Title Goes Here" v-model="title" class="input" />
  <br />
  <Textarea placeholder="description" v-model="description" class="input"></Textarea>
  <Editor v-model="body" placeholder="content" editor-style="height: 320px" />
  <br />
  <Button rounded @click="publishHandller">{{ isEditMode ? 'Update' : 'Publish' }}</Button>
</template>

<style scoped>
.input {
  width: 100%;
  margin-bottom: 20px;
}
</style>

<script setup>
import Avatar from 'primevue/avatar'
import Divider from 'primevue/divider'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import Button from 'primevue/button'
import md5 from 'md5'
import Toast from 'primevue/toast'
import { useToast } from 'primevue/usetoast'
import { ref, onMounted } from 'vue'
import { addComment, getComments } from '../service/blog'
const props = defineProps({
  blogId: {
    type: String,
    required: true
  }
})

const toast = useToast()

const comments = ref([])
const emailAddress = ref(null)
const name = ref(null)
const message = ref(null)

const getAllComments = () => {
  getComments(props.blogId)
    .then((res) => {
      comments.value = res.data
    })
    .catch((err) => {
      toast.add({ severity: 'error', detail: "Can't load comments" })
    })
}

onMounted(() => {
  getAllComments()
})

const addCommentHandller = async () => {
  try {
    if (emailAddress.value === null || name.value === null) {
      toast.add({ severity: 'warn', detail: 'Email address and name are required' })
      return
    }

    const payload = {
      emailAddress: emailAddress.value,
      name: name.value,
      message: message.value
    }
    const res = await addComment(props.blogId, payload)
    emailAddress.value = null
    name.value = null
    message.value = null
    toast.add({ severity: 'success', detail: 'comment added' })
    getAllComments()
  } catch (err) {
    console.error(err)
    toast.add({ severity: 'error', detail: 'something went wrong' })
  }
}
</script>

<template>
  <Toast />
  <h1>Share Your Opinion</h1>
  <Divider />
  <InputText type="email" placeholder="emailaddress" class="input" v-model="emailAddress" />
  <br />
  <InputText type="text" placeholder="name" class="input" v-model="name" />
  <br />
  <Textarea placeholder="your comment" class="input" rows="5" v-model="message"></Textarea>
  <br />
  <Button rounded @click="addCommentHandller">Comment</Button>
  <Divider />
  <h1>Comments</h1>
  <div v-for="comment in comments">
    <div class="header">
      <Avatar
        :image="`https://www.gravatar.com/avatar/${md5(comment?.email)}?s=200`"
        size="xlarge"
        shape="circle"
      />
      <h4>{{ comment?.name }}</h4>
    </div>
    <p>{{ comment?.message }}</p>
    <Divider />
  </div>
</template>

<style scoped>
.header {
  display: flex;
  align-items: center;
}
.header h4 {
  margin-left: 20px;
}
.input {
  margin-bottom: 16px;
  width: 40%;
}
</style>

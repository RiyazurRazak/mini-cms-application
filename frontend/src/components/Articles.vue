<script setup>
import Card from 'primevue/card'
import Button from 'primevue/button'
import { getAllBlogs, getTopBlogs } from '../service/blog'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  title: {
    type: String,
    required: true
  }
})

const data = ref([])

if (props.title === 'Top Articles') {
  getTopBlogs()
    .then((res) => {
      data.value = res.data
    })
    .catch((err) => {
      data.value = []
      console.error(err)
    })
} else {
  getAllBlogs()
    .then((res) => {
      data.value = res.data
    })
    .catch((err) => {
      data.value = []
      console.error(err)
    })
}

const redirectToBlogPageHandller = (id) => {
  router.replace(`/post/${id}`)
}
</script>

<template>
  <h2 class="title">{{ title }}</h2>
  <div class="panel">
    <Card class="card" v-for="blog in data">
      <template #title> {{ blog?.title }} </template>
      <template #content>
        <p>
          {{ blog?.description }}
        </p>
        <p>By: {{ blog?.author }}</p>
        <p>Likes: {{ blog?.likes }}</p>
        <p>Published At: {{ new Date(blog?.createdAt).toDateString() }}</p>
      </template>
      <template #footer>
        <Button rounded outlined @click="redirectToBlogPageHandller(blog?.id)">Read More</Button>
      </template>
    </Card>
  </div>
</template>

<style scoped>
.title {
  text-align: center;
  font-family: 'Satisfy', cursive !important;
}
.card {
  width: 40%;
  height: 350px;
  margin: 20px;
}
.panel {
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 20px 5%;
  flex-wrap: wrap;
}
</style>

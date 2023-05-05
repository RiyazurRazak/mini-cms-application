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
  router.push(`/post/${id}`)
}
</script>

<template>
  <h2 class="title">{{ title }}</h2>
  <div class="panel">
    <Card class="card" v-for="blog in data">
      <template #header>
        <img
          class="header-img"
          :src="`https://picsum.photos/280/150?random=${blog?.id}`"
          alt="header"
        />
      </template>
      <template #title> {{ blog?.title }} </template>
      <template #content>
        <p>{{ blog?.description.substring(0, 40) }}...</p>
        <Button @click="redirectToBlogPageHandller(blog?.id)">Read More</Button>
      </template>
      <template #footer>
        <div class="row">
          <div>
            <p>{{ blog?.likes }}</p>
            <h6 style="margin: 0">Likes</h6>
          </div>
          <div>
            <p>{{ blog?.author }}</p>
            <h6 style="margin: 0">Author</h6>
          </div>
          <div>
            <p>{{ new Date(blog?.createdAt).toLocaleDateString() }}</p>
            <h6 style="margin: 0">Published</h6>
          </div>
        </div>
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
  width: 300px;
  height: 500px;
  margin: 20px;
}
.panel {
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 20px 5%;
  flex-wrap: wrap;
}
.header-img {
  margin: 10px;
  width: 280px;
  border-radius: 5px;
}
.row {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.row div {
  text-align: center;
}
</style>

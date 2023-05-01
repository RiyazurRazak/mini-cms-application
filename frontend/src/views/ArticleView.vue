<script setup>
import { useRoute } from 'vue-router'
import { ref } from 'vue'
import { getBlog, addLike } from '../service/blog'
import NavBar from '../components/NavBar.vue'
import Divider from 'primevue/divider'
import Button from 'primevue/button'
import Comments from '../components/Comments.vue'

const route = useRoute()

const data = ref(null)

getBlog(route.params.id)
  .then((res) => {
    data.value = res.data
  })
  .catch((err) => {
    alert('Something Went Wrong\n Page breaks!!')
  })

const likeHandller = async () => {
  try {
    const res = await addLike(route.params.id)
    data.value = res.data
  } catch (err) {
    console.error(err)
  }
}
</script>

<template>
  <NavBar />
  <h1 class="title">{{ data?.title }}</h1>
  <h4 class="author">Written By: {{ data?.author }}</h4>
  <p class="date">Published At: {{ new Date(data?.createdAt).toDateString() }}</p>
  <p class="date">{{ data?.likes }} Likes</p>
  <Divider />
  <div class="article">
    <article v-html="data?.body"></article>
    <Button rounded outlined @click="likeHandller">Like This post</Button>
    <Divider />
    <Comments :blog-id="route.params.id" />
  </div>
</template>

<style scoped>
.title {
  text-align: center;
}
.author {
  text-align: center;
  color: #808080;
}
.date {
  text-align: center;
}
.article {
  width: 70vw;
  margin: auto;
  text-align: justify;
}
</style>

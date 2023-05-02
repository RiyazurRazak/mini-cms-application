<script setup>
import { useRoute } from 'vue-router'
import { ref, onMounted, watch } from 'vue'
import { getPage } from '../service/pages'
import NavBar from '../components/NavBar.vue'
import Divider from 'primevue/divider'
const route = useRoute()

const isNotfound = ref(false)
const pageData = ref(null)

const getPageDetails = async (slug) => {
  try {
    const res = await getPage(slug)
    pageData.value = res.data
  } catch (err) {
    console.error(err)
    isNotfound.value = true
  }
}

watch(route, (slug) => {
  getPageDetails(slug.params.slug)
})

onMounted(() => {
  getPageDetails(route.params.slug)
})
</script>

<template>
  <NavBar />
  <br />
  <div v-if="isNotfound" class="banner">
    <img src="/images/notfound.jpg" alt="not found" class="img" />
  </div>
  <main v-else v-html="pageData?.domElements" />
  <Divider />
  <footer class="footer">
    <p>Crafted By Riyazur Razak | Made With Vue Js & .Net Core | Powered by Grapesjs</p>
  </footer>
</template>

<style scoped>
.banner {
  width: 80%;
  height: 500px;
  margin: auto;
}
.img {
  width: 100%;
  height: 100%;
  object-fit: contain;
}
.footer {
  text-align: center;
}
</style>

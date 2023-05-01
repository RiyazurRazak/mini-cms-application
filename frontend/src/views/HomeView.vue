<script setup>
import Divider from 'primevue/divider'
import Articles from '../components/Articles.vue'
import Gallery from '../components/Gallery.vue'
import NavBar from '../components/NavBar.vue'
import Stats from '../components/Stats.vue'
import TopComments from '../components/TopComments.vue'
import { useMetaStore } from '../stores/store'

const metaStore = useMetaStore()
</script>

<template>
  <NavBar />
  <main>
    <div class="header-root">
      <img
        src="https://cdn.pixabay.com/photo/2016/05/24/16/48/mountains-1412683_1280.png"
        alt="header"
        class="header-img"
      />
      <p class="header-content">{{ metaStore.data?.brandDetails?.brandName }}</p>
    </div>
    <Articles title="Top Articles" />
    <br />
    <Gallery />
    <Divider />
    <Stats />
    <Divider />
    <TopComments />
  </main>
</template>
<style scoped>
.header-root {
  position: relative;
}
.header-img {
  width: 100%;
  height: 450px;
  object-fit: cover;
  filter: blur(2px);
}
.header-content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 38px;
  font-weight: 800;
  color: #000;
}
</style>

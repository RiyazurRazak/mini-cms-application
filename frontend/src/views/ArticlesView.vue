<script setup>
import Articles from '../components/Articles.vue'
import NavBar from '../components/NavBar.vue'
</script>

<template>
  <NavBar />
  <Articles title="All Posts" />
</template>

<style scoped>
.title {
  text-align: center;
  font-family: 'Satisfy', cursive !important;
}
.card {
  width: 40%;
  margin: 20px;
}
</style>

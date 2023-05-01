<script setup>
import Knob from 'primevue/knob'
import { ref } from 'vue'
import { useMetaStore } from '../stores/store'

const metaStore = useMetaStore()
</script>

<template>
  <div class="panel">
    <div class="child">
      <Knob :model-value="metaStore.data?.totalComments" readonly />
      <h3>Total Comments</h3>
    </div>
    <div class="child">
      <Knob :model-value="metaStore.data?.totalArticles" readonly />
      <h3>Total Articles Posted</h3>
    </div>
    <div class="child">
      <Knob :model-value="metaStore.data?.totalLikes" readonly />
      <h3>Total Likes</h3>
    </div>
  </div>
</template>

<style scoped>
.panel {
  display: flex;
  align-items: center;
  justify-content: center;
}
.child {
  margin: 0 26px;
  text-align: center;
}
</style>

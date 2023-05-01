<script setup>
import Knob from 'primevue/knob'
import { ref } from 'vue'

const value = ref(50)
</script>

<template>
  <div class="panel">
    <div class="child">
      <Knob v-model="value" readonly />
      <h3>Total Comments</h3>
    </div>
    <div class="child">
      <Knob v-model="value" readonly />
      <h3>Total Articles Posted</h3>
    </div>
    <div class="child">
      <Knob v-model="value" readonly />
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

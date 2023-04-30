<script setup>
import Card from 'primevue/card'
import Button from 'primevue/button'

defineProps({
  title: {
    type: String,
    required: true
  }
})
</script>

<template>
  <h2 class="title">{{ title }}</h2>
  <div class="panel">
    <Card class="card">
      <template #title> Simple Card </template>
      <template #content>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Inventore sed consequuntur error
          repudiandae numquam deserunt quisquam repellat libero asperiores earum nam nobis, culpa
          ratione quam perferendis esse, cupiditate neque quas!
        </p>
      </template>
      <template #footer>
        <Button rounded outlined>Read More</Button>
      </template>
    </Card>
    <Card class="card">
      <template #title> Simple Card </template>
      <template #content>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Inventore sed consequuntur error
          repudiandae numquam deserunt quisquam repellat libero asperiores earum nam nobis, culpa
          ratione quam perferendis esse, cupiditate neque quas!
        </p>
      </template>
      <template #footer>
        <Button rounded outlined>Read More</Button>
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
  margin: 20px;
}
.panel {
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 20px 5%;
}
</style>

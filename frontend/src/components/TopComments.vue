<script setup>
import Fieldset from 'primevue/fieldset'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery } from '@tanstack/vue-query'
import { getTopComments } from '../service/blog'

const { data, isLoading, isError } = useQuery({
  queryKey: ['top-comments'],
  queryFn: getTopComments
})
</script>

<template>
  <h2 class="title">Top Comments</h2>
  <ProgressSpinner v-if="isLoading || isError" />
  <div v-else class="panel">
    <Fieldset :legend="comment?.name" v-for="comment in data.data" class="box">
      <p>
        {{ comment?.message }}
      </p>
    </Fieldset>
  </div>
</template>

<style scoped>
.title {
  text-align: center;
  font-family: 'Satisfy', cursive !important;
}
.panel {
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 10%;
}
.box {
  margin: 0 30px;
}
</style>

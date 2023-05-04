<script setup>
import { ref, watch } from 'vue'
import Message from 'primevue/message'
import ProgressSpinner from 'primevue/progressspinner'
import { useQuery } from '@tanstack/vue-query'
import { getUsers, getCommentStatus } from '../../service/admin/index'
import Chart from 'primevue/chart'
import Avatar from 'primevue/avatar'
import md5 from 'md5'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Toast from 'primevue/toast'

const { data, isLoading, isError } = useQuery({
  queryKey: ['admin-comment-users'],
  queryFn: getUsers
})

const {
  data: statsData,
  isLoading: statsLoading,
  isError: statsError
} = useQuery({
  queryKey: ['admin-comment-stats'],
  queryFn: getCommentStatus
})
const chartData = ref()
const chartOptions = ref({
  scales: {
    y: {
      beginAtZero: true
    }
  }
})

watch(statsData, (newData, _) => {
  if (data !== undefined) {
    const label = []
    const data = []
    newData.data.forEach((stats) => {
      label.push(stats?.username)
      data.push(stats?.comments)
    })
    chartData.value = {
      labels: label,
      datasets: [
        {
          label: 'Comments',
          data: data,
          borderWidth: 1
        }
      ]
    }
  }
})
</script>

<template>
  <Toast />
  <Message :closable="false" severity="info"
    >You can view the users interact with the page here</Message
  >
  <br />
  <ProgressSpinner v-if="isLoading || isError || statsError || statsLoading" />
  <template v-else>
    <h2>Stats</h2>
    <p>No of Comments Comment by the users</p>
    <Chart type="bar" :data="chartData" :options="chartOptions" style="height: 250px" />
    <br />
    <DataTable :value="data?.data">
      <Column field="name" header="Name"></Column>
      <Column field="emailAddress" header="Email"></Column>
      <Column field="emailAddress" header="Avatar">
        <template #body="slotprops">
          <Avatar
            :image="`https://www.gravatar.com/avatar/${md5(slotprops.data?.emailAddress)}?s=200`"
            size="large"
            shape="square"
          />
        </template>
      </Column>
    </DataTable>
  </template>
</template>

<style scoped></style>

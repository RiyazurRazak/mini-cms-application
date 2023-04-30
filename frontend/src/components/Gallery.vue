<script setup>
import Galleria from 'primevue/galleria'
import { ref } from 'vue'

const images = ref([
  {
    itemImageSrc: 'https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg',
    thumbnailImageSrc: 'https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg',
    alt: 'Description for Image 1',
    title: 'Title 1'
  },
  {
    itemImageSrc: 'https://cdn.pixabay.com/photo/2015/12/01/20/28/road-1072821__480.jpg',
    thumbnailImageSrc: 'https://cdn.pixabay.com/photo/2015/12/01/20/28/road-1072821__480.jpg',
    alt: 'Description for Image 1',
    title: 'Title 1'
  },
  {
    itemImageSrc: 'https://picsum.photos/200/300',
    thumbnailImageSrc: 'https://picsum.photos/200/300',
    alt: 'Description for Image 1',
    title: 'Title 1'
  }
])
const responsiveOptions = ref([
  {
    breakpoint: '991px',
    numVisible: 4
  },
  {
    breakpoint: '767px',
    numVisible: 3
  },
  {
    breakpoint: '575px',
    numVisible: 1
  }
])
</script>

<template>
  <h2 class="title">Gallery</h2>
  <div class="panel">
    <Galleria
      :value="images"
      :numVisible="5"
      :autoPlay="true"
      :circular="true"
      :responsiveOptions="responsiveOptions"
      :showThumbnails="false"
    >
      <template #item="slotProps">
        <img
          :src="slotProps.item.itemImageSrc"
          :alt="slotProps.item.alt"
          style="width: 100%; height: 450px; object-fit: cover"
          :title="slotProps.item.title"
        />
      </template>
    </Galleria>
  </div>
</template>

<style scoped>
.title {
  text-align: center;
  font-family: 'Satisfy', cursive !important;
}
.panel {
  margin: 0 10%;
  margin-bottom: 30px;
}
</style>

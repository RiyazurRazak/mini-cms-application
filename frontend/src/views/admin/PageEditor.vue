<script setup>
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
import juice from 'juice'
import { addPage } from '../../service/admin/index'

const router = useRouter()
let editor = null
const title = ref(null)
const isShowEditor = ref(false)

const savePageHandller = async (domElements) => {
  try {
    await addPage({
      title: title.value,
      domElements
    })
    alert('Page Deployed Successfull')
    router.push({ path: '/admin/pages' })
  } catch (err) {
    console.log(err)
    alert('Something Went Wrong')
  }
}

const save = () => {
  const domElements = juice(`<style>${editor.getCss()}</style>${editor.getHtml()}`)
  savePageHandller(domElements)
}

const toggleEditorHandller = () => {
  isShowEditor.value = true
  editor = grapesjs.init({
    container: '#editor',
    fromElement: true,
    width: 'auto',
    storageManager: false,
    plugins: [
      'gjs-blocks-basic',
      'grapesjs-plugin-forms',
      'grapesjs-component-countdown',
      'grapesjs-tabs',
      'grapesjs-touch',
      'grapesjs-parser-postcss',
      'grapesjs-tooltip',
      'grapesjs-tui-image-editor',
      'grapesjs-typed',
      'grapesjs-style-bg',
      'grapesjs-preset-webpage',
      'grapesjs-plugin-export'
    ],
    pluginsOpts: {
      'grapesjs-preset-webpage': {}
    }
  })
  editor.Panels.addButton('options', {
    id: 'save-page',
    className: 'pi pi-save',
    command: function () {
      save(editor)
    },
    attributes: {
      title: 'Publish Site',
      'data-tooltip-pos': 'bottom'
    }
  })
  editor.Panels.removeButton('options', 'export-template')
  editor.Panels.removeButton('options', 'gjs-open-import-webpage')
}

onMounted(() => {
  isShowEditor.value = false
})
</script>

<template>
  <div v-if="!isShowEditor">
    <InputText placeholder="Enter The Page Title Here" v-model="title" class="input" />
    <br />
    <Button rounded @click="toggleEditorHandller">Enter To Page Editor</Button>
  </div>
  <h3 v-else>Page Title: {{ title }}</h3>
  <div
    id="editor"
    :style="`${isShowEditor ? 'visibility: visible;' : 'visibility: hidden;'}height:900px`"
  />
</template>

<style scoped>
.input {
  width: 80%;
  margin-bottom: 20px;
}
</style>

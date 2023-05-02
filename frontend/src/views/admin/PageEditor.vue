<script setup>
import { onMounted, ref } from 'vue'

let editor = null
const save = () => {
  console.log(editor.getHtml())
  console.log(editor.getCss())
}

onMounted(() => {
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
})
</script>

<template>
  <div id="editor" />
</template>

<template>
  <v-tabs v-model="activeTab">
    <v-tab :to="{ name: 'analyze' }">Analyze</v-tab>
    <v-tab :to="{ name: 'categorize' }">Categorize</v-tab>
    <v-tab :to="{ name: 'upload' }">Upload</v-tab>
  </v-tabs>

  <RouterView />
</template>

<script setup lang="ts">
import { ref } from 'vue';

const activeTab = ref("analyze");
</script>
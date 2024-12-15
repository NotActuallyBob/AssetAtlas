<script setup lang="ts">
</script>

<template>
  <v-app>
    <v-app-bar>
      <v-app-bar-title>
        <router-link to="/">
          Main Page
        </router-link>
      </v-app-bar-title>
    </v-app-bar>

    <v-main>
      <v-container>
        <v-row justify="center">
          <v-col cols="12" md="8">
            <RouterView />
          </v-col>
        </v-row>
      </v-container> 
    </v-main>
  </v-app>
</template>

<style scoped>
</style>
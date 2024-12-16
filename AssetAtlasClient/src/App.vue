<template>
  <v-app>
    <v-layout>
      <v-app-bar color="primary">
        <v-btn>
          <router-link class="navigationlink" :to="{ name: 'expense' }">
              Expense
          </router-link>
        </v-btn>
      </v-app-bar>

      <v-main>
        <v-container id="main-container" fluid>
          <RouterView />
        </v-container>
      </v-main>
    </v-layout>
  </v-app>
</template>

<style scoped>
.navigationlink {
  color: white;
  text-decoration: none;
}

#main-container {
  padding: 0;
}
</style>
<template>
  <v-app>
    <v-app-bar color="primary">
      <v-btn>
        <router-link class="navigationlink" :to="{ name: 'expense' }">
            Expense
        </router-link>
      </v-btn>
    </v-app-bar>

    <v-main>
      <RouterView />
    </v-main>
  </v-app>
</template>

<style scoped>
.navigationlink {
  color: white;
  text-decoration: none;
}
</style>
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
.navigationlink {
  color: white;
  text-decoration: none;
}
</style>
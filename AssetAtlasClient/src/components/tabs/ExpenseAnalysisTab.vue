<template>
  <v-container fluid>
    <h1>Expense Chart</h1>
    
    <input type="text" v-model="start"></input>
    <input type="text" v-model="end"></input>
    <button @click="refreshExpenseData">Refresh</button>
    <v-row>
      <v-col>
        <PieChart
          :data="expenseStore.expenses"
          name="expensePieChart"
        ></PieChart>
      </v-col>
      <v-col>
        <v-data-table
          width="100px"
            :items="expenseStore.expenses"
        >
          <template v-slot:item.amount="{ value }">
            {{ value }}€
          </template>
        </v-data-table>
        <h2>Total {{ expenseStore.expenseTotal.toFixed(0) }}€</h2>
      </v-col>
    </v-row>
    <h1>Income Chart</h1>
    <input type="text" v-model="start"></input>
    <input type="text" v-model="end"></input>
    <button @click="refreshIncomeData">Refresh</button>
    <v-row>
      <v-col>
        <PieChart
          name="incomePieChart"
          :data="expenseStore.incomes"
        >
        </PieChart>
      </v-col>
      <v-col>
        <v-data-table
          width="100px"
            :items="expenseStore.incomes"
          >
            <template v-slot:item.amount="{ value }">
              {{ value }}€
            </template>
          </v-data-table>
          <h2>Total {{ expenseStore.incomeTotal.toFixed(0) }}€</h2>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue";
  import { useExpenseStore } from "../../stores/expenseStore";
  import PieChart from "./PieChart.vue";

  const expenseStore = useExpenseStore();

  onMounted(async () => {
    await refreshExpenseData();
    await refreshIncomeData();
  });

  let start = ref('2024-01-01')
  let end = ref('2024-12-31')

  async function refreshExpenseData() {
    await expenseStore.refreshExpenses(start.value, end.value);
  }

  async function refreshIncomeData() {
    await expenseStore.refreshIncomes(start.value, end.value);
  }
</script>


<style scoped>
#expenseChart {
  width: 100%;
  height: 600px;
}

#incomeChart {
  width: 100%;
  height: 600px;
}
</style>
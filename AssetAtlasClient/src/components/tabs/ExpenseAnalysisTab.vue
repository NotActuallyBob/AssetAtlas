<template>
  <v-container fluid>
    <h1>Expense Chart</h1>
    
    <input type="text" v-model="expenseStart"></input>
    <input type="text" v-model="expenseEnd"></input>
    <button @click="refreshExpenseData">Refresh</button>
    <v-row>
      <v-col>
        <div id="expenseChart"></div>
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
    <input type="text" v-model="incomeStart"></input>
    <input type="text" v-model="incomeEnd"></input>
    <button @click="refreshIncomeData">Refresh</button>
    <v-row>
      <v-col>
        <div id="incomeChart"></div>
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
import { onMounted, onUnmounted, ref } from "vue";
  import * as am5 from "@amcharts/amcharts5";
  import * as am5percent from "@amcharts/amcharts5/percent";
  import { useExpenseStore } from "../../stores/expenseStore";
  import { addSeries, createPie, createRoot } from "../../plugins/amcharts";

  const expenseStore = useExpenseStore();

  let rootExpense: am5.Root | null | undefined = undefined;
  let chartExpense: am5percent.PieChart | undefined = undefined;
  let seriesExpense: am5percent.PieSeries | undefined = undefined;

  let rootIncome: am5.Root | null | undefined = undefined;
  let chartIncome: am5percent.PieChart | undefined = undefined;
  let seriesIncome: am5percent.PieSeries | undefined = undefined;

  let expenseStart = ref('2024-01-01')
  let expenseEnd = ref('2024-12-31')

  async function refreshExpenseData() {
    await expenseStore.refreshExpenses(expenseStart.value, expenseEnd.value);

    if(!seriesExpense) {
        return;
    }

    seriesExpense.data.setAll(expenseStore.expenses);
  }

  onMounted(async () => {
    rootExpense = createRoot('expenseChart');
    chartExpense = createPie(rootExpense);
    seriesExpense = addSeries(rootExpense, chartExpense, "Series", "category", "amount", "default");

    rootIncome = createRoot('incomeChart');
    chartIncome = createPie(rootIncome);
    seriesIncome = addSeries(rootIncome, chartIncome, "Series", "source", "amount", "default")

    seriesExpense.labels.template.setAll({
      text: "{category}: {value}€", // Use valueField (amount) as the label
      textType: "circular", // Ensures the text wraps around the slice
      centerX: am5.p50,
      centerY: am5.p50,
      inside: true, // Place the labels inside the slices
    });

    seriesIncome.labels.template.setAll({
      text: "{category}: {value}€", // Use valueField (amount) as the label
      textType: "circular", // Ensures the text wraps around the slice
      centerX: am5.p50,
      centerY: am5.p50,
      inside: true, // Place the labels inside the slices
    });
    await refreshExpenseData();
    await refreshIncomeData();
  });

  onUnmounted(() => {
    if (rootExpense) {
      rootExpense.dispose();
      rootExpense = null;
    }
    if (rootIncome) {
      rootIncome.dispose();
      rootIncome = null;
    }
  });

  let incomeStart = ref('2024-01-01')
  let incomeEnd = ref('2024-12-31')


  async function refreshIncomeData() {
    await expenseStore.refreshIncomes(incomeStart.value, incomeEnd.value);

    if(!seriesIncome) {
        return;
    }

    seriesIncome.data.setAll(expenseStore.incomes);
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
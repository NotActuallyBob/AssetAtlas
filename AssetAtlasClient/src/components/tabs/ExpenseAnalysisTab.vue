<template>
  <v-container fluid>
    <h1>Spending Graph</h1>
    
    <input type="text" v-model="start"></input>
    <input type="text" v-model="end"></input>
    <button @click="refreshData">Refresh</button>
    <v-row>
      <v-col cols="12" lg="6">
        <div id="chartdiv"></div>
      </v-col>
      <v-col cols="12" lg="5">
        <v-data-table
          width="100px"
            :items="expenseStore.expenses"
          >
          </v-data-table>
          <h2>Total {{ expenseStore.expenseTotal.toFixed(0) }}€</h2>
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

  let root: am5.Root | null | undefined = undefined;
  let chart: am5percent.PieChart | undefined = undefined;
  let series: am5percent.PieSeries | undefined = undefined;

  let start = ref('2024-01-01')
  let end = ref('2024-12-31')

  async function refreshData() {
    await expenseStore.refresh(start.value, end.value);

    if(!series) {
        return;
    }

    series.data.setAll(expenseStore.expenses);
  }

  onMounted(async () => {
    root = createRoot('chartdiv');
    chart = createPie(root);
    series = addSeries(root, chart, "Series", "category", "amount", "default");

    series.labels.template.setAll({
      text: "{category}: {value}€", // Use valueField (amount) as the label
      textType: "circular", // Ensures the text wraps around the slice
      centerX: am5.p50,
      centerY: am5.p50,
      inside: true, // Place the labels inside the slices
    });
    await refreshData();
  });

  onUnmounted(() => {
    if (root) {
      root.dispose();
      root = null;
    }
  });

</script>


<style scoped>
#chartdiv {
  width: 100%;
  height: 600px;
}
</style>
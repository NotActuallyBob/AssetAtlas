<template>
    <input type="text" v-model="start"></input>
    <input type="text" v-model="end"></input>
    <button @click="refreshData">Refresh</button>
    <div id="chartdiv"></div>
    <v-btn>lol</v-btn>
</template>

<script setup lang="ts">
  import { onMounted, onUnmounted, ref } from "vue";
  import * as am5 from "@amcharts/amcharts5";
  import * as am5percent from "@amcharts/amcharts5/percent";
  import { useExpenseStore } from "../stores/expenseStore";
  import { addSeries, createPie, createRoot } from "../plugins/amcharts";

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
  width: 800px;
  height: 600px;
}
</style>

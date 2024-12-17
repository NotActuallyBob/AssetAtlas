<template>
  <div :id="name" style="width: 100%; height: 600px;"></div>
</template>

<script setup lang="ts">
const props = defineProps<{
  name: string,
  data: PieData[]
}>()

import { onMounted, onUnmounted, watch } from "vue";
import * as am5 from "@amcharts/amcharts5";
import * as am5percent from "@amcharts/amcharts5/percent";
import { addSeries, createPie, createRoot } from "../../plugins/amcharts";
import { PieData } from "../../models/PieData";

let root: am5.Root | null | undefined = undefined;
let pieChart: am5percent.PieChart | undefined = undefined;
let series: am5percent.PieSeries | undefined = undefined;

onMounted(() => {
  root = createRoot(props.name);
  pieChart = createPie(root);
  series = addSeries(root, pieChart, "Series", "category", "value", "default");

  series.labels.template.setAll({
    text: "{category}: {value}€", // Use valueField (amount) as the label
    textType: "circular", // Ensures the text wraps around the slice
    centerX: am5.p50,
    centerY: am5.p50,
    inside: true, // Place the labels inside the slices
  });
})

onUnmounted(() => {
    if (root) {
      root.dispose();
      root = null;
    }
})

watch(() => props.data, () => {
  refresh();  
})

function refresh() {
  if(!series) {
      return;
  }

  series.data.setAll(props.data);
}

</script>

<style scoped>
</style>
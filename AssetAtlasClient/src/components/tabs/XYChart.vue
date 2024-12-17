<template>
    <div :id="name" style="width: 100%; height: 600px;"></div>
</template>
  
<script setup lang="ts">
  const props = defineProps<{
    data: XYDateData[]
    name: string
  }>()
  
  import { onMounted, onUnmounted, watch } from "vue";
  import * as am5 from "@amcharts/amcharts5";
  import * as am5xy from "@amcharts/amcharts5/xy";
  import { addXYSeries, addXAxis, addYAxis, createXY, createRoot } from "../../plugins/amcharts";
  import { XYDateData } from "../../models/XYDateData";
  
  let root: am5.Root | null | undefined = undefined;
  let xyChart: am5xy.XYChart | undefined = undefined;
  let xAxis: am5xy.DateAxis<am5xy.AxisRenderer> | undefined = undefined;
  let yAxis: am5xy.ValueAxis<am5xy.AxisRenderer> | undefined = undefined;
  let series: am5xy.XYSeries | undefined = undefined;


  onMounted(() => {
    root = createRoot(props.name);
    xyChart = createXY(root);
    xAxis = addXAxis(root, xyChart);
    yAxis = addYAxis(root, xyChart);
    series = addXYSeries(root, xyChart, xAxis, yAxis);

    xyChart.set("cursor", am5xy.XYCursor.new(root, {
      xAxis: xAxis,
      yAxis: yAxis,
      behavior: "zoomXY",
      snapToSeries: [series],
    }));
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
  
  onUnmounted(() => {
      if (root) {
        root.dispose();
        root = null;
      }
  })
  
</script>
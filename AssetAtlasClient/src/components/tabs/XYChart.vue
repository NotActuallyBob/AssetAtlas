<template>
    <div :id="name" style="width: 100%; height: 800px;"></div>
</template>
  
<script setup lang="ts">
  const props = defineProps<{
    data: XYDateData[][]
    name: string
  }>()
  
  import { onMounted, onUnmounted, watch } from "vue";
  import * as am5 from "@amcharts/amcharts5";
  import * as am5xy from "@amcharts/amcharts5/xy";
  import { addXYSeries, addXAxis, addYAxis, createXY, createRoot, getColorSet } from "../../plugins/amcharts";
  import { XYDateData } from "../../models/XYDateData";
  import { expenseCategory } from "../../models/Expense";
  
  let root: am5.Root | null | undefined = undefined;
  let xyChart: am5xy.XYChart | undefined = undefined;
  let xAxis: am5xy.DateAxis<am5xy.AxisRenderer> | undefined = undefined;
  let yAxis: am5xy.ValueAxis<am5xy.AxisRenderer> | undefined = undefined;
  let series: am5xy.XYSeries[] = [];
  
  onMounted(() => {
    root = createRoot(props.name);
    xyChart = createXY(root);
    xAxis = addXAxis(root, xyChart);
    yAxis = addYAxis(root, xyChart);

    const colors = getColorSet("default");

    Object.keys(expenseCategory).filter((key) => isNaN(Number(key))).forEach((key, index) => {
      if(!root || !xyChart || !xAxis|| !yAxis) {
        return;
      }

      const serie = addXYSeries(key, root, xyChart, xAxis, yAxis);
      serie.set("fill", colors[index]);
      serie.set("stroke", colors[index]); 

      series.push(serie);
    });
    

    xyChart.set("cursor", am5xy.XYCursor.new(root, {
      xAxis: xAxis,
      yAxis: yAxis,
      behavior: "zoomXY",
      snapToSeries: series,
    }));

    let legend = xyChart.children.push(am5.Legend.new(root, {
      centerX: am5.p50,
      x: am5.p50
    }));
    legend.data.setAll(xyChart.series.values);
  })

  watch(() => props.data, () => {
    refresh();
  })

  function refresh() {
    if(!series) {
      return;
    }

    Object.keys(expenseCategory).filter((key) => !isNaN(Number(key))).forEach((key, index) => {
      const i = key as unknown as number;
      series[i].data.setAll(props.data[i]);
    })
    
  }
  
  onUnmounted(() => {
      if (root) {
        root.dispose();
        root = null;
      }
  })
  
</script>
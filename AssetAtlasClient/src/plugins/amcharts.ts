import * as am5 from "@amcharts/amcharts5";
import * as am5percent from "@amcharts/amcharts5/percent";
import * as am5xy from "@amcharts/amcharts5/xy";
import am5themes_Animated from "@amcharts/amcharts5/themes/Animated";

export type ColorSetName = "default";

const colorSets: Record<ColorSetName, am5.Color[]> = {
    default: [
        am5.color("#5A5A5A"),
        am5.color("#4CAF50"),
        am5.color("#2196F3"),
        am5.color("#FF5722"),
        am5.color("#9C27B0"),
        am5.color("#E91E63"),
        am5.color("#FFC107"),
        am5.color("#795548"),
        am5.color("#009688"),
        am5.color("#9E9E9E"),
    ],
}

export function getColorSet(name: ColorSetName): am5.Color[] {
    return colorSets[name] || colorSets["default"];
}

export function createRoot(containerId: string): am5.Root {
    const root = am5.Root.new(containerId);

    root.setThemes([
        am5themes_Animated.new(root)
    ]);

    root.dateFormatter.setAll({
        dateFormat: "dd-MM-yyyy",
        dateFields: ["date"]
      });

    return root;
}

export function createPie(root: am5.Root): am5percent.PieChart {
    const chart = root.container.children.push(
        am5percent.PieChart.new(root, {})
    );

    return chart;
}

export function addSeries(root: am5.Root, chart: am5percent.PieChart, name: string, categoryField: string, valueField: string, colorSet: ColorSetName): am5percent.PieSeries {
    const series = chart.series.push(
        am5percent.PieSeries.new(root, {
          name,
          categoryField,
          valueField,
        },
      )
    );

    series.set(
        "colors",
        am5.ColorSet.new(root, {
        colors: getColorSet(colorSet),
        passOptions: {}, // Default behavior for color assignments
        })
    );

    return series
}


export function createXY(root: am5.Root): am5xy.XYChart {
    let chart = root.container.children.push(am5xy.XYChart.new(root, {
        layout: root.verticalLayout
    }));

    return chart;
}

export function addXAxis(root: am5.Root, chart: am5xy. XYChart): am5xy.DateAxis<am5xy.AxisRenderer> {
    let xAxis = chart.xAxes.push(am5xy.DateAxis.new(root, {
        baseInterval: { timeUnit: "day", count: 1 },
        autoZoom: false,
        renderer: am5xy.AxisRendererX.new(root, { 
            minGridDistance: 50,
            minorGridEnabled:true
        }),
        gridIntervals: [
            { timeUnit: "week", count: 1 }
          ],
        tooltip: am5.Tooltip.new(root, {})
    }));

    return xAxis
}

export function addYAxis(root: am5.Root, chart: am5xy. XYChart): am5xy.ValueAxis<am5xy.AxisRenderer> {
    let yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
        numberFormat: "#€",
        renderer: am5xy.AxisRendererY.new(root, {}),
        autoZoom: false,
        tooltip: am5.Tooltip.new(root, {})
      }));
      

    return yAxis;
}

export function addXYSeries(name: string, root: am5.Root, chart: am5xy.XYChart, xAxis: am5xy.DateAxis<am5xy.AxisRenderer>, yAxis: am5xy.ValueAxis<am5xy.AxisRenderer>): am5xy.XYSeries {
    let series = chart.series.push(am5xy.ColumnSeries.new(root, {
        name,
        stacked: true,
        xAxis: xAxis,
        yAxis: yAxis,
        valueYField: "value",
        valueXField: "date",
        tooltip: am5.Tooltip.new(root, {
          labelText: "{valueX.formatDate()}, y: {valueY}€"
        })
      }));

      
    return series;
}
import * as am5 from "@amcharts/amcharts5";
import * as am5percent from "@amcharts/amcharts5/percent";
import * as am5xy from "@amcharts/amcharts5/xy";
import am5themes_Animated from "@amcharts/amcharts5/themes/Animated";

export type ColorSetName = "default";

const colorSets: Record<ColorSetName, am5.Color[]> = {
    default: [
        am5.color("#000"),
        am5.color("#e163cf"),
        am5.color("#2a53c7"),
        am5.color("#89b8ff"),
        am5.color("#20886a"),
        am5.color("#26e674"),
        am5.color("#6cf29f"),
        am5.color("#ffc114"),
        am5.color("#ab7a03"),
        am5.color("#e39e52"),
    ],
}

function getColorSet(name: ColorSetName): am5.Color[] {
    return colorSets[name] || colorSets["default"];
}

export function createRoot(containerId: string): am5.Root {
    const root = am5.Root.new(containerId);

    root.setThemes([
        am5themes_Animated.new(root)
    ]);

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
    let chart: am5xy.XYChart = am5xy.XYChart.new(root, {
        panX: false,
        panY: false
    });

    return chart;
}

export function addXAxis(root: am5.Root, chart: am5xy. XYChart): am5xy.DateAxis<am5xy.AxisRenderer> {
    let xAxis =  chart.xAxes.push(am5xy.DateAxis.new(root, {
        baseInterval: {
            timeUnit: 'day',
            count: 1
        },
        renderer: am5xy.AxisRendererX.new(root, {
            minorGridEnabled: true,
            minorLabelsEnabled: true,
        }),
        tooltip: am5.Tooltip.new(root, {})
    }));

    xAxis.set("minorDateFormats", {
        "day":"dd",
        "month":"MM"
    });

    return xAxis
}

export function addYAxis(root: am5.Root, chart: am5xy. XYChart): am5xy.ValueAxis<am5xy.AxisRenderer> {
    let yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
        renderer: am5xy.AxisRendererY.new(root, {})
    }));

    return yAxis;
}

export function addXYSeries(root: am5.Root, chart: am5xy.XYChart, xAxis: am5xy.DateAxis<am5xy.AxisRenderer>, yAxis: am5xy.ValueAxis<am5xy.AxisRenderer>): am5xy.XYSeries {
    let series = chart.series.push(am5xy.ColumnSeries.new(root, {
        name: "Series",
        xAxis,
        yAxis,
        valueYField: "value",
        valueXField: "date",
        tooltip: am5.Tooltip.new(root, {
          labelText: "{valueY}"
        })
      }));
    return series;
}
import * as am5 from "@amcharts/amcharts5";
import * as am5percent from "@amcharts/amcharts5/percent";
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
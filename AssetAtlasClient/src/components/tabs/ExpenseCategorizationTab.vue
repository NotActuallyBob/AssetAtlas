<template>
    <v-select 
        v-model="selectedCategory"
        :items="categoryOptions"
        item-title="text"
        item-value="value"
        :loading="loading"
    ></v-select>
    <v-btn 
        @click="save">
        Save
    </v-btn>
    <v-data-table 
        v-model="selected"
        item-value="id"
        show-select
        :items="expenseStore.unCategorized"
        >
    </v-data-table>
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue";

  import { useExpenseStore } from "../../stores/expenseStore";
  import { expenseCategory } from "../../models/Expense";
  import restHelper from "../../helpers/restHelper";

  const expenseStore = useExpenseStore();

  const loading = ref<boolean>(false);
  const selected = ref([]);
  const selectedCategory = ref<number>();

  const categoryOptions = Object.keys(expenseCategory)
  .filter((key) => isNaN(Number(key))) // Exclude reverse-mapped numeric keys
  .map((key) => ({
    text: key, // Use the key (name of the category) as text
    value: expenseCategory[key as keyof typeof expenseCategory], // Use the numeric value
  }));

  onMounted(async () => {
    loading.value = true;
    await refresh();
    loading.value = false;
  });

  async function save() {
    loading.value = true;
    await restHelper.post("/api/ExpensesCategorize", {
        ids: selected.value,
        category: selectedCategory.value
    })

    selected.value = [];

    await refresh();
    loading.value = false;
  }

  async function refresh() {
    await expenseStore.refreshUncategorized();
  }

</script>

<style scoped>
#chartdiv {
  width: 800px;
  height: 600px;
}
</style>

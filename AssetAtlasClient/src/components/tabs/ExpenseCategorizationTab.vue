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
        :headers="headers"
        show-select
        :items="expenseStore.unCategorized"
    >
        <template v-slot:item.spendTime="{ value }">
            {{ value.getDate() }}.{{ value.getMonth()+1 }}.{{ value.getFullYear() }}
        </template>

        <template v-slot:item.amount="{ value }">
            {{ value / 100 }}€
        </template>

        <template v-slot:item.expenseCategory="{ value }">
            {{ expenseCategory[value] }}
        </template>
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
  
  const headers = [
    { title: 'Recipient', key: 'recipient' },
    { title: 'SpendTime', key: 'spendTime' },
    { title: 'Amount', key: 'amount' },
    { title: 'Category', key: 'expenseCategory' },
  ];

  const categoryOptions = Object.keys(expenseCategory)
  .filter((key) => isNaN(Number(key)))
  .map((key) => ({
    text: key,
    value: expenseCategory[key as keyof typeof expenseCategory],
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
</style>

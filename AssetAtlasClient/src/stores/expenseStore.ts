import { defineStore } from 'pinia'
import { ref } from 'vue'
import restHelper from '../helpers/restHelper'

export const useExpenseStore = defineStore('expense', () => {
  // State
  const expenses = ref([])


  enum Category {
    Uncategorized = 0,
    Groceries = 1,
    Housing = 2,
    Restaurants = 3,
    Travel = 4,
    Entertainment = 5,
    Hobbies = 6,
    Clothing = 7,
    Health = 8,
    Other = 9,
  }

  function getCategoryName(value: number): string {
    return Category[value] || "Unknown";
  }

  // Actions
  const refresh = async (start: string, end: string) => {
    const response = await restHelper.get("/api/Expense", {
        params: {
            start,
            end
        }
    });

    expenses.value = response.data.map((obj: { item1: number; item2: number }) => ({
        category: getCategoryName(obj.item1),
        amount: obj.item2 / 100,
    }));
  }

  return {
    expenses,
    refresh,
  }
})
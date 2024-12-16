import { defineStore } from 'pinia'
import { ref } from 'vue'
import restHelper from '../helpers/restHelper'
import { Expense, expenseCategory } from '../models/Expense'
import { ExpenseDto } from '../dtos/ExpenseDto'

export const useExpenseStore = defineStore('expense', () => {
  // State
  const expenses = ref([])
  const expenseTotal = ref<number>(0);

  const unCategorized = ref<Expense[]>([])

  function getCategoryName(value: number): string {
    return expenseCategory[value] || "Unknown";
  }

  // Actions
  const refresh = async (start: string, end: string) => {
    expenseTotal.value = 0;
    const response = await restHelper.get("/api/Expense", {
        params: {
            start,
            end
        }
    });

    expenses.value = response.data.map((obj: { item1: number; item2: number }) => {
      const amount = obj.item2 / 100
      expenseTotal.value += amount;
      return {
        category: getCategoryName(obj.item1),
        amount
      };
    });
  }

  const refreshUncategorized = async () => {
    const dataDto = (await restHelper.get("/api/ExpensesUncategorized")).data as ExpenseDto[];

    let dataExpenses: Expense[] = dataDto.map((dto: ExpenseDto) => {
      let expense: Expense = {
        id: dto.id,
        amount: dto.amount,
        spendTime: new Date(dto.spendTime),
        recipient: dto.recipient,
        expenseCategory: dto.expenseCategory
      };

      return expense;
    })

    unCategorized.value = dataExpenses;
  }

  return {
    expenses,
    refresh,
    expenseTotal,
    unCategorized,
    refreshUncategorized
  }
})
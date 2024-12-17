import { defineStore } from 'pinia'
import { ref } from 'vue'
import restHelper from '../helpers/restHelper'
import { Expense, expenseCategory } from '../models/Expense'
import { ExpenseDto } from '../dtos/ExpenseDto'
import { PieData } from '../models/PieData'
import { XYDateData } from '../models/XYDateData'

export const useExpenseStore = defineStore('expense', () => {
  const expenses = ref<PieData[]>([])
  const expenseTotal = ref<number>(0);

  const incomes = ref<PieData[]>([])
  const incomeTotal = ref<number>(0);

  var xyData = ref<XYDateData[]>([]);

  const refreshXY = async (start: string, end: string) => {
    const response = await restHelper.get("/api/XY", {
        params: {
            start,
            end
        }
    });

    xyData.value = response.data.map((obj: { item1: string; item2: any[] }) => {

      const amount = obj.item2 / 100
      return {
        date: new Date(obj.item1).getTime(),
        value: amount
      };
    });
    console.log(xyData);
  }

  const unCategorized = ref<Expense[]>([])

  function getCategoryName(value: number): string {
    return expenseCategory[value] || "Unknown";
  }

  const refreshExpenses = async (start: string, end: string) => {
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
        value: amount
      };
    });
  }

  const refreshIncomes = async (start: string, end: string) => {
    incomeTotal.value = 0;
    const response = await restHelper.get("/api/Income", {
        params: {
            start,
            end
        }
    });

    incomes.value = response.data.map((obj: { item1: number; item2: number }) => {
      const amount = obj.item2 / 100
      incomeTotal.value += amount;
      return {
        category: obj.item1,
        value: amount
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
    refreshExpenses,
    expenseTotal,
    unCategorized,
    refreshUncategorized,
    incomes,
    incomeTotal,
    refreshIncomes,
    xyData,
    refreshXY
  }
})
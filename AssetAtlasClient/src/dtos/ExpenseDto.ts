import { expenseCategory } from "../models/Expense"

export type ExpenseDto = {
    id: number,
    amount: number,
    spendTime: string,
    recipient: string,
    expenseCategory: expenseCategory
}
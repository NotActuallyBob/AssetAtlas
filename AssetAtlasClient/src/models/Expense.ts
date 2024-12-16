export enum expenseCategory {
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

export type Expense = {
    id: number,
    amount: number,
    recipient: string,
    expenseCategory: expenseCategory,
    spendTime: Date,
}
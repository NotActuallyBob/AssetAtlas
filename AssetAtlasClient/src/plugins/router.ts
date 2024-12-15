import { createRouter } from 'vue-router'
import { createWebHistory } from 'vue-router'

import MainView from '../views/MainView.vue'

import ExpenseAnalysisTab from '../components/tabs/ExpenseAnalysisTab.vue'
import ExpenseCategorizationTab from '../components/tabs/ExpenseCategorizationTab.vue'
import ExpenseUploadTab from '../components/tabs/ExpenseUploadTab.vue'

const routes = [
    { 
        path: '/', 
        name: 'Main', 
        component: MainView,
        children: [
            {
                path: 'analyze',
                name: 'analyze',
                component: ExpenseAnalysisTab
            },
            {
                path: 'categorize',
                name: 'categorize',
                component: ExpenseCategorizationTab
            },
            {
                path: 'upload',
                name: 'upload',
                component: ExpenseUploadTab
            }
        ]
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
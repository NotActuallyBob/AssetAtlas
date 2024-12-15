import { createRouter } from 'vue-router'
import { createWebHistory } from 'vue-router'
import MainView from '../views/MainView.vue'
import CategorizationView from '../views/CategorizationView.vue'
import UploadView from '../views/UploadView.vue'

const routes = [
    { path: '/', name: 'Main', component: MainView },
    { path: '/categorization', name: 'Categorization', component: CategorizationView },
    { path: '/upload', name: 'Upload', component: UploadView },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
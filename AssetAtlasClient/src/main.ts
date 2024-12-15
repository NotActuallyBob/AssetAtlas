import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './plugins/router'
import { createPinia } from 'pinia'
import vuetify from './plugins/vuetify'

const pinia = createPinia() 

createApp(App).use(pinia).use(router).use(vuetify).mount('#app')
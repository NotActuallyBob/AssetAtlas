import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    port: 8080,
    proxy: {
      "/api": {
					target: "http://localhost:5000",
					changeOrigin: true,
					secure: false,
					rewrite: (p) => p.replace(/^\/api/, "/api"),
      },
    },
    cors: false
  },
});
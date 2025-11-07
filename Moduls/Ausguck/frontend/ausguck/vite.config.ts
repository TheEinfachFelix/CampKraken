import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
    build: {
      outDir: 'build'  // falls du wirklich build/ willst
    },
    server: {
    host: '0.0.0.0',
    port: 4173,
    //allowedHosts: ['einfachfelix.ydns.eu']  // Erlaubt den Zugriff über deine Domain

  }
  
})

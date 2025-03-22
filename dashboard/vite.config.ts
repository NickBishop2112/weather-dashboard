import tailwindcss from '@tailwindcss/vite';
import react from '@vitejs/plugin-react'; // Or any other plugins you're using
import { defineConfig } from 'vite';

// Vite configuration
export default defineConfig({
    plugins: [react(), tailwindcss()],
    test: {
        globals: true,
        environment: "jsdom",
        setupFiles: ['./src/setupTests.ts'],        
      },
    server: {
        port: 3000, // Customize the development server port
    },
    resolve: {
        alias: {
            '@': '/src', // Example alias for cleaner imports
        },
    },
});

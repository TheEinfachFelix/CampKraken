# Use the latest LTS version of Node.js
FROM node:20-alpine
 
# Set the working directory inside the container
WORKDIR /app
 
# Copy package.json and package-lock.json
COPY package*.json ./
 
# Install dependencies
RUN npm install
 
# Copy the rest of your application files
COPY . .
 
# Expose the port your app runs on
EXPOSE 5173
 
# Define the command to run your app
CMD ["npm", "start"]

## Final Production Dockerfile Example
#### Stage 1: Build the React app
###FROM node:18-alpine AS build
###WORKDIR /app
###
#### Leverage caching by installing dependencies first
###COPY package.json package-lock.json ./
###RUN npm install --frozen-lockfile
###
#### Copy the rest of the application code and build for production
###COPY . ./
###RUN npm run build
###
#### Stage 2: Development environment
###FROM node:18-alpine AS development
###WORKDIR /app
###
#### Install dependencies again for development
###COPY package.json package-lock.json ./
###RUN npm install --frozen-lockfile
###
#### Copy the full source code
###COPY . ./
###
#### Expose port for the development server
###EXPOSE 3000
###CMD ["npm", "start"]
###
#### Stage 3: Production environment
###FROM nginx:alpine AS production
###
#### Copy the production build artifacts from the build stage
###COPY --from=build /app/build /usr/share/nginx/html
###
#### Expose the default NGINX port
###EXPOSE 80
###CMD ["nginx", "-g", "daemon off;"]
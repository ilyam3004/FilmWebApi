FROM node:latest AS build

WORKDIR /app

COPY . .
RUN npm install
RUN npm run build --prod

FROM nginx:alpine

COPY --from=build /app/dist/web-client /usr/share/nginx/html
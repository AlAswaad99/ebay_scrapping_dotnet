FROM eclipse-temurin:17-jdk-alpine AS builder

WORKDIR /app

# Copy the source code
COPY . .

# Build the application
RUN ./mvnw package -DskipTests

FROM eclipse-temurin:17-jre-alpine

# # Install wget
# RUN apt-get update && \
#     apt-get install -y netcat-openbsd wget

WORKDIR /app

EXPOSE 8081

# Add App
COPY --from=builder /app/target/*.jar /app/service.jar

# Add wait-for-it for docker-compose
# COPY wait-for-it.sh wait-for-it.sh
# RUN chmod +x wait-for-it.sh
# RUN sed -i 's/\r$//' wait-for-it.sh

# # Wait for 30 seconds before running the entrypoint command
# RUN sleep 30


ENTRYPOINT ["java","-jar","/app/service.jar"]
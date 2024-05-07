FROM eclipse-temurin:17-jdk-alpine AS builder

WORKDIR /app

# Copy the source code
COPY . .

# Build the application
RUN ./mvnw package -DskipTests

FROM eclipse-temurin:17-jre-alpine

WORKDIR /app

EXPOSE 8081

# Add App
COPY --from=builder /app/target/*.jar /app/service.jar

ENTRYPOINT ["java","-jar","/app/service.jar"]
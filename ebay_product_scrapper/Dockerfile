FROM eclipse-temurin:17-jre-alpine

WORKDIR /app

EXPOSE 8081

# Add App
ARG JAR_FILE=target/*.jar

COPY ${JAR_FILE} /app/service.jar

ENTRYPOINT ["java","-jar","/app/service.jar"]
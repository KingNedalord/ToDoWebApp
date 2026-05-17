# Этап 1: Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# Копируем файл проекта и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем все остальные файлы и собираем релизную версию
COPY . ./
RUN dotnet publish -c Release -o out

# Этап 2: Запуск приложения
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

# Открываем порт 5242 внутри контейнера
EXPOSE 5242
ENV ASPNETCORE_URLS=http://+:5242

# Точка входа для запуска Web App
ENTRYPOINT ["dotnet", "ToDo.dll"]
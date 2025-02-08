# Online Chat (Test Project)

## Описание
Тестовое приложение - онлайн-чат, реализовано на **ASP.NET Core**. 
Приложение включает сервер и три клиента:
- **[Клиент 1](https://github.com/your-username/client1)** отправляет сообщение на сервер.
- **Сервер** сохраняет сообщение в **PostgreSQL** и рассылает его клиенту 2 по **WebSockets**.
- **[Клиент 2](https://github.com/your-username/client2)** получает сообщение через **WebSockets**.
- **[Клиент 3](https://github.com/your-username/client3)** по нажатию кнопки получает все сообщения за последние **10 минут** через API.

Приложение **не использует ORM**, а работает напрямую с базой данных. 
Логирование реализовано с помощью **ILogger**, а для маппинга используется **AutoMapper**.

Оформлен **Docker-compose** файл для упрощенного развертывания.

## 🚀 Стек технологий
- **ASP.NET Core 8.0**
- **PostgreSQL 17**
- **WebSockets**
- **Docker**
- **ILogger** (Логирование)
- **AutoMapper** (Маппинг моделей)

## 🛠 API эндпоинты
- **[Swagger UI](http://localhost:7027/swagger)**
- **[SendMessage](http://localhost:7027/SendMessage)**
- **[ReceiveMessage](http://localhost:7027/ReceiveMessage)**
- **[GetLastMessages](http://localhost:7027/GetLastMessages)**

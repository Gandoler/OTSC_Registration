# 🧠 RegistrationApi — Сервис регистрации пользователей

## 📌 Назначение

RegistrationApi — это REST API микросервис для регистрации новых пользователей. Работает в связке с `DBProxy`, через который осуществляется проверка и регистрация в базе данных.

---

## 🚀 Возможности

- Регистрация новых пользователей.
- Проверка существования пользователей через внешний прокси-сервис.
- Валидация входных данных (логин, пароль, email).
- Логирование запросов и ошибок.
- Swagger-документация и примеры запросов.

---

## 🛠️ Технологии

- ASP.NET Core
- Serilog
- FluentValidation
- Swagger (Swashbuckle)
- HttpClientFactory

---

## 🔗 Зависимости

- **DBProxy** — внешний микросервис, к которому обращается сервис регистрации:
  - `RegistrProxy` — регистрация пользователя.
  - `CheckExistProxy` — проверка существования.

---

## 🧪 Swagger

Swagger UI будет доступен по адресу:
```
https://localhost:PORT/swagger
```

Содержит описание и примеры запроса на регистрацию.

---

## ⚙️ Переменные окружения

| Переменная | Назначение |
|-----------|------------|
| `DbProxy` | URL к сервису DBProxy (например, http://localhost:5001) |

---

## 🐳 Docker

### Сборка вручную

```bash
DOCKER_BUILDKIT=1 docker build -t registrationapi_image -f Dockerfile .
docker run -d -p 8082:8080 --name registrationapi_container -e DbProxy=http://dbproxy_url registrationapi_image
```

### С помощью Docker Compose

```bash
docker-compose up --build
```

---

## 🧩 Эндпоинты

### POST `/api/RegistrationUser`

Регистрирует нового пользователя.

#### Пример запроса:
```json
{
  "login": "exampleUser",
  "password": "Pa55word123",
  "email": "example@example.com"
}
```

#### Возможные ответы:
- `200 OK`: Пользователь успешно зарегистрирован.
- `400 Bad Request`: Ошибка валидации.
- `409 Conflict`: Пользователь уже существует.
- `500 Internal Server Error`: Внутренняя ошибка.

---

## 📂 Структура проекта (основные папки)

- `Controllers` — Контроллеры Web API.
- `Domain` — DTO и интерфейсы.
- `Infrastructure` — Реализация логики прокси.
- `Validators` — FluentValidation-правила.
- `Services` — Бизнес-логика регистрации.

---

## 📞 Поддержка

Для вопросов и предложений — [Issues на GitHub](https://github.com/Gandoler/OTSC_DBProxy/issues)

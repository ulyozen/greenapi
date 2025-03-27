# GreenApiQA Automation

Автоматические тесты для проверки методов API.

---

## Как запустить автотесты

### Предварительные требования
- Установлен .NET 8 SDK: [скачать](https://dotnet.microsoft.com/download)
- Аккаунт в [Green-API](https://console.green-api.com/auth) с активным `idInstance` и `apiTokenInstance`
- Клонирован репозиторий с GitHub

---

### Настройка локальной среды

Заполните файл `appsettings.json` в проекте `GreenApiQA.Automation`

```json
{
  "GreenApi": {
    "ChatId": "7700XXXXXXX@c.us",
    "IdInstance": "1200XXXXXX",
    "ApiTokenInstance": "XXXXXXXXXXXXXXX",
    "Url": "https://api.domain.com"
  }
}
```

---

### Запуск автотестов

```bash
cd GreenApiQA
dotnet test
```

Дождитесь завершения всех тестов и убедитесь, что они прошли успешно.

---
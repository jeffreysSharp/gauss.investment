# 💼 Gauss Investment Platform

Complete system for the investment market, focusing on **onboarding**, **suitability**, **credit analysis**, **operational limits**, and **regulatory compliance**. Built with a layered architecture following the principles of **DDD**, **Clean Code**, and **SOLID**.

---

## 📚 About the Project

### 🧠 Name Origin

The name **Gauss Investment** was inspired by **Johann Carl Friedrich Gauss**, known as the *Prince of Mathematics*, for his fundamental contributions to **number theory**, **statistics**, and **mathematical modeling** — all of which relate directly to the investment world and risk analysis.

This project aims to deliver a **robust and scalable platform** for the investment market, covering everything from **investor registration** to **risk and operational limit management**, with asynchronous messaging, JWT authentication, and full CI/CD integration using Azure DevOps.

---

## 🚀 Technologies Used

- **.NET Core 8 / C# 12**
- **ASP.NET Core Web API**
- **React**
- **Entity Framework Core / Dapper / UnitOfWork**
- **RabbitMQ**
- **FluentValidation / AutoMapper / MediatR**
- **JWT Authentication**
- **FluentMigrator / FluentMigrator.RunnerXUnit**
- **Docker / Azure DevOps / GitHub**
- **SonarCloud** for static code analysis
- **xUnit, Moq, Bogus, FluentAssertions**
- **OpenAI** (Future AI integration)
- Layered architecture based on **DDD**
- Clear separation of responsibilities between `Domain`, `Application`, `Infrastructure`, `WebAPI`, and `Tests`
- Test organization by business context
- **Over 90% test coverage**, including unit and integration tests

---

## 🔀 Multi-Database Support

- Supports **SQL Server**, **MySQL**, **PostgreSQL**, and **MongoDB**
- Dynamic configuration via `appsettings.json`, allowing easy switching between databases with a simple change

---

## 🌍 Multi-language Support

- System already implemented in **Portuguese**, **English**, **French**, and **Spanish**

---

## 📋 Agile Management

- Entire development process managed with **Scrum** using **Azure DevOps Boards**
- **Continuous Integration** and **code review** via **SonarCloud**

---

## ✅ Features Implemented



---

## 🛠️ Features in Development


---

## 📦 Project Structure

```bash
src/
├── Backend/
│   ├── Gauss.Investment.Domain
│   ├── Gauss.Investment.Application
│   ├── Gauss.Investment.Infrastructure
│   └── Gauss.Investment.WebAPI
├── Shared/
│   ├── Gauss.Investment.Communication
│   └── Gauss.Investment.Exceptions
├── Mobile/ (reserved for app)
tests/
├── CommonTestUtilities
├── UseCase.Test
├── Validators.Test
└── WebApi.Test
```

---

## 📈 Roadmap

- 🔜 Integration with real brokers via **Open Finance**
- 🔜 Real-time monitoring with **SignalR**
- 🔜 Automated deployment in cloud environments (**Azure / Kubernetes**)
- 🔜 Real-time risk dashboard with **Blazor**

---

## 📄 License

This project is licensed under the **MIT** license. See the [LICENSE](LICENSE) file for more details.

---

## 📸 Screenshots

_(Add screenshots of Swagger, project structure, or the running app here)_

---

## 🙋‍♂️ Developer

**Jeferson Almeida**  
🔗 [GitHub](https://github.com/jeffreysSharp)  
🔗 [LinkedIn](https://www.linkedin.com/in/jeffreys-sharp/)  
📞 +55 11 99754-1210


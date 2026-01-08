# Family Finance API

🚀 **Family Finance API** é uma API RESTful desenvolvida em **.NET 10**, seguindo boas práticas de arquitetura, para gerenciamento financeiro familiar. Permite controle de famílias, membros, categorias, orçamentos e transações, com banco de dados **PostgreSQL**.

---

## Funcionalidades

- CRUD de famílias e membros
- CRUD de categorias e orçamentos
- Registro de transações financeiras
- Estrutura preparada para múltiplos ambientes (Local/Docker)

---

## Tecnologias

- .NET 10 (C#)
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose

---

## Estrutura do Projeto

```text
FamilyFinance.Api/
├─ Controllers/              # Endpoints da API
├─ Data/                     # DbContext e configurações do banco
├─ Migrations/               # Histórico de migrations do EF Core
├─ Models/                   # Modelos de domínio
├─ Services/                 # Lógica de negócio
├─ appsettings.json          # Configuração genérica
├─ appsettings.Development.json # Configuração local
├─ docker-compose.yml        # Orquestração de containers
└─ README.md
```
---

## 📡 Endpoints principais

- `/api/families` → CRUD de famílias  
- `/api/members` → CRUD de membros  
- `/api/categories` → CRUD de categorias  
- `/api/budgets` → CRUD de orçamentos  
- `/api/transactions` → Registro e consulta de transações

---

## ▶️ Execução rápida

A API sobe via Docker Compose.

```bash
docker-compose up --build
```

---

## 🗺️ Roadmap

- [ ] Autenticação e autorização (JWT)
- [ ] Testes unitários e de integração
- [ ] Versionamento da API
- [ ] Pipeline CI/CD

# Family Finance API

🚀 **Family Finance API** é uma API RESTful desenvolvida em **.NET 10**, voltada para gerenciamento financeiro familiar. Permite controle de famílias, membros, categorias, orçamentos e transações, com banco de dados **PostgreSQL**.

---

## Funcionalidades

- CRUD de famílias e membros
- CRUD de categorias e orçamentos
- Registro de transações financeiras
- Estrutura preparada para múltiplos ambientes (Local/Docker)

---

## Tecnologias

- **.NET 10 (C#)**
- **Entity Framework Core**
- **PostgreSQL**
- **Docker & Docker Compose**

---

## Estrutura do Projeto

FamilyFinance.Api/
│
├─ Controllers/ # Endpoints da API
├─ Data/ # DbContext e configurações do banco
├─ Migrations/ # Migrations do EF Core
├─ Models/ # Modelos de domínio
├─ Services/ # Lógica de negócio
├─ appsettings.json # Configuração genérica (subida no GitHub)
├─ appsettings.Development.json (não subir) # Config local com senha
├─ docker-compose.yml # Orquestração de containers
└─ README.md

markdown

---

## Endpoints

- `/api/families` → CRUD de famílias
- `/api/categories` → CRUD de categorias
- `/api/budgets` → CRUD de orçamentos
- `/api/transactions` → Registro e consulta de transações
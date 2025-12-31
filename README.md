### **Entidades principais**

1. **User** – quem registra os dados (opcional se for só 1 família)

    - Id (GUID)

    - Name

    - Email

    - PasswordHash

2. **Family** – grupo de pessoas para controle financeiro

    - Id (GUID)

    - Name

3. **Member** – pessoas dentro da família

    - Id (GUID)

    - Name

    - FamilyId (FK)

4. **Transaction** – registro de ganho ou gasto

    - Id (GUID)

    - MemberId (FK)

    - FamilyId (FK)

    - Type (Enum: Income | Expense)

    - Category (Enum ou string: Alimentação, Transporte, etc.)

    - Amount (decimal)

    - Date (DateTime)

    - Description (string opcional)

5. **Budget** – limite de gastos por categoria/mês

    - Id (GUID)

    - FamilyId (FK)

    - Category

    - LimitAmount (decimal)

    - Month (int)

    - Year (int)


---

### **Endpoints básicos**

#### **Family**

- `GET /families` → listar famílias

- `POST /families` → criar família

- `GET /families/{id}` → detalhes da família


#### **Member**

- `GET /families/{id}/members` → listar membros

- `POST /families/{id}/members` → criar membro


#### **Transaction**

- `GET /families/{id}/transactions` → listar todos os gastos/ganhos

- `POST /families/{id}/transactions` → registrar transação

- `GET /families/{id}/transactions/{month}/{year}` → extrato mensal

- `GET /families/{id}/transactions/summary` → resumo total: ganhos, gastos, saldo


#### **Budget**

- `GET /families/{id}/budgets` → listar orçamentos

- `POST /families/{id}/budgets` → criar/atualizar orçamento

- `GET /families/{id}/budgets/alert` → ver categorias que estouraram o limite


---

### **Observações rápidas**

- Transactions podem ser filtradas por membro, categoria e data.

- Resumo financeiro = soma de incomes – soma de expenses.

- Para alertas de orçamento, compare o total de expenses da categoria com o limite definido.

- Use autenticação JWT se for multiusuário.
# Ambev Developer Evaluation API

API construída para o desfio técnico da Ambev, seguindo os princípios de Clean Architecture, Domain-Driven Design (DDD), e com foco em boas práticas de engenharia de software.

---

## Tecnologias e Padrões Utilizados

- **.NET 8**
- **PostgreSQL** (via Docker)
- **Clean Architecture**
- **DDD (Domain-Driven Design)**
- **CQRS com MediatR**
- **Entity Framework Core**
- **FluentValidation**
- **AutoMapper**
- **xUnit + NSubstitute + Bogus**
- **Swagger**
- **Publicação de eventos (simulada)**

---

## Estrutura de Projetos

```text
├── WebApi                           # Camada de entrada (HTTP)
├── Application                      # Casos de uso e comandos/queries
├── Domain                           # Entidades e eventos
├── Infrastructure.ORM               # EF Core + contexto e migrations
├── Infrastructure.Events            # Publisher fake de eventos
└── Tests                            # Testes unitários organizados por contexto
```

---

## Como Executar a API Localmente

### 1. Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- [DBeaver] para explorar o banco

### 2. Subir o ambiente com Docker
```bash
docker compose up -d
```

Isso sobe:
- PostgreSQL
- MongoDB
- Redis (não utilizado no momento, mas disponível)

---

### 3. Rodar a aplicação

Navegue até o projeto WebApi e execute:

```bash
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

Acesse: [https://localhost:7181/swagger](https://localhost:7181/swagger)

---

## Executar os Testes

```bash
dotnet test
```

---

## Funcionalidades Implementadas

### Users
- CRUD completo
- Validação com FluentValidation
- Autenticação com JWT

### Products
- CRUD completo
- Validação de modelo
- Testes unitários

### Carts
- CRUD completo
- Validação de quantidade máxima de produto
- Descontos automáticos aplicados
- Testes de regra de negócio

### Sales
- CRUD completo
- Entidade rica com `SaleItem`
- Validação de descontos
- Cancelamento lógico
- Publicação de eventos:
  - `SaleCreated`
  - `SaleModified`
  - `SaleCancelled`

---

## Eventos Publicados

Os eventos são **simulados**, via `FakeDomainEventPublisher`, e registrados no log da aplicação:

- `SaleCreatedEvent`
- `SaleModifiedEvent`
- `SaleCancelledEvent`

---

## Considerações

- Estrutura escalável para microsserviços ou integração com mensageria
- Código limpo, com separation of concerns e padrões modernos
- Cobertura de testes unitários para todos os fluxos principais

---

## Autor

**[Kleiton Freitas]**  
LinkedIn: [www.linkedin.com/in/kleitonsfreitas]  
Email: [kleitonsfreitas@gmail.com]

---
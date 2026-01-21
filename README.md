# 📚 Gerenciador de Livraria - API REST (.NET)

Este projeto é uma **API REST desenvolvida em .NET** como desafio prático para consolidar conceitos de backend, incluindo CRUD, validações de regras de negócio e documentação com Swagger.

A aplicação permite o gerenciamento de livros, possibilitando criar, listar, buscar, atualizar e remover registros.

---

## 🚀 Tecnologias Utilizadas

- .NET 7
- ASP.NET Core Web API
- Swagger (OpenAPI)
- C#
- Visual Studio

---

## 📦 Funcionalidades

- Criar livro
- Listar livros (com filtros opcionais)
- Buscar livro por ID
- Atualizar livro
- Excluir livro
- Validações de regras de negócio

---

## 📋 Regras de Negócio Implementadas

- Título e autor não podem ser duplicados
- Preço não pode ser negativo
- Estoque não pode ser negativo
- Gênero deve ser um valor válido do enum `Genre`
- `CreatedAt` é preenchido na criação do livro
- `UpdatedAt` é atualizado sempre que o livro sofre alterações

---

## ▶️ Como rodar e visualizar o projeto

Este projeto é uma **API**, portanto a visualização do funcionamento é feita através do **Swagger**.

### Pré-requisitos

- .NET SDK 7.0 ou superior
- Visual Studio 2022 (ou VS Code)

### Passo a passo

1. Clone o repositório:

```bash
git clone https://github.com/brunakelly/GerenciadorDeLivraria.git
```

2. Abra o projeto no **Visual Studio**

3. Execute a aplicação:
- Pressione `Ctrl + F5`
- Ou clique em **Run**

4. Após a execução, abra o navegador e acesse:

```
https://localhost:{porta}/swagger
```

> A porta é definida automaticamente pelo Visual Studio e pode ser visualizada no console ao rodar o projeto.

5. No Swagger é possível:
- Visualizar todos os endpoints disponíveis
- Testar as requisições (POST, GET, PUT, DELETE)
- Ver exemplos de request e response

---

## 🧪 Endpoints Principais

- `POST /api/books` → Criar um livro
- `GET /api/books` → Listar livros (com filtros opcionais)
- `GET /api/books/{id}` → Buscar livro por ID
- `PUT /api/books/{id}` → Atualizar livro
- `DELETE /api/books/{id}` → Excluir livro

---

## 📄 Documentação

Toda a documentação dos endpoints, incluindo códigos de resposta e exemplos, está disponível via **Swagger**.

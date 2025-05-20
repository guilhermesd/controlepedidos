# 🍽️ Microserviço: Controle de Pedidos

Este repositório contém o microsserviço **Controle de Pedidos**, responsável pelo **gerenciamento de produtos e clientes** dentro do sistema distribuído de controle de pedidos.

## 📌 Funcionalidades

- Cadastro, atualização e exclusão de **produtos**
- Cadastro, atualização e exclusão de **clientes**
- Consulta de **produtos**, **clientes** e **categorias de produtos**
- Conexão com banco de dados **MySQL RDS da AWS**

---

## 🧱 Arquitetura

Este projeto faz parte de um sistema de **microsserviços**, divididos da seguinte forma:

| Microsserviço | Descrição                                | Repositório | Cobertura de Testes |
|---------------|--------------------------------------------|-------------|----------------------|
| 🍽️ Pedidos     | Gerenciamento de pedidos dos clientes     | [github.com/seu-usuario/ms-pedidos](https://github.com/seu-usuario/ms-pedidos) | ![Cobertura Pedidos](docs/cobertura-pedidos.png) |
| 🧾 Pagamentos  | Processamento de pagamentos e faturas     | [github.com/seu-usuario/ms-pagamentos](https://github.com/seu-usuario/ms-pagamentos) | ![Cobertura Pagamentos](docs/cobertura-pagamentos.png) |
| 👨‍🍳 Produção    | Controle de produção e estoque            | [github.com/seu-usuario/ms-producao](https://github.com/seu-usuario/ms-producao) | ![Cobertura Produção](docs/cobertura-producao.png) |
| 🧍 Clientes    | Cadastro e manutenção de clientes         | **(Este repositório)** | ![Cobertura Clientes](docs/cobertura-clientes.png) |
| 📦 Produtos    | Catálogo de produtos e preços             | **(Este repositório)** | ![Cobertura Produtos](docs/cobertura-produtos.png) |

> 💡 Substitua os links e imagens acima conforme os reais disponíveis no seu repositório.

---

## ⚙️ Tecnologias Utilizadas

- ASP.NET Core 7
- Mysql
- Docker
- GitHub Actions (CI/CD)
- xUnit e Moq para testes
- OpenAPI (Swagger)

---
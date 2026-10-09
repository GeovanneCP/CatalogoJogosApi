# 🎮 Catálogo de Jogos API

> API RESTful de Alta Performance desenvolvida com **.NET 8 / C#** e persistência em **MongoDB**.  
> Projeto concebido segundo os padrões de arquitetura limpa, princípios SOLID, observabilidade e garantia de qualidade com testes automatizados.

---

## 📌 Sobre o Projeto

O **Catálogo de Jogos API** foi desenvolvido com o objetivo de fornecer uma solução escalável para gestão de catálogo, precificação e inventário de jogos eletrónicos físicos e digitais.

A solução resolve as dores clássicas de controlo de catálogo em e-commerces e lojas especializadas:
* **Cadastro estruturado**: Tipagem forte e metadados no padrão BSON.
* **Consultas otimizadas**: Filtros compostos por plataforma e faixa de preço.
* **Relatórios analíticos de inventário**: Pipelines de agregação do MongoDB em tempo real.
* **Monitorização de infraestrutura**: Endpoints de *Health Check* nativos.
* **Qualidade de software**: Testes unitários isolados com dublês de teste (*Mocks*).

---

## 🏛️ Arquitetura e Boas Práticas

O projeto foi desenhado sob uma arquitetura desacoplada em três camadas principais:

```text
[ Requisições HTTP ]
         │
         ▼
┌──────────────────┐
│   Controllers    │  --> Validação de payload & HTTP Status
└────────┬─────────┘
         │
         ▼
┌──────────────────┐
│     Services     │  --> Regras de negócio & Logging estruturado
└────────┬─────────┘
         │
         ▼
┌──────────────────┐
│   Repositories   │  --> MongoDB Driver & Consultas BSON / Agregações
└────────┬─────────┘
         │
         ▼
[( Banco: MongoDB )]

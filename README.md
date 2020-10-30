# TestDgBar
O Bar do DG, quer desenvolver um sistema de comandas para seus clientes.

# Tecnologias implementadas:
- ASP.NET Core 3.1 (com .NET Core 3.1)
- Entity Framework Core 3.1.8
- Swagger UI 5.6.3
- Sql Server Database
- Autofac para gerenciar a injeção de dependência
- API Rest
- xUnit Test (API)

# Arquitetura
- Arquitetura em camadas
- S.O.L.I.D.
- Clean Code
- Domain Driven Design (DDD)
- Repository Pattern

# Front-end
- Utilizado ASP.NET CORE MVC
- Layout base https://adminlte.io/

# Configuração do banco
Para criação do banco foi utilizado o conceito de Database-first.
- Passo a passo para configurar no Visual Studio:

Na aba SQL Server Object Explorer, abaixo de "(localdb)MSSQLLocalDB" clicar com o botão direito em cima "Databases" e criar um banco de dados chamado BARDG.
Após isso, clicar com o direito em cima do banco (BARDG) e selecionar "New Query".
Copiar os comandos descritos no arquivo Script_CriacaoBanco.sql e executar para criar as tabelas.

Feito isso, vá até a aba "Server Explorer", clique em cima de "Data Connections" e "Add Connection". Configure do jeito abaixo:
  * Data Source: Microsoft SQL Server (SqlClient)
  * Server name: (localdb)\mssqllocaldb
Clique em Refresh e após isso vá em "Connect to a database e na primeira opção:
- Select or enter a database name, selecione o banco BARDG.

# Modo de uso
- Inicie ambos projetos: BarDoDG.API e BarDoDG.WebSite para utilizar.

# Sugestões de melhoria
- Adicionar token para autenticação entre Aplicação Web e API;
- Permitir adicionar mais de um item de uma vez na comanda;
- Permitir que a partir do cliente já crie uma comanda;
- Adicionar login para autenticação;

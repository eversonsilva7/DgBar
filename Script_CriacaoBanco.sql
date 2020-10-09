CREATE TABLE Cliente
(
   IdCliente int IDENTITY(1,1) NOT NULL,
   Nome nvarchar(100) NOT NULL,

   CONSTRAINT PK_Cliente PRIMARY KEY (IdCliente)
);

CREATE TABLE Item
(
   IdItem int IDENTITY(1,1) NOT NULL,   
   Descricao nvarchar(100) NOT NULL,
   Valor DECIMAL(10,2) NOT NULL,

   CONSTRAINT PK_Item PRIMARY KEY (IdItem)
);

CREATE TABLE Comanda
(
   IdComanda int IDENTITY(1,1) NOT NULL,   
   DataAbertura datetime NOT NULL,
   DataFechamento datetime NULL,
   ValorTotal DECIMAL(10,2) NULL,
   Desconto DECIMAL(10,2) NULL,
   ValorTotalComDesconto DECIMAL(10,2) NULL,
   IdCliente int NOT NULL,

   CONSTRAINT PK_Camanda PRIMARY KEY (IdComanda),
   CONSTRAINT FK_Comanda_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente (IdCliente)
);

CREATE TABLE ItemComprado
(
   IdItemComprado int IDENTITY(1,1) NOT NULL,   
   IdItem int NOT NULL,
   IdComanda int NOT NULL,

   CONSTRAINT PK_ItemComprado PRIMARY KEY (IdItemComprado),
   CONSTRAINT FK_ItemComprado_Item FOREIGN KEY (IdItem) REFERENCES Item (IdItem),
   CONSTRAINT FK_ItemComprado_Comanda FOREIGN KEY (IdComanda) REFERENCES Comanda (IdComanda)
);

INSERT INTO Item (Descricao, Valor) VALUES ('Cerveja', 5.00);
INSERT INTO Item (Descricao, Valor) VALUES ('Conhaque', 20.00);
INSERT INTO Item (Descricao, Valor) VALUES ('Suco', 50.00);
INSERT INTO Item (Descricao, Valor) VALUES ('Água', 70.00);
CREATE TABLE [Usuario] (
	[Id] INT NOT NULL IDENTITY UNIQUE,
	[Nome] VARCHAR(50) NOT NULL,
	[Senha] VARBINARY(255) NOT NULL,
	[Email] VARCHAR(50) NOT NULL UNIQUE,
	[TipoUsuario] CHAR(3) NOT NULL,
	[Ativo] BIT NOT NULL,
	[Bloqueado] BIT NOT NULL,
	[TentativasAcesso] INT NOT NULL,
	[PrimeiroAcesso] BIT NOT NULL,
	[DataCriacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [Veiculos] (
	[Id] INT NOT NULL IDENTITY UNIQUE,
	[Placa] VARCHAR(7) NOT NULL,
	[Categoria] CHAR(3) NOT NULL,
	[Transportadora] INT NOT NULL,
	[Ativo] BIT NOT NULL,
	[DataCriacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [Transportadoras] (
	[Id] INT NOT NULL IDENTITY UNIQUE,
	[Nome] VARCHAR(50) NOT NULL,
	[CNPJ] VARCHAR(14) NOT NULL,
	[Ativo] BIT NOT NULL,
	[DataCriacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [CategoriaVeiculo] (
	[Id] CHAR(3) NOT NULL UNIQUE,
	[Descricao] VARCHAR(50) NOT NULL,
	[Capacidade] INT NOT NULL,
	[DataCriacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [Motoristas] (
	[Id] INT NOT NULL IDENTITY UNIQUE,
	[Nome] VARCHAR(50) NOT NULL,
	[CPF] VARCHAR(10) NOT NULL,
	[CNH] VARCHAR(10) NOT NULL,
	[Transportadora] INT NOT NULL,
	[Ativo] BIT NOT NULL,
	[DataCriacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [StatusCarregamento] (
	[Id] INT NOT NULL IDENTITY UNIQUE,
	[Descricao] VARCHAR(50) NOT NULL,
	[DataCriacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [PlanejamentoCargas] (
	[Id] VARCHAR(15) NOT NULL UNIQUE,
	[Veiculo] INT NOT NULL,
	[Motorista] INT NOT NULL,
	[Peso] INT NOT NULL,
	[Status] INT NOT NULL,
	[DataCriacao] DATETIME NOT NULL,
	[DataAtualizacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [UsuarioHierarquia] (
	[Id] CHAR(3) NOT NULL UNIQUE,
	[Descricao] VARCHAR(50) NOT NULL,
	[DataCriacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [HistoricoMovimentacao] (
	[Id] INT NOT NULL IDENTITY UNIQUE,
	[Carga] VARCHAR(15) NOT NULL,
	[StatusAnterior] INT NOT NULL,
	[StatusAtual] INT NOT NULL,
	[DataAtualizacao] DATETIME NOT NULL,
	PRIMARY KEY([Id])
);
GO

ALTER TABLE [Usuario]
ADD FOREIGN KEY([TipoUsuario]) REFERENCES [UsuarioHierarquia]([Id])
GO

ALTER TABLE [CategoriaVeiculo]
ADD FOREIGN KEY([Id]) REFERENCES [Veiculos]([Categoria])
GO

ALTER TABLE [Transportadoras]
ADD FOREIGN KEY([Id]) REFERENCES [Veiculos]([Transportadora])
GO

ALTER TABLE [Transportadoras]
ADD FOREIGN KEY([Id]) REFERENCES [Motoristas]([Transportadora])
GO

ALTER TABLE [Veiculos]
ADD FOREIGN KEY([Id]) REFERENCES [PlanejamentoCargas]([Veiculo])
GO

ALTER TABLE [Motoristas]
ADD FOREIGN KEY([Id]) REFERENCES [PlanejamentoCargas]([Motorista])
GO

ALTER TABLE [StatusCarregamento]
ADD FOREIGN KEY([Id]) REFERENCES [PlanejamentoCargas]([Status])
GO

ALTER TABLE [PlanejamentoCargas]
ADD FOREIGN KEY([Id]) REFERENCES [HistoricoMovimentacao]([Carga])
GO
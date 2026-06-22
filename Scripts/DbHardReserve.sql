USE master;
GO

IF DB_ID('Hard_Reserve') IS NOT NULL
BEGIN
    ALTER DATABASE Hard_Reserve SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Hard_Reserve;
END
GO

CREATE DATABASE Hard_Reserve;
GO

USE Hard_Reserve;
GO

CREATE TABLE Usuario (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    Nome            NVARCHAR(150) NOT NULL,
    Email           NVARCHAR(100) NOT NULL UNIQUE,
    Senha           NVARCHAR(255) NOT NULL,
    Status_Usuario  CHAR(1)       NOT NULL DEFAULT 'D' CHECK (Status_Usuario IN ('D','I')),
    Role            CHAR(1)       NOT NULL CHECK (Role IN ('A','P','T')),
    Turma_Usuario   NVARCHAR(50)  NOT NULL
);
GO

CREATE TABLE Kit (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    NomeKit           NVARCHAR(100) NOT NULL,
    Descricao         NVARCHAR(255) NULL,
    UsuarioCriadorId  INT           NOT NULL,
    Localizacao       NVARCHAR(100) NOT NULL,
    Quantidade        INT           NOT NULL,
    CONSTRAINT FK_Kit_Usuario FOREIGN KEY (UsuarioCriadorId) REFERENCES Usuario(Id)
);
GO

CREATE TABLE Hardware (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    Nome               NVARCHAR(100) NOT NULL,
    Descricao          NVARCHAR(255) NULL,
    Quantidade_Total   INT           NOT NULL,
    Localizacao        NVARCHAR(100) NULL,
    Kit_Id             INT           NULL,
    Categoria          NVARCHAR(50)  NULL,
    Status             NVARCHAR(20)  NOT NULL DEFAULT 'disponivel',
    Codigo_Patrimonio  NVARCHAR(50)  NULL,
    Imagem             NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Hardware_Kit FOREIGN KEY (Kit_Id) REFERENCES Kit(Id)
);
GO

CREATE TABLE Reserva (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId      INT          NOT NULL,
    DataInicial    DATETIME     NOT NULL,
    DataFinal      DATETIME     NOT NULL,
    StatusReserva  CHAR(2)      NOT NULL DEFAULT 'PE' CHECK (StatusReserva IN ('PE','AP','CA','RE','DE','AT')),
    Quantidade     INT          NOT NULL,
    Protocolo      NVARCHAR(20) NULL,
    CONSTRAINT FK_Reserva_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);
GO

CREATE TABLE Hardware_Reserva (
    Reserva_Id   INT NOT NULL,
    Hardware_Id  INT NOT NULL,
    Quantidade   INT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Hardware_Reserva PRIMARY KEY (Reserva_Id, Hardware_Id),
    CONSTRAINT FK_HR_Reserva  FOREIGN KEY (Reserva_Id)  REFERENCES Reserva(Id),
    CONSTRAINT FK_HR_Hardware FOREIGN KEY (Hardware_Id) REFERENCES Hardware(Id)
);
GO

INSERT INTO Usuario (Nome, Email, Senha, Status_Usuario, Role, Turma_Usuario)
VALUES ('Técnico do Laboratório', 'tecnico@hardreserve.com', '123', 'D', 'T', 'LAB-01');
GO

INSERT INTO Usuario (Nome, Email, Senha, Status_Usuario, Role, Turma_Usuario)
VALUES ('Aluno de Teste', 'aluno@hardreserve.com', '123', 'D', 'A', 'DS-2026');
GO

INSERT INTO Hardware (Nome, Descricao, Quantidade_Total, Localizacao, Categoria, Status, Codigo_Patrimonio, Imagem)
VALUES
('ESP-32 DevKit', 'Placa de desenvolvimento com Wi-Fi e Bluetooth embutidos.', 13, 'Armário A - Lab 3', 'microcontroladores', 'disponivel', 'HARD-2026-01', 'img/Esp32.png'),
('Arduino Uno R3', 'Placa de prototipagem eletrônica de código aberto.', 13, 'Armário A - Lab 3', 'microcontroladores', 'disponivel', 'HARD-2026-02', 'img/ArduinoUno.png'),
('Servo Motor SG90', 'Atuador com controle preciso de posição.', 13, 'Armário B - Lab 3', 'atuadores', 'disponivel', 'HARD-2026-03', 'img/ServoMotor.png'),
('Sensor Ultrassônico HC-SR04', 'Mede distância entre o sensor e um objeto.', 13, 'Armário B - Lab 3', 'sensores', 'disponivel', 'HARD-2026-04', 'img/SensorDistancia.png'),
('Notebook Dell', 'Notebook para programação e desenvolvimento de projetos.', 5, 'Armário C - Lab 3', 'outros', 'disponivel', 'HARD-2026-05', 'img/Notebook.png');
GO

INSERT INTO Reserva (UsuarioId, DataInicial, DataFinal, StatusReserva, Quantidade, Protocolo) VALUES
(2, '2026-06-21T09:00:00', '2026-06-23T09:00:00', 'PE', 1, '210626090000'),
(2, '2026-06-20T10:00:00', '2026-06-24T10:00:00', 'AP', 1, '200626100000'),
(2, '2026-06-18T11:00:00', '2026-06-19T11:00:00', 'CA', 1, '180626110000'),
(2, '2026-06-19T12:00:00', '2026-06-25T12:00:00', 'RE', 1, '190626120000'),
(2, '2026-06-10T13:00:00', '2026-06-12T13:00:00', 'DE', 1, '100626130000'),
(2, '2026-06-05T14:00:00', '2026-06-07T14:00:00', 'AT', 1, '050626140000');
GO

INSERT INTO Hardware_Reserva (Reserva_Id, Hardware_Id, Quantidade) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 1),
(4, 4, 1),
(5, 5, 1),
(6, 1, 1);
GO

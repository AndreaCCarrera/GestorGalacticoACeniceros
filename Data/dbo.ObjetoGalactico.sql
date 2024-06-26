CREATE TABLE [dbo].[ObjetoGalactico] (
    [Id]              INT          NOT NULL,
    [Tipo]            VARCHAR (30) NOT NULL,
    [Nombre]          VARCHAR (30) NOT NULL,
    [Descubrimiento]  DATE         NOT NULL,
    [Tamano]          FLOAT (53)   NOT NULL,
    [DistanciaTierra] FLOAT (53)   NOT NULL,
    [Agua]            BIT          NOT NULL,
    [Vida]            BIT          NOT NULL,
    [Atmosfera]       BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


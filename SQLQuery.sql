
--CREATE TABLE [dbo].[ObjetoGalactico] (
--    [Id]              INT          NOT NULL,
--    [Tipo]            VARCHAR (30) NOT NULL,
--    [Nombre]          VARCHAR (30) NOT NULL,
--    [Descubrimiento]  DATE         NOT NULL,
--    [Tamano]          FLOAT (53)   NOT NULL,
--    [DistanciaTierra] FLOAT (53)   NOT NULL,
--    [Agua]            BIT          NOT NULL,
--    [Vida]            BIT          NOT NULL,
--    [Atmosfera]       BIT          NOT NULL,
--    PRIMARY KEY CLUSTERED ([Id] ASC)
--);

--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (1, 'Satélite', 'Luna', '2023-09-10', 342.0, 56565.0, 0, 0, 0);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (2, 'Galaxia', 'Andrómeda', '2020-01-04 15:53:00', 14.0, 432342.0, 0, 0, 0);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (3, 'Planeta', 'Tierra', '2023-11-06 15:53:00', 0.0, 0.0, 1, 1, 1);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (4, 'Planeta', 'Saturno', '2023-02-06', 12345.0, 8979.0, 0, 0, 1);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (5, 'Satélite', 'Titán', '2024-01-01 15:53:00', 76687.0, 978967.0, 0, 0, 1);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (6, 'Galaxia', 'ViaLactea', '2020-01-01', 43534.0, 0.0, 1, 1, 1);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (7, 'Planeta', 'Mercurio', '2024-03-11', 4.0, 9.0, 1, 1, 1);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (8, '', 'Io', '2023-08-01', 198.0, 74536.0, 1, 0, 0);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (9, 'Planeta', 'Venus', '1987-01-04', 455.0, 878.0, 0, 0, 0);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (10, 'Planeta', 'Marte', '2023-12-04', 5468.0, 75.0, 0, 0, 0);


--INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) 
--VALUES (11, 'Galaxia', 'Phoenix', '2024-02-26', 2141.0, 53667.0, 1, 0, 1);


--CREATE PROCEDURE PruebaProcedimientoAlmacenado
--(
--@param VARCHAR(30) = NULL, -- Parámetro de entrada, NULL default.
--@paramNombreTabla VARCHAR(50), -- Parámetro de entrada
--@param_salida VARCHAR(150) OUTPUT -- Parámetro de salida
--)
--AS
--BEGIN
--SET @param_salida = 'ESTA ES LA RESPUESTA DE UN PROCEDIMIENTO
--ALMACENADO DEL GESTOR DE ELEMENTOS. EL PARÁMETRO QUE ME HAS ENVIADO
--ES: ' + @param;
--DECLARE @sqlQuery NVARCHAR(200);
--DECLARE @numElems INT;
--SET @sqlQuery = 'SELECT @numElems = COUNT(*) FROM ' +
--@paramNombreTabla;
--EXEC SP_EXECUTESQL @sqlQuery, N'@numElems INT OUTPUT', @numElems
--OUTPUT
--RETURN @numElems;
--END
--GO
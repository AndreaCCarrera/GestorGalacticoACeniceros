CREATE PROCEDURE PruebaProcedimientoAlmacenado
(
@param VARCHAR(30) = NULL, -- Parámetro de entrada, NULL default.
@paramNombreTabla VARCHAR(50), -- Parámetro de entrada
@param_salida VARCHAR(150) OUTPUT -- Parámetro de salida
)
AS
BEGIN
SET @param_salida = 'ESTA ES LA RESPUESTA DE UN PROCEDIMIENTO
ALMACENADO DEL GESTOR DE ELEMENTOS. EL PARÁMETRO QUE ME HAS ENVIADO
ES: ' + @param;
DECLARE @sqlQuery NVARCHAR(200);
DECLARE @numElems INT;
SET @sqlQuery = 'SELECT @numElems = COUNT(*) FROM ' +
@paramNombreTabla;
EXEC SP_EXECUTESQL @sqlQuery, N'@numElems INT OUTPUT', @numElems
OUTPUT
RETURN @numElems;
END
GO
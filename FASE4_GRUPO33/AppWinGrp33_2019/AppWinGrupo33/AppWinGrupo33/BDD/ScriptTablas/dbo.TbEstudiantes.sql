CREATE TABLE [dbo].[TbEstudiantes]
(
	
	[Id_registro] INT NOT NULL PRIMARY KEY, 
    [Tipo_Documento] INT NULL, 
    [Num_Documento] VARCHAR(30) NULL, 
    [Nombres] VARCHAR(100) NULL, 
    [Apellidos] VARCHAR(100) NULL, 
    [Correo] VARCHAR(100) NULL, 
    [Telefono] VARCHAR(10) NULL, 
    [Password] VARCHAR(30) NULL, 
    [Rol] VARCHAR(50) NULL
)

 /*CREO NUEVO ESQUEMA */
CREATE SCHEMA [3FG] AUTHORIZATION [gd];
GO
/*CREO LAS TABLAS DEL DER*/

/*CREO LA TABLA DE USUARIOS */

CREATE TABLE [3FG].USUARIOS(
	ID_USUARIO BIGINT PRIMARY KEY IDENTITY(1,1),
	USUARIO_NOMBRE VARCHAR(250),
	CONTRASEÑA VARCHAR(250),
	CANT_INTENTOS TINYINT DEFAULT 0,
	NOMBRE VARCHAR(100),
	APELLIDO VARCHAR(100),
	TIPO_DE_DOCUMENTO VARCHAR(100) DEFAULT 'D.N.I',
	NUMERO_DOCUMENTO BIGINT,
	TELEFONO BIGINT,
	DIRECCION VARCHAR(100),
	MAIL VARCHAR(100),
	FECHA_NACIMIENTO DATETIME,
	SEXO VARCHAR(20),
	HABILITADO TINYINT DEFAULT 1
);
GO

/*CREO LA TABLA DE ROLES */
CREATE TABLE [3FG].ROLES (
	ID_ROL BIGINT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE_ROL VARCHAR(100) UNIQUE,
	HABILITADO TINYINT DEFAULT 1
);
GO

/*CREO LA TABLA DE ROLES POR USUARIO*/
CREATE TABLE [3FG].ROLES_USUARIO (
	ID_USUARIO BIGINT NOT NULL,
	ID_ROL BIGINT NOT NULL,
);
GO

/*CREO LA TABLA DE FUNCIONALIDADES*/
CREATE TABLE [3FG].FUNCIONALIDADES (
	ID_FUNCIONALIDAD BIGINT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE VARCHAR(100) UNIQUE,
);
GO

/*CREO LA TABLA DE FUNCIONALIDADES POR ROL*/
CREATE TABLE [3FG].FUNCIONALIDADES_ROL (
	ID_ROL BIGINT NOT NULL,
	ID_FUNCIONALIDAD BIGINT NOT NULL,
);
GO

/*CREO LA TABLA DE AFILIADOS */
CREATE TABLE [3FG].AFILIADOS (
	ID_USUARIO BIGINT PRIMARY KEY,
	ID_PLAN BIGINT,
	ESTADO_CIVIL VARCHAR(20),
	CANT_FAMILIARES BIGINT DEFAULT 0,
	RAIZ_AFILIADO BIGINT,
	NUMERO_FAMILIA BIGINT DEFAULT 01,
	CANT_BONOS_UTILIZADOS BIGINT
);
GO

/*CREO LA TABLA DE PROFESIONALES */
CREATE TABLE [3FG].PROFESIONALES (
	ID_USUARIO BIGINT PRIMARY KEY,
	MATRICULA BIGINT,
	INICIO_DISPONIBILIDAD DATETIME,
	FIN_DISPONIBILIDAD DATETIME
);
GO

/*CREO LA TABLA DE HISTORIAL_CAMBIOS_PLAN*/
CREATE TABLE [3FG].HISTORIAL_CAMBIOS_PLAN (
	ID_CAMBIO BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_USUARIO BIGINT,
	MOTIVO_CAMBIO_PLAN VARCHAR(250),
	FECHA_MODIFICACION DATETIME
);
GO

/*CREO LA TABLA DE ESPECIALIDADES*/
CREATE TABLE [3FG].ESPECIALIDADES (
	ID_ESPECIALIDAD BIGINT PRIMARY KEY,
	ID_TIPO_ESPECIALIDAD BIGINT,
	DESCRIPCION_ESPECIALIDAD VARCHAR(250)
);
GO

/*CREO LA TABLA DE ESPECIALIDAD_PROFESIONAL*/
CREATE TABLE [3FG].ESPECIALIDAD_PROFESIONAL (
	ID_USUARIO BIGINT NOT NULL,
	ID_ESPECIALIDAD BIGINT NOT NULL
);
GO

/*CREO LA TABLA DE AGENDA*/
CREATE TABLE [3FG].AGENDA (
	ID_AGENDA BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_USUARIO BIGINT,
	ID_ESPECIALIDAD BIGINT,
	DIA_ATENCION VARCHAR(20),
	INICIO_ATENCION TIME,
	FIN_ATENCION TIME
);
GO 

/*CREO LA TABLA DE TIPO_ESPECIALIDAD */
CREATE TABLE [3FG].TIPO_ESPECIALIDAD (
	ID_TIPO_ESPECIALIDAD BIGINT PRIMARY KEY,
	DESCRIPCION_TIPO_ESPECIALIDAD VARCHAR(250)
);
GO

/*CREO LA TABLA DE PLANES*/
CREATE TABLE [3FG].PLANES (
	ID_PLAN BIGINT IDENTITY(555555,1) PRIMARY KEY,
	DESCRIPCION_PLAN VARCHAR(250),
	PRECIO_BONO_CONSULTA BIGINT,
	PRECIO_BONO_FARMACIA BIGINT
);
GO

/*CREO LA TABLA DE LOS BONOS*/
CREATE TABLE [3FG].BONOS (
	ID_BONO BIGINT IDENTITY(46494,1) PRIMARY KEY,
	ID_PLAN BIGINT,
	ID_COMPRA BIGINT,
	NUMERO_CONSULTA BIGINT
);
GO

/*CREO LA TABLA DE COMPRAS*/
CREATE TABLE [3FG].COMPRAS (
	ID_COMPRA BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_USUARIO BIGINT,
	FECHA_COMPRA DATETIME,
	CANTIDAD_BONOS BIGINT,
	MONTO_PAGADO BIGINT
);
GO 

/*CREO LA TABLA DE TURNOS*/
CREATE TABLE [3FG].TURNOS (
	ID_TURNO BIGINT IDENTITY(56565,1) PRIMARY KEY,
	ID_AFILIADO BIGINT,
	ID_AGENDA BIGINT,
	FECHA_TURNO DATETIME
);
GO

/*CREO LA TABLA DE RECEPCIONES*/
CREATE TABLE [3FG].RECEPCIONES (
	ID_RECEPCION BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_TURNO BIGINT,
	ID_BONO BIGINT,
	FECHA_RECEPCIONES DATETIME
);
GO

/*CREO LA TABLA DE ATENCIONES_MEDICAS*/
CREATE TABLE [3FG].ATENCIONES_MEDICAS (
	ID_ATENCION BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_RECEPCION BIGINT,
	FECHA_ATENCION DATETIME,
	DIAGNOSTICO VARCHAR(250),
	SINTOMAS VARCHAR(250)
);
GO

/*CREO LA TABLA DE CANCELACIONES*/
CREATE TABLE [3FG].CANCELACIONES (
	ID_CANCELACION BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_TURNO BIGINT,
	TIPO_CANCELACION VARCHAR(20),
	MOTIVO_CANCELACION VARCHAR(250)
);
GO

/* CREO LAS PRIMARY KEY COMPUESTAS  */
alter table [3FG].ROLES_USUARIO add constraint PK_ROL_POR_USUARIO
	primary key clustered (ID_ROL,ID_USUARIO);
GO
alter table [3FG].FUNCIONALIDADES_ROL add constraint PK_FUNCIONALIDAD_POR_ROL
	primary key clustered (ID_ROL,ID_FUNCIONALIDAD);
GO
alter table [3FG].ESPECIALIDAD_PROFESIONAL add constraint PK_ESPECECIALIDAD_POR_PROFESIONAL
	primary key clustered (ID_USUARIO,ID_ESPECIALIDAD);
GO

/*CREO LA FOREIGN KEY COMPUESTA*/ --NO SE SI ESTO ESTA BIEN--
alter table [3FG].AGENDA add constraint FK_AGENDA_ESPECIALIDAD_PROFESIONAL
	foreign key (ID_USUARIO,ID_ESPECIALIDAD) references [3FG].ESPECIALIDAD_PROFESIONAL (ID_USUARIO,ID_ESPECIALIDAD) ;
GO

/*CREO LAS FOREIGN KEY*/ 

alter table [3FG].FUNCIONALIDADES_ROL add constraint FK1_FUNC_ROL
	foreign key (ID_ROL) references [3FG].ROLES (ID_ROL);
GO
alter table [3FG].FUNCIONALIDADES_ROL add constraint FK2_FUNC_ROL
	foreign key (ID_FUNCIONALIDAD) references [3FG].FUNCIONALIDADES (ID_FUNCIONALIDAD);
GO	
alter table [3FG].ROLES_USUARIO add constraint FK1_ROL_USUARIO
	foreign key (ID_ROL) references [3FG].ROLES (ID_ROL);
GO	
alter table [3FG].ROLES_USUARIO add constraint FK2_ROL_USUARIO
	foreign key (ID_USUARIO) references [3FG].USUARIOS (ID_USUARIO);
GO
alter table [3FG].AFILIADOS add constraint FK_AFILIADO_USUARIO
	foreign key (ID_USUARIO) references [3FG].USUARIOS (ID_USUARIO);
GO
alter table [3FG].PROFESIONALES add constraint FK_PROFESIONAL_USUARIO
	foreign key (ID_USUARIO) references [3FG].USUARIOS (ID_USUARIO);
GO
alter table [3FG].AFILIADOS add constraint FK_AFILIADO_PLAN
	foreign key (ID_PLAN) references [3FG].PLANES (ID_PLAN);
GO
alter table [3FG].HISTORIAL_CAMBIOS_PLAN add constraint FK2_AFILIADO_CAMBIO_PLAN
	foreign key (ID_USUARIO) references [3FG].AFILIADOS (ID_USUARIO);
GO
alter table [3FG].ESPECIALIDAD_PROFESIONAL add constraint FK1_ESPECIALIDAD_POR_PROFESIONAL
	foreign key (ID_USUARIO) references [3FG].PROFESIONALES (ID_USUARIO);
GO
alter table [3FG].ESPECIALIDAD_PROFESIONAL add constraint FK2_ESPECIALIDAD_POR_PROFESIONAL
	foreign key (ID_ESPECIALIDAD) references [3FG].ESPECIALIDADES (ID_ESPECIALIDAD);
GO
alter table [3FG].ESPECIALIDADES add constraint FK_ESPECIALIDADES_TIPO_DE_ESPECIALIDAD
	foreign key (ID_TIPO_ESPECIALIDAD) references [3FG].TIPO_ESPECIALIDAD (ID_TIPO_ESPECIALIDAD);
GO
alter table [3FG].BONOS add constraint FK_BONO_COMPRA
	foreign key (ID_COMPRA) references [3FG].COMPRAS (ID_COMPRA);
GO
alter table [3FG].BONOS add constraint FK_BONO_PLAN
	foreign key (ID_PLAN) references [3FG].PLANES (ID_PLAN);
GO
alter table [3FG].COMPRAS add constraint FK_COMPRA_AFILIADO
	foreign key (ID_USUARIO) references [3FG].AFILIADOS (ID_USUARIO);
GO
alter table [3FG].TURNOS add constraint FK_TURNO_AFILIADO
	foreign key (ID_AFILIADO) references [3FG].AFILIADOS (ID_USUARIO);
GO
alter table [3FG].TURNOS add constraint FK_TURNO_AGENDA_PROFESIONAL
	foreign key (ID_AGENDA) references [3FG].AGENDA (ID_AGENDA);
GO
alter table [3FG].RECEPCIONES add constraint FK_RECEPCIONES_TURNO
	foreign key (ID_TURNO) references [3FG].TURNOS (ID_TURNO);
GO
alter table [3FG].RECEPCIONES add constraint FK_RECEPCIONES_BONO
	foreign key (ID_BONO) references [3FG].BONOS (ID_BONO);
GO
alter table [3FG].ATENCIONES_MEDICAS add constraint FK_ATENCIONES_MEDICAS_RECEPCION
	foreign key (ID_RECEPCION) references [3FG].RECEPCIONES (ID_RECEPCION);
GO
alter table [3FG].CANCELACIONES add constraint FK_CANCELACIONES_TURNO
	foreign key (ID_TURNO) references [3FG].TURNOS (ID_TURNO);
GO

/* Crear trigger que ante cada recepcion cree la consulta correspondiente */

CREATE TRIGGER [3FG].CargarAtencionDespuesDeLaRecepcionTrigger ON [3FG].RECEPCIONES
AFTER INSERT
AS
BEGIN
	INSERT INTO [3FG].ATENCIONES_MEDICAS(ID_RECEPCION,FECHA_ATENCION,DIAGNOSTICO,SINTOMAS)
	SELECT i.ID_RECEPCION,Bono_Consulta_Fecha_Impresion,Consulta_Enfermedades,Consulta_Sintomas
	FROM gd_esquema.Maestra m, inserted i
	WHERE m.Turno_Numero = i.ID_TURNO
	AND Compra_Bono_Fecha is NULL
	AND Bono_Consulta_Fecha_Impresion is NOT NULL
END;
GO

/*FUNCIONES RELACIONADAS CON LA FECHA UTILIZADAS PARA GENERAR LA AGENDA*/

CREATE FUNCTION [3FG].obtenerDia(@FECHA DATETIME)
RETURNS VARCHAR(20)
AS
BEGIN
	DECLARE @DIA TINYINT, @NOMBREDIA VARCHAR(20);
	SET @DIA = DATEPART(WEEKDAY,@FECHA);
	SET @NOMBREDIA = (
	CASE @DIA
	WHEN 1 THEN 'DOMINGO'
	WHEN 2 THEN 'LUNES'
	WHEN 3 THEN 'MARTES'
	WHEN 4 THEN 'MIERCOLES'
	WHEN 5 THEN 'JUEVES'
	WHEN 6 THEN 'VIERNES'
	WHEN 7 THEN 'SABADO'
	END
	)
	RETURN @NOMBREDIA;
END
GO

CREATE FUNCTION [3FG].obtenerHora(@FECHA DATETIME)
RETURNS CHAR(8)
AS
BEGIN
DECLARE @HORA CHAR(8)
SET @HORA = CONVERT(CHAR(8), @FECHA, 108)
	RETURN @HORA;
END
GO

/* -- Migracion-- */

	
CREATE PROCEDURE [3FG].MigrarAfiliadosAUsuarios
AS
BEGIN

	--Se migran los pacientes de la tabla Maestra
	INSERT INTO [3FG].USUARIOS(NOMBRE,APELLIDO,NUMERO_DOCUMENTO,DIRECCION,TELEFONO,MAIL,FECHA_NACIMIENTO)
	SELECT DISTINCT Paciente_Nombre,Paciente_Apellido,Paciente_Dni,Paciente_Direccion,Paciente_Telefono,Paciente_Mail,Paciente_Fecha_Nac 
	FROM gd_esquema.Maestra

END;
GO

/* FALTA VER COMO RELACIONAMOS LOS PACIENTES DE LA TABLA MAESTRA CON SUS FAMILIARES*/
CREATE PROCEDURE [3FG].MigrarAfiliados
AS
BEGIN
	
	alter TABLE [3FG].AFILIADOS 
	NOCHECK CONSTRAINT FK_AFILIADO_PLAN;

	--Se migran los pacientes de la tabla Maestra
	INSERT INTO [3FG].AFILIADOS(ID_USUARIO,ID_PLAN, CANT_BONOS_UTILIZADOS,RAIZ_AFILIADO)
	SELECT DISTINCT ID_USUARIO,Plan_Med_Codigo,0, ID_USUARIO 
	FROM gd_esquema.Maestra M JOIN [3FG].USUARIOS U ON (M.Paciente_Dni = U.NUMERO_DOCUMENTO)

END;
GO

CREATE PROCEDURE [3FG].MigrarProfesionalesAUsuarios
AS
BEGIN

	--Se migran los profesionales de la tabla Maestra
	INSERT INTO [3FG].USUARIOS(NOMBRE,APELLIDO,NUMERO_DOCUMENTO,DIRECCION,TELEFONO,MAIL,FECHA_NACIMIENTO)
	SELECT DISTINCT Medico_Nombre,Medico_Apellido,Medico_Dni,Medico_Direccion,Medico_Telefono,Medico_Mail,Medico_Fecha_Nac 
	FROM gd_esquema.Maestra

END;
GO

/* VER EL TEMA DE LA MATRICULA, SAQUE LA CONSTRAINT UNIQUE PORQUE NO DEJABA MIGRAR */
CREATE PROCEDURE [3FG].MigrarProfesionales
AS
BEGIN

	--Se migran los profesionales de la tabla Maestra
	INSERT INTO [3FG].PROFESIONALES(ID_USUARIO,INICIO_DISPONIBILIDAD,FIN_DISPONIBILIDAD)
	SELECT DISTINCT ID_USUARIO,MIN(Turno_Fecha) INICIO_DISPONIBILIDAD,MAX(Bono_Consulta_Fecha_Impresion) FIN_DISPONIBILIDAD
	FROM gd_esquema.Maestra M JOIN [3FG].USUARIOS U ON (M.Medico_Dni = U.NUMERO_DOCUMENTO)
	WHERE Compra_Bono_Fecha IS NULL
	GROUP BY ID_USUARIO

END;
GO


CREATE PROCEDURE [3FG].MigrarTiposDeEspecialidad
AS
BEGIN

	--Se migran los tipos de especialidad de la tabla Maestra
	INSERT INTO [3FG].TIPO_ESPECIALIDAD(ID_TIPO_ESPECIALIDAD,DESCRIPCION_TIPO_ESPECIALIDAD)
	SELECT DISTINCT Tipo_Especialidad_Codigo,Tipo_Especialidad_Descripcion
	FROM gd_esquema.Maestra
	WHERE Tipo_Especialidad_Codigo is NOT NULL
	ORDER BY 1 asc

END;
GO

CREATE PROCEDURE [3FG].MigrarEspecialidades
AS
BEGIN

	--Se migran las especialidades de la tabla Maestra
	INSERT INTO [3FG].ESPECIALIDADES(ID_ESPECIALIDAD,ID_TIPO_ESPECIALIDAD,DESCRIPCION_ESPECIALIDAD)
	SELECT DISTINCT Especialidad_Codigo,Tipo_Especialidad_Codigo,Especialidad_Descripcion
	FROM gd_esquema.Maestra
	WHERE Especialidad_Codigo is NOT NULL
	ORDER BY 1 asc

END;
GO

CREATE PROCEDURE [3FG].MigrarEspecialidadPorProfesional
AS
BEGIN

	--Se migran las especialidades por profesional de la tabla Maestra
	INSERT INTO [3FG].ESPECIALIDAD_PROFESIONAL(ID_USUARIO,ID_ESPECIALIDAD)
	SELECT DISTINCT u.ID_USUARIO,m.Especialidad_Codigo
	FROM gd_esquema.Maestra m, USUARIOS u
	WHERE m.Medico_Dni = u.NUMERO_DOCUMENTO
	
END;
GO

CREATE PROCEDURE [3FG].MigrarAgenda
AS
BEGIN

	alter TABLE [3FG].AGENDA 
	NOCHECK CONSTRAINT FK_AGENDA_ESPECIALIDAD_PROFESIONAL;
	
	--Se genera la agenda a partir de la tabla Maestra
	INSERT INTO [3FG].AGENDA(ID_USUARIO,ID_ESPECIALIDAD,DIA_ATENCION,INICIO_ATENCION,FIN_ATENCION)
	SELECT ID_USUARIO, Especialidad_Codigo,[3FG].obtenerDia(Turno_Fecha) DIA_ATENCION,MIN([3FG].obtenerHora(Turno_Fecha))INICIO_ATENCION,MAX([3FG].obtenerHora(Bono_Consulta_Fecha_Impresion)) FIN_ATENCION
	FROM gd_esquema.Maestra M JOIN [3FG].USUARIOS U ON (M.Medico_Dni = U.NUMERO_DOCUMENTO)
	WHERE Compra_Bono_Fecha IS NULL
	GROUP BY ID_USUARIO,Especialidad_Codigo,[3FG].obtenerDia(Turno_Fecha)
	ORDER BY 1 ASC, 2 ASC

END;
GO

CREATE PROCEDURE [3FG].MigrarPlanes
AS
BEGIN

	--Se migran los planes de la tabla Maestra
	INSERT INTO [3FG].PLANES(DESCRIPCION_PLAN,PRECIO_BONO_CONSULTA,PRECIO_BONO_FARMACIA)
	SELECT DISTINCT Plan_Med_Descripcion,Plan_Med_Precio_Bono_Consulta,Plan_Med_Precio_Bono_Farmacia
	FROM gd_esquema.Maestra
	ORDER BY 1 ASC

END;
GO

CREATE PROCEDURE [3FG].MigrarCompras
AS
BEGIN

	alter TABLE [3FG].COMPRAS
	NOCHECK CONSTRAINT FK_COMPRA_AFILIADO;

	--Se cargan las compras luego de migrar los bonos
	INSERT INTO [3FG].COMPRAS(ID_USUARIO,FECHA_COMPRA,CANTIDAD_BONOS,MONTO_PAGADO)
	SELECT ID_USUARIO,Compra_Bono_Fecha,COUNT(*) CANTIDAD_BONOS,(COUNT(*)*Plan_Med_Precio_Bono_Consulta) TOTAL_PAGADO
	FROM gd_esquema.Maestra m, [3FG].USUARIOS u
	WHERE m.Paciente_Dni= u.NUMERO_DOCUMENTO
	AND Compra_Bono_Fecha=Bono_Consulta_Fecha_Impresion
	AND Compra_Bono_Fecha is NOT NULL
	GROUP BY ID_USUARIO,Compra_Bono_Fecha,Plan_Med_Precio_Bono_Consulta

END;
GO

/*VER SI ESTO ESTA BIEN*/
CREATE PROCEDURE [3FG].MigrarBonos
AS
BEGIN

	--Se migran los bonos de la tabla Maestra
	INSERT INTO [3FG].BONOS(ID_PLAN,ID_COMPRA)
	SELECT Plan_Med_Codigo,ID_COMPRA
	FROM gd_esquema.Maestra m,  [3FG].COMPRAS c, [3FG].AFILIADOS a, [3FG].USUARIOS u
	WHERE c.ID_USUARIO = a.ID_USUARIO 
	AND a.ID_USUARIO = u.ID_USUARIO
	AND m.Paciente_Dni = u.NUMERO_DOCUMENTO
	AND Compra_Bono_Fecha = Bono_Consulta_Fecha_Impresion
	AND Bono_Consulta_Numero is NOT NULL
	AND c.FECHA_COMPRA = Compra_Bono_Fecha
	ORDER BY Bono_Consulta_Numero ASC

END;
GO


/*Tablas temporales para afiliados y medicos*/

CREATE TABLE [3FG].#TMP_AFILIADOS (
	ID_AFILIADO BIGINT,
	NUMERO_DOCUMENTO BIGINT
)
GO

CREATE TABLE [3FG].#TMP_PROFESIONALES (
	ID_PROFESIONAL BIGINT,
	NUMERO_DOCUMENTO BIGINT
)
GO


CREATE PROCEDURE [3FG].Migrar_Afiliados_Profesionales_Temporales
AS
BEGIN

	--Se migran los AFILIADOS de la usuarios a la temporal
	INSERT INTO #TMP_AFILIADOS (ID_AFILIADO,NUMERO_DOCUMENTO)
	SELECT DISTINCT u.ID_USUARIO,m.Paciente_Dni
	FROM USUARIOS u JOIN gd_esquema.Maestra m ON (u.NUMERO_DOCUMENTO = m.Paciente_Dni)
	
	--Se migran los PROFESIONES de la usuarios a la temporal
	INSERT INTO #TMP_PROFESIONALES (ID_PROFESIONAL,NUMERO_DOCUMENTO)
	SELECT DISTINCT u.ID_USUARIO,m.Medico_Dni
	FROM USUARIOS u JOIN gd_esquema.Maestra m ON (u.NUMERO_DOCUMENTO = m.Medico_Dni) 

END;
GO

CREATE PROCEDURE [3FG].MigrarTurnos
AS
BEGIN

	alter TABLE [3FG].TURNOS
	NOCHECK CONSTRAINT FK_TURNO_AFILIADO;

	alter TABLE [3FG].TURNOS
	NOCHECK CONSTRAINT FK_TURNO_AGENDA_PROFESIONAL;


	--Se migran los turnos de la tabla Maestra
	INSERT INTO [3FG].TURNOS(ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
	SELECT DISTINCT a.ID_AFILIADO,ag.ID_AGENDA,m.Turno_Fecha
	FROM gd_esquema.Maestra m, #TMP_AFILIADOS a, #TMP_PROFESIONALES p, [3FG].Agenda ag
	WHERE m.Paciente_Dni = a.NUMERO_DOCUMENTO
	AND m.Medico_Dni = p.NUMERO_DOCUMENTO
	AND p.ID_PROFESIONAL = ag.ID_USUARIO
	AND m.Especialidad_Codigo = ag.ID_ESPECIALIDAD
	AND [3FG].obtenerDia(m.Turno_Fecha) = ag.DIA_ATENCION
	AND m.Compra_Bono_Fecha is NULL
	AND m.Bono_Consulta_Fecha_Impresion is NULL
	AND m.Consulta_Sintomas is NULL
	AND m.Consulta_Enfermedades is NULL
	ORDER BY 1 ASC

END;
GO

CREATE PROCEDURE [3FG].MigrarRecepciones
AS
BEGIN

	alter TABLE [3FG].RECEPCIONES
	NOCHECK CONSTRAINT FK_RECEPCIONES_BONO;

	alter TABLE [3FG].RECEPCIONES
	NOCHECK CONSTRAINT FK_RECEPCIONES_TURNO;

	--Se cargan las recepciones de la tabla maestra
	INSERT INTO [3FG].RECEPCIONES(ID_TURNO,ID_BONO,FECHA_RECEPCIONES)
	SELECT Turno_Numero,Bono_Consulta_Numero,Bono_Consulta_Fecha_Impresion
	FROM gd_esquema.Maestra
	WHERE Compra_Bono_Fecha is NULL
	AND Bono_Consulta_Fecha_Impresion is NOT NULL

END;
GO

/*PROCEDURE QUE ESTABLECE USUARIOS Y CONTRASEÑAS POR DEFAULT A LOS USUARIOS MIGRADOS*/
CREATE PROCEDURE [3FG].EsteblecerUsuarioYContraseñaPorDefault
AS
BEGIN
	
	DECLARE @nombre VARCHAR(100)
	DECLARE @apellido VARCHAR(100)
	DECLARE @documento BIGINT
	
	DECLARE UsuarioCursor CURSOR FOR

	SELECT NOMBRE,APELLIDO,NUMERO_DOCUMENTO FROM [3FG].USUARIOS

	OPEN UsuarioCursor;

FETCH NEXT FROM UsuarioCursor INTO @nombre,@apellido,@documento

WHILE @@FETCH_STATUS = 0

BEGIN

SELECT @nombre=NOMBRE, @apellido=APELLIDO, @documento = NUMERO_DOCUMENTO
FROM [3FG].USUARIOS
WHERE NUMERO_DOCUMENTO = @documento;


UPDATE [3FG].USUARIOS
SET USUARIO_NOMBRE= @nombre + @apellido,CONTRASEÑA=(SELECT SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA2_256',CAST(@documento AS VARCHAR(100)))),3,250))
WHERE  NUMERO_DOCUMENTO = @documento;


FETCH NEXT FROM UsuarioCursor INTO @nombre,@apellido,@documento

END;

CLOSE UsuarioCursor;
DEALLOCATE UsuarioCursor;

END;
GO

/* PROCEDURES DE LA APLICACION*/

create procedure agregarEntablasUsuarioYAfiliado @usuario varchar(250), @contraseña varchar(250),@id_plan bigint
as
begin 

INSERT INTO [3FG].USUARIOS(USUARIO_NOMBRE,CONTRASEÑA)
VALUES (@usuario,(SELECT SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA2_256',@contraseña)),3,250) ))

 select ID_USUARIO from [3FG].USUARIOS where USUARIO_NOMBRE= @usuario

 insert into [3FG].AFILIADOS(ID_USUARIO,ID_PLAN,ESTADO_CIVIL,CANT_FAMILIARES,RAIZ_AFILIADO,NUMERO_FAMILIA)
 values((select ID_USUARIO from [3FG].USUARIOS where USUARIO_NOMBRE= @usuario),@id_plan,'soltero',2,12313,01)

end
GO

/* -- Inserto los ROLES -- */

INSERT INTO [3FG].ROLES(NOMBRE_ROL) VALUES('Administrativo');
INSERT INTO [3FG].ROLES(NOMBRE_ROL) VALUES('Afiliado');
INSERT INTO [3FG].ROLES(NOMBRE_ROL) VALUES('Profesional');

/* Se inserta el usuario admin, password w23e */

INSERT INTO [3FG].USUARIOS(USUARIO_NOMBRE,CONTRASEÑA)
VALUES ('admin',(SELECT SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA2_256','w23e')),3,250) ))
GO

/* Inserto las FUNCIONALIDADES */

INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('ABM de Rol');
INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('ABM de Afiliado');
INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('Solicitar turno');
INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('Registrar agenda del profesional');
INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('Comprar Bonos');
INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('Cancelar turno');
INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('Registrar resultado consulta');
INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('Listados Estadisticos');
INSERT INTO [3FG].FUNCIONALIDADES(NOMBRE) VALUES('Registrar Llegadas');



/*Se agregan funcionalidades al rol Profesional*/

INSERT INTO [3FG].FUNCIONALIDADES_ROL(ID_ROL, ID_FUNCIONALIDAD)
SELECT tablaRol.ID_ROL,tablaFuncionalidad.ID_FUNCIONALIDAD FROM [3FG].ROLES  tablaRol, [3FG].FUNCIONALIDADES tablaFuncionalidad
WHERE tablaRol.NOMBRE_ROL = 'Profesional' AND tablaFuncionalidad.NOMBRE IN ('Registrar agenda del profesional', 'Cancelar turno', 'Registrar resultado consulta');
GO

INSERT INTO [3FG].FUNCIONALIDADES_ROL(ID_ROL, ID_FUNCIONALIDAD)
SELECT tablaRol.ID_ROL,tablaFuncionalidad.ID_FUNCIONALIDAD FROM [3FG].ROLES  tablaRol, [3FG].FUNCIONALIDADES tablaFuncionalidad
WHERE tablaRol.NOMBRE_ROL = 'Afiliado' AND tablaFuncionalidad.NOMBRE IN ('Solicitar Turno', 'Cancelar turno','Comprar Bonos');
GO

INSERT INTO [3FG].FUNCIONALIDADES_ROL(ID_ROL, ID_FUNCIONALIDAD)
SELECT tablaRol.ID_ROL,tablaFuncionalidad.ID_FUNCIONALIDAD FROM [3FG].ROLES  tablaRol, [3FG].FUNCIONALIDADES tablaFuncionalidad
WHERE tablaRol.NOMBRE_ROL = 'Administrativo' AND tablaFuncionalidad.NOMBRE IN ('ABM de Rol', 'ABM de Afiliado', 'Comprar Bonos','Registrar Llegadas', 'Listados Estadisticos');
GO


/*

/*procedure para llenar la tabla afiliado*/
create procedure [3FG].asginarValoresALosAfiliados 
as
begin 
		DECLARE @idAfiliado bigint
		DECLARE @estadoCivil VARCHAR(20)
	DECLARE @cant_familiares bigint
	DECLARE @raizAfiliado bigint
	declare @numeroFamilia bigint
	

	DECLARE A1 CURSOR FOR

	SELECT ID_USUARIO,ESTADO_CIVIL,CANT_FAMILIARES,RAIZ_AFILIADO,NUMERO_FAMILIA FROM [3FG].AFILIADOS

	OPEN A1;
	
FETCH NEXT FROM A1 INTO @idAfiliado,@estadoCivil,@cant_familiares,@raizAfiliado,@numeroFamilia

WHILE @@FETCH_STATUS = 0

BEGIN

set @estadoCivil='SOLTERO'
set @cant_familiares=0

set @numeroFamilia=1
update [3FG].AFILIADOS  set  ESTADO_CIVIL=@estadoCivil,CANT_FAMILIARES=@cant_familiares,RAIZ_AFILIADO=@idAfiliado,NUMERO_FAMILIA=@numeroFamilia
where ID_USUARIO=@idAfiliado

FETCH NEXT FROM A1 INTO @idAfiliado,@estadoCivil,@cant_familiares,@raizAfiliado,@numeroFamilia

END

CLOSE A1;
DEALLOCATE A1;

end
GO
*/

/*Procedure que agrega los roles a los afilidos y profesionales*/
CREATE PROCEDURE [3FG].cargarRolesAAfiliadosYProfesionales
AS
BEGIN

INSERT INTO [3FG].ROLES_USUARIO(ID_USUARIO,ID_ROL)
SELECT ID_USUARIO,2
FROM [3FG].AFILIADOS

INSERT INTO [3FG].ROLES_USUARIO(ID_USUARIO,ID_ROL)
SELECT ID_USUARIO,3
FROM [3FG].PROFESIONALES

END;
GO

/*TRIGGER PARA LA MIGRACIÓN DE RECEPCIONES*/
CREATE TRIGGER [3FG].guardarNroConsulta
on [3FG].RECEPCIONES
AFTER INSERT
AS
BEGIN
DECLARE @ID_BONO BIGINT, @ID_AFILIADO BIGINT
DECLARE RECEPCIONCURSOR CURSOR FOR
SELECT ID_BONO, ID_AFILIADO FROM INSERTED I JOIN [3FG].TURNOS T ON (I.ID_TURNO = T.ID_TURNO)
ORDER BY FECHA_RECEPCIONES ASC

OPEN RECEPCIONCURSOR
FETCH NEXT FROM RECEPCIONCURSOR
INTO @ID_BONO, @ID_AFILIADO

WHILE (@@FETCH_STATUS = 0)
BEGIN

UPDATE [3FG].AFILIADOS
SET CANT_BONOS_UTILIZADOS += 1
WHERE ID_USUARIO = @ID_AFILIADO

UPDATE [3FG].BONOS
SET NUMERO_CONSULTA = (SELECT TOP 1 CANT_BONOS_UTILIZADOS FROM [3FG].AFILIADOS WHERE ID_USUARIO = @ID_AFILIADO)
WHERE ID_BONO = @ID_BONO

FETCH NEXT FROM RECEPCIONCURSOR
INTO @ID_BONO, @ID_AFILIADO

END

CLOSE RECEPCIONCURSOR
DEALLOCATE RECEPCIONCURSOR

END
GO

-- INICIO DE LA MIGRACION --

EXEC [3FG].MigrarAfiliadosAUsuarios
EXEC [3FG].MigrarAfiliados
EXEC [3FG].MigrarProfesionalesAUsuarios
EXEC [3FG].MigrarProfesionales
EXEC [3FG].MigrarAgenda
EXEC [3FG].MigrarPlanes
EXEC [3FG].MigrarTiposDeEspecialidad
EXEC [3FG].MigrarEspecialidades
EXEC [3FG].MigrarEspecialidadPorProfesional
EXEC [3FG].Migrar_Afiliados_Profesionales_Temporales
EXEC [3FG].MigrarTurnos
EXEC [3FG].MigrarCompras
EXEC [3FG].MigrarBonos
EXEC [3FG].MigrarRecepciones
EXEC [3FG].EsteblecerUsuarioYContraseñaPorDefault
--execute [3FG].asginarValoresALosAfiliados
EXEC [3FG].cargarRolesAAfiliadosYProfesionales
GO

-- FIN DE LA MIGRACION--

-- ELIMINO LAS TMP QUE UTILIZAMOS PARA LA MIGRACIÓN--
DROP TABLE dbo.#TMP_AFILIADOS;
GO
DROP TABLE dbo.#TMP_PROFESIONALES;
GO

-- ELIMINO EL TRIGGER UTILIZADO PARA LA MIGRACION
DROP TRIGGER [3FG].CargarAtencionDespuesDeLaRecepcionTrigger
GO


/*lista de cancelaciones*/

CREATE PROCEDURE [3FG].top5Cancelaciones @semetre integer,@anio integer 
AS
BEGIN

IF (@semetre = 1)
BEGIN
select top 5 count(e.ID_ESPECIALIDAD)'N° de cancelaciones' , e.DESCRIPCION_ESPECIALIDAD,[3FG].mes(MONTH(t.FECHA_TURNO))AS MES
 from [3FG].ESPECIALIDADES  e join [3FG].AGENDA a on
 (e.ID_ESPECIALIDAD=A.ID_ESPECIALIDAD) join [3FG].TURNOS t on
 (a.ID_AGENDA=t.ID_AGENDA) join [3FG].CANCELACIONES c on
 (t.ID_TURNO=c.ID_TURNO)
 where YEAR(t.FECHA_TURNO) =@anio and MONTH(t.FECHA_TURNO) >=1 and MONTH(t.FECHA_TURNO) <=6
 group by e.ID_ESPECIALIDAD,e.DESCRIPCION_ESPECIALIDAD,[3FG].mes(MONTH(t.FECHA_TURNO))
 order by 1 DESC

END
ELSE

BEGIN

select top 5 count(e.ID_ESPECIALIDAD)'N° de cancelaciones' , e.DESCRIPCION_ESPECIALIDAD,[3FG].mes(MONTH(t.FECHA_TURNO))AS MES
 from [3FG].ESPECIALIDADES  e join [3FG].AGENDA a on
 (e.ID_ESPECIALIDAD=A.ID_ESPECIALIDAD) join [3FG].TURNOS t on
 (a.ID_AGENDA=t.ID_AGENDA) join [3FG].CANCELACIONES c on
 (t.ID_TURNO=c.ID_TURNO)
 where YEAR(t.FECHA_TURNO) =@anio and MONTH(t.FECHA_TURNO) >=7 and MONTH(t.FECHA_TURNO) <=12
 group by e.ID_ESPECIALIDAD,e.DESCRIPCION_ESPECIALIDAD,[3FG].mes(MONTH(t.FECHA_TURNO))
 order by 1 DESC

END

END
GO



/*listado estadistico de profesionales mas consultados por plan y especialidad*/

CREATE PROCEDURE [3FG].top5ProfesionalesConsultadosPorPLan @descripcionPlan varchar(250),@descripcionEspecialidad varchar(250),@semetre integer,@anio integer 
AS
BEGIN
IF (@semetre = 1)
BEGIN
select top 5 U.NOMBRE,U.APELLIDO,[3FG].mes(MONTH(T.FECHA_TURNO))AS MES,PP.DESCRIPCION_PLAN as 'plan', count(A.ID_USUARIO)'Consultas Por Plan',E.DESCRIPCION_ESPECIALIDAD
 from [3FG].AGENDA A join
[3FG].ESPECIALIDADES E on(A.ID_ESPECIALIDAD=E.ID_ESPECIALIDAD) join [3FG].PROFESIONALES P
on(A.ID_USUARIO=P.ID_USUARIO) join [3FG].TURNOS T on(T.ID_AGENDA =A.ID_AGENDA) join 
[3FG].AFILIADOS AA on(T.ID_AFILIADO=AA.ID_USUARIO) join [3FG].PLANES PP on 
(PP.ID_PLAN=AA.ID_PLAN) join [3FG].USUARIOS U on (P.ID_USUARIO=U.ID_USUARIO)
 where PP.DESCRIPCION_PLAN=@descripcionPlan and E.DESCRIPCION_ESPECIALIDAD = @descripcionEspecialidad and
YEAR(T.FECHA_TURNO) =@anio and MONTH(T.FECHA_TURNO) >=1 and MONTH(T.FECHA_TURNO) <=6
group by E.DESCRIPCION_ESPECIALIDAD,PP.DESCRIPCION_PLAN,U.NOMBRE,U.APELLIDO,[3FG].mes(MONTH(T.FECHA_TURNO))
order by 4 DESC  
END
ELSE 
BEGIN
select top 5 U.NOMBRE,U.APELLIDO,[3FG].mes(MONTH(T.FECHA_TURNO))AS MES,PP.DESCRIPCION_PLAN as 'plan', count(A.ID_USUARIO)'Consultas Por Plan',E.DESCRIPCION_ESPECIALIDAD
 from [3FG].AGENDA A join
[3FG].ESPECIALIDADES E on(A.ID_ESPECIALIDAD=E.ID_ESPECIALIDAD) join [3FG].PROFESIONALES P
on(A.ID_USUARIO=P.ID_USUARIO) join [3FG].TURNOS T on(T.ID_AGENDA =A.ID_AGENDA) join 
[3FG].AFILIADOS AA on(T.ID_AFILIADO=AA.ID_USUARIO) join [3FG].PLANES PP on 
(PP.ID_PLAN=AA.ID_PLAN) join [3FG].USUARIOS U on (P.ID_USUARIO=U.ID_USUARIO)
 where PP.DESCRIPCION_PLAN=@descripcionPlan and
YEAR(T.FECHA_TURNO) =@anio and MONTH(T.FECHA_TURNO) >=1 and MONTH(T.FECHA_TURNO) <=6
group by E.DESCRIPCION_ESPECIALIDAD,PP.DESCRIPCION_PLAN,U.NOMBRE,U.APELLIDO,[3FG].mes(MONTH(T.FECHA_TURNO))
order by 4 DESC 


END
END
GO

/*lista de afiliados con mayor cantidad de bonos comprados*/


CREATE PROCEDURE [3FG].top5AfiliafsCompradoosConMasBonos @semetre integer,@anio integer 
AS
BEGIN
IF (@semetre = 1)
BEGIN
select U.NOMBRE as'Nombre',U.APELLIDO as'Apellido',[3FG].mes(MONTH(C.FECHA_COMPRA))AS MES,A.CANT_FAMILIARES as 'Cant. Familia' 
,sum(CANTIDAD_BONOS)'Cantidad Comprada' 
from [3FG].COMPRAS C join [3FG].AFILIADOS A
on(C.ID_USUARIO=A.ID_USUARIO) join [3FG].USUARIOS U on(A.ID_USUARIO=U.ID_USUARIO)
where YEAR(C.FECHA_COMPRA) =@anio and MONTH(C.FECHA_COMPRA) >=1 and MONTH(C.FECHA_COMPRA) <=6
group by U.NOMBRE,U.APELLIDO,A.CANT_FAMILIARES,[3FG].mes(MONTH(C.FECHA_COMPRA))
order by 4 DESC

END
ELSE 
BEGIN
select U.NOMBRE as'Nombre',U.APELLIDO as'Apellido',[3FG].mes(MONTH(C.FECHA_COMPRA))AS MES,A.CANT_FAMILIARES as 'Cant. Familia' 
,sum(CANTIDAD_BONOS)'Cantidad Comprada' 
from [3FG].COMPRAS C join [3FG].AFILIADOS A
on(C.ID_USUARIO=A.ID_USUARIO) join [3FG].USUARIOS U on(A.ID_USUARIO=U.ID_USUARIO)
where YEAR(C.FECHA_COMPRA) =@anio and MONTH(C.FECHA_COMPRA) >=7 and MONTH(C.FECHA_COMPRA) <=12
group by U.NOMBRE,U.APELLIDO,A.CANT_FAMILIARES,[3FG].mes(MONTH(C.FECHA_COMPRA))
order by 4 DESC

END
END
GO


/*lista especialiadades con mas Bono de Consulta Utilizado */
CREATE FUNCTION [3FG].mes(@numeroMes TINYINT)
RETURNS VARCHAR(20)
AS
BEGIN
	DECLARE @NOMBREMES VARCHAR(20);
	SET @NOMBREMES = (
	CASE @numeroMes
	WHEN 1 THEN 'ENERO'
	WHEN 2 THEN 'FEBRERO'
	WHEN 3 THEN 'MARZO'
	WHEN 4 THEN 'ABRIL'
	WHEN 5 THEN 'MAYO'
	WHEN 6 THEN 'JUNIO'
	WHEN 7 THEN 'JULIO'
	WHEN 8 THEN 'AGOSTO'
	WHEN 9 THEN 'SEPTIEMBRE'
	WHEN 10 THEN 'OCTUBRE'
	WHEN 11 THEN 'NOVIEMBRE'
	WHEN 12 THEN 'DICIEMBRE'
	END
	)
	RETURN @NOMBREMES;
END
GO



CREATE PROCEDURE [3FG].top5EspecialidadesConMasBonosUtilizados(@SEMESTRE TINYINT,@ANIO SMALLINT)
AS
BEGIN
	
	IF(@SEMESTRE = 1)
	BEGIN
	SELECT DESCRIPCION_ESPECIALIDAD, [3FG].mes(MONTH(T.FECHA_TURNO)) AS MES, COUNT(*) AS CANTIDAD_BONOS_UTILIZADOS
	FROM [3FG].RECEPCIONES R JOIN [3FG].TURNOS T
	ON (R.ID_TURNO = T.ID_TURNO) JOIN [3FG].AGENDA A
	ON (T.ID_AGENDA = A.ID_AGENDA) JOIN [3FG].ESPECIALIDADES E
	ON (A.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD)
	WHERE DESCRIPCION_ESPECIALIDAD IN
	(
	SELECT DESCRIPCION_ESPECIALIDAD
	FROM (SELECT TOP 5 DESCRIPCION_ESPECIALIDAD, COUNT(*) AS CANTIDAD_BONOS_UTILIZADOS
	FROM [3FG].RECEPCIONES R JOIN [3FG].TURNOS T
	ON (R.ID_TURNO = T.ID_TURNO) JOIN [3FG].AGENDA A
	ON (T.ID_AGENDA = A.ID_AGENDA) JOIN [3FG].ESPECIALIDADES E
	ON (A.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD)
	GROUP BY DESCRIPCION_ESPECIALIDAD
	ORDER BY 2 DESC) AS ESPECIALIDADES_CON_MAS_BONOS
	)
	AND YEAR(T.FECHA_TURNO) = @ANIO
	AND MONTH(T.FECHA_TURNO) BETWEEN 1 AND 6
	GROUP BY DESCRIPCION_ESPECIALIDAD, MONTH(T.FECHA_TURNO), YEAR(T.FECHA_TURNO)
	ORDER BY 3 DESC
	END
		ELSE
	BEGIN
	SELECT DESCRIPCION_ESPECIALIDAD, [3FG].mes(MONTH(T.FECHA_TURNO)) AS MES, COUNT(*) AS CANTIDAD_BONOS_UTILIZADOS
	FROM [3FG].RECEPCIONES R JOIN [3FG].TURNOS T
	ON (R.ID_TURNO = T.ID_TURNO) JOIN [3FG].AGENDA A
	ON (T.ID_AGENDA = A.ID_AGENDA) JOIN [3FG].ESPECIALIDADES E
	ON (A.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD)
	WHERE DESCRIPCION_ESPECIALIDAD IN
	(
	SELECT DESCRIPCION_ESPECIALIDAD
	FROM (SELECT TOP 5 DESCRIPCION_ESPECIALIDAD, COUNT(*) AS CANTIDAD_BONOS_UTILIZADOS
	FROM [3FG].RECEPCIONES R JOIN [3FG].TURNOS T
	ON (R.ID_TURNO = T.ID_TURNO) JOIN [3FG].AGENDA A
	ON (T.ID_AGENDA = A.ID_AGENDA) JOIN [3FG].ESPECIALIDADES E
	ON (A.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD)
	GROUP BY DESCRIPCION_ESPECIALIDAD
	ORDER BY 2 DESC) AS ESPECIALIDADES_CON_MAS_BONOS
	)
	AND YEAR(T.FECHA_TURNO) = @ANIO
	AND MONTH(T.FECHA_TURNO) BETWEEN 7 AND 12
	GROUP BY DESCRIPCION_ESPECIALIDAD, MONTH(T.FECHA_TURNO), YEAR(T.FECHA_TURNO)
	ORDER BY 3 DESC
	END

END
GO

/*top 5 profesionales Con Menos Horas Trabajadas*/

CREATE PROCEDURE [3FG].top5ProfesionalesConMenosHorasTrabajadas(@DESCRIPCION_PLAN VARCHAR(250), @DESCRIPCION_ESPECIALIDAD VARCHAR(250), @SEMESTRE TINYINT, @ANIO SMALLINT)
AS
BEGIN
	
	IF(@SEMESTRE = 1)
	BEGIN
	SELECT A.ID_USUARIO,[3FG].mes(MONTH(T.FECHA_TURNO))AS MES, DESCRIPCION_ESPECIALIDAD, DESCRIPCION_PLAN, COUNT(*)/2 AS CANTIDAD_HORAS_TRABAJADAS, [3FG].mes(MONTH(T.FECHA_TURNO)) AS MES
	FROM [3FG].ATENCIONES_MEDICAS AM JOIN [3FG].RECEPCIONES R
	ON (AM.ID_RECEPCION = R.ID_RECEPCION) JOIN [3FG].TURNOS T
	ON (R.ID_TURNO = T.ID_TURNO) JOIN [3FG].AGENDA A
	ON (T.ID_AGENDA = A.ID_AGENDA) JOIN [3FG].ESPECIALIDADES E
	ON (A.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD) JOIN [3FG].AFILIADOS AF
	ON (T.ID_AFILIADO = AF.ID_USUARIO) JOIN [3FG].PLANES P
	ON (AF.ID_PLAN = P.ID_PLAN)
	WHERE A.ID_USUARIO IN (
	SELECT ID_USUARIO
	FROM (SELECT TOP 5 A.ID_USUARIO, COUNT(*)/2 AS CANTIDAD_HORAS_TRABAJADAS
	FROM [3FG].ATENCIONES_MEDICAS AM JOIN [3FG].RECEPCIONES R
	ON (AM.ID_RECEPCION = R.ID_RECEPCION) JOIN [3FG].TURNOS T
	ON (R.ID_TURNO = T.ID_TURNO) JOIN [3FG].AGENDA A
	ON (T.ID_AGENDA = A.ID_AGENDA)
	GROUP BY A.ID_USUARIO
	ORDER BY 2 ASC) AS USUARIOS_CON_MENOS_HORAS_TRABAJADAS
	)
	AND YEAR(T.FECHA_TURNO) = @ANIO
	AND MONTH(T.FECHA_TURNO) BETWEEN 1 AND 6
	AND DESCRIPCION_ESPECIALIDAD = @DESCRIPCION_ESPECIALIDAD
	AND DESCRIPCION_PLAN = @DESCRIPCION_PLAN
	GROUP BY A.ID_USUARIO, MONTH(T.FECHA_TURNO), YEAR(T.FECHA_TURNO), DESCRIPCION_ESPECIALIDAD, DESCRIPCION_PLAN
	ORDER BY 1 ASC
	END
		ELSE
	BEGIN
	SELECT A.ID_USUARIO, [3FG].mes(MONTH(T.FECHA_TURNO))AS MES,DESCRIPCION_ESPECIALIDAD, DESCRIPCION_PLAN, COUNT(*)/2 AS CANTIDAD_HORAS_TRABAJADAS, [3FG].mes(MONTH(T.FECHA_TURNO)) AS MES
	FROM [3FG].ATENCIONES_MEDICAS AM JOIN [3FG].RECEPCIONES R
	ON (AM.ID_RECEPCION = R.ID_RECEPCION) JOIN [3FG].TURNOS T
	ON (R.ID_TURNO = T.ID_TURNO) JOIN [3FG].AGENDA A
	ON (T.ID_AGENDA = A.ID_AGENDA) JOIN [3FG].ESPECIALIDADES E
	ON (A.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD) JOIN [3FG].AFILIADOS AF
	ON (T.ID_AFILIADO = AF.ID_USUARIO) JOIN [3FG].PLANES P
	ON (AF.ID_PLAN = P.ID_PLAN)
	WHERE A.ID_USUARIO IN (
	SELECT ID_USUARIO
	FROM (SELECT TOP 5 A.ID_USUARIO, COUNT(*)/2 AS CANTIDAD_HORAS_TRABAJADAS
	FROM [3FG].ATENCIONES_MEDICAS AM JOIN [3FG].RECEPCIONES R
	ON (AM.ID_RECEPCION = R.ID_RECEPCION) JOIN [3FG].TURNOS T
	ON (R.ID_TURNO = T.ID_TURNO) JOIN [3FG].AGENDA A
	ON (T.ID_AGENDA = A.ID_AGENDA)
	GROUP BY A.ID_USUARIO
	ORDER BY 2 ASC) AS USUARIOS_CON_MENOS_HORAS_TRABAJADAS
	)
	AND YEAR(T.FECHA_TURNO) = @ANIO
	AND MONTH(T.FECHA_TURNO) BETWEEN 7 AND 12
	AND DESCRIPCION_ESPECIALIDAD = @DESCRIPCION_ESPECIALIDAD
	AND DESCRIPCION_PLAN = @DESCRIPCION_PLAN
	GROUP BY A.ID_USUARIO, MONTH(T.FECHA_TURNO), YEAR(T.FECHA_TURNO), DESCRIPCION_ESPECIALIDAD, DESCRIPCION_PLAN
	ORDER BY 1 ASC
	END

END
GO

			/* USUARIOS DE PRUEBA */

/*ADMINISTRADOR*/
INSERT INTO [3FG].ROLES_USUARIO(ID_USUARIO,ID_ROL)
VALUES(1,1)
/*
/*AFILIADO*/
INSERT INTO [3FG].ROLES_USUARIO(ID_USUARIO,ID_ROL)
VALUES(7,2)

/*PROFESIONAL*/
INSERT INTO [3FG].ROLES_USUARIO(ID_USUARIO,ID_ROL)
VALUES(5575,3)
*/

		/* DATOS PARA TEST DE CANCELACIONES */

INSERT INTO [3FG].TURNOS(ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(7,191,'20161203 10:00:00 AM')

INSERT INTO [3FG].TURNOS(ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(7,191,'20161203 11:00:00 AM')

INSERT INTO [3FG].TURNOS(ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(7,191,'20161203 12:30:00 PM')




		/* DATOS PARA TEST DE PEDIDO DE TURNOS */

SET IDENTITY_INSERT [3FG].USUARIOS ON

INSERT INTO [3FG].USUARIOS(ID_USUARIO,NOMBRE,APELLIDO,USUARIO_NOMBRE)
VALUES(99999,'De Test','Profesional','test')

INSERT INTO [3FG].PROFESIONALES(ID_USUARIO,INICIO_DISPONIBILIDAD,FIN_DISPONIBILIDAD)
VALUES(99999,'20100101 08:00:00 AM','20160615 06:00:00 PM')

INSERT INTO [3FG].ESPECIALIDAD_PROFESIONAL(ID_USUARIO,ID_ESPECIALIDAD)
VALUES(99999,9999)

INSERT INTO [3FG].AGENDA(ID_USUARIO,ID_ESPECIALIDAD,DIA_ATENCION,INICIO_ATENCION,FIN_ATENCION)
VALUES(99999,9999,'Lunes','08:00:00 AM','06:00:00 PM')
INSERT INTO [3FG].AGENDA(ID_USUARIO,ID_ESPECIALIDAD,DIA_ATENCION,INICIO_ATENCION,FIN_ATENCION)
VALUES(99999,9999,'Miercoles','08:00:00 AM','06:00:00 PM')
INSERT INTO [3FG].AGENDA(ID_USUARIO,ID_ESPECIALIDAD,DIA_ATENCION,INICIO_ATENCION,FIN_ATENCION)
VALUES(99999,9999,'Viernes','08:00:00 AM','06:00:00 PM')

INSERT INTO [3FG].USUARIOS(ID_USUARIO,NOMBRE,APELLIDO,USUARIO_NOMBRE,CONTRASEÑA)
VALUES(100000,'Probando','Todo','probando',(SELECT SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA2_256','1234')),3,250) ))

INSERT INTO [3FG].PROFESIONALES(ID_USUARIO)
VALUES(100000)

INSERT INTO [3FG].ESPECIALIDAD_PROFESIONAL(ID_USUARIO,ID_ESPECIALIDAD)
VALUES(100000,10037)

INSERT INTO [3FG].ESPECIALIDAD_PROFESIONAL(ID_USUARIO,ID_ESPECIALIDAD)
VALUES(100000,9999)

INSERT INTO [3FG].ROLES_USUARIO(ID_USUARIO,ID_ROL)
VALUES(100000,3)


SET IDENTITY_INSERT [3FG].USUARIOS OFF

	


		/* DATOS PARA TEST DE REGISTRO DE CONSULTAS*/

SET IDENTITY_INSERT [3FG].TURNOS ON

INSERT INTO [3FG].TURNOS(ID_TURNO,ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(1111222,8,191,'20160101 11:00:00 AM')

INSERT INTO [3FG].TURNOS(ID_TURNO,ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(1111333,9,191,'20160101 09:00:00 AM')

INSERT INTO [3FG].TURNOS(ID_TURNO,ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(1111444,10,191,'20160101 11:30:00 AM')

INSERT INTO [3FG].TURNOS(ID_TURNO,ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(1111555,2,191,'20151230 11:30:00 PM')

INSERT INTO [3FG].TURNOS(ID_TURNO,ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(1111666,2,191,'20151230 09:00:00 PM')

INSERT INTO [3FG].TURNOS(ID_TURNO,ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(1111777,3,1,'20151230 11:30:00 PM')

INSERT INTO [3FG].TURNOS(ID_TURNO,ID_AFILIADO,ID_AGENDA,FECHA_TURNO)
VALUES(1111888,4,1,'20151230 05:00:00 PM')

INSERT INTO [3FG].RECEPCIONES(ID_TURNO)
VALUES(1111222)

INSERT INTO [3FG].RECEPCIONES(ID_TURNO)
VALUES(1111333)

INSERT INTO [3FG].RECEPCIONES(ID_TURNO)
VALUES(1111444)

SET IDENTITY_INSERT [3FG].TURNOS OFF

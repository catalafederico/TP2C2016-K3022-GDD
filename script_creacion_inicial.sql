 /*CREO NUEVO ESQUEMA */
CREATE SCHEMA [3FG] AUTHORIZATION [gd];
GO
/*CREO LAS TABLAS DEL DER*/

/*CREO LA TABLA DE USUARIOS */
CREATE TABLE [3FG].USUARIOS (
	ID_USUARIO BIGINT IDENTITY(1,1) PRIMARY KEY,
	USUARIO_NOMBRE VARCHAR(250) UNIQUE,
	PASSWORD VARCHAR(250),
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
CREATE TABLE [3FG].ROLES_USUARIOS (
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
	CANT_FAMILIARES BIGINT,
	RAIZ_AFILIADO BIGINT,
	NUMERO_FAMILIA BIGINT
);
GO
/*CREO LA TABLA DE PROFESIONALES */
CREATE TABLE [3FG].PROFESIONALES (
	ID_USUARIO BIGINT PRIMARY KEY,
	MATRICULA BIGINT UNIQUE,
	INICIO_DISPONIBILIDAD DATETIME,
	FIN_DISPONIBILIDAD DATETIME
);
GO
/*CREO LA TABLA DE HISTORIAL_CAMBIOS_PLAN*/
CREATE TABLE [3FG].HISTORIAL_CAMBIOS_PLAN (
	ID_CAMBIO BIGINT IDENTITY(1,1) PRIMARY KEY,
	MOTIVO_CAMBIO_PLAN VARCHAR(250),
	FECHA_MODIFICACION DATETIME
);
GO
/*CREO LA TABLA DE ESPECIALIDADES*/
CREATE TABLE [3FG].ESPECIALIDADES (
	ID_ESPECIALIDAD BIGINT PRIMARY KEY,
	ID_TIPO_ESPECIALIDAD BIGINT,
	DESCRIPCION_ESPECIALIDAD VARCHAR(250),
	--CODIGO_ESPECIALIDAD BIGINT
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
	DIA_ATENCION VARCHAR(20),
	INICIO_ATENCION TIME,
	FIN_ATENCION TIME
);
GO
/*CREO LA TABLA DE TIPO_ESPECIALIDAD */
CREATE TABLE [3FG].TIPO_ESPECIALIDAD (
	ID_TIPO_ESPECIALIDAD BIGINT PRIMARY KEY,
	DESCRIPCION_TIPO_ESPECIALIDAD VARCHAR(250),
	--CODIGO_TIPO_ESPECIALIDAD BIGINT
);
GO
/*CREO LA TABLA DE PLANES*/
CREATE TABLE [3FG].PLANES (
	ID_PLAN BIGINT IDENTITY(1,1) PRIMARY KEY,
	CODIGO_PLAN BIGINT,
	DESCRIPCION_PLAN VARCHAR(250),
	PRECIO_BONO_CONSULTA BIGINT,
	PRECIO_BONO_FARMACIA BIGINT
);
GO
/*CREO LA TABLA DE LOS BONOS*/
CREATE TABLE [3FG].BONOS (
	ID_BONO BIGINT IDENTITY(46494,1) PRIMARY KEY,
	ID_PLAN BIGINT,
	ID_USUARIO BIGINT,
	ID_COMPRA BIGINT,
	NUMERO_CONSULTA BIGINT,
	FECHA_IMPRESION DATETIME
);
GO
/*CREO LA TABLA DE COMPRAS*/
CREATE TABLE [3FG].COMPRAS (
	ID_COMPRAS BIGINT IDENTITY(1,1) PRIMARY KEY,
	CANTIDAD_BONOS BIGINT,
	MONTO_PAGADO BIGINT
);
GO
/*CREO LA TABLA DE TURNOS*/
CREATE TABLE [3FG].TURNOS (
	ID_TURNO BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_AFILIADO BIGINT,
	ID_PROFESIONAL BIGINT,
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
	primary key clustered (ID_USUARIO,ID_PROFESIONAL);
GO

/*CREO LA FOREIGN KEY COMPUESTA*/ --NO SE SI ESTO ESTA BIEN--
alter table [3FG].AGENDA add constraint PK_AGENDA_ESPECIALIDAD_PROFESIONAL
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
alter table [3FG].AFILIADO add constraint FK_AFILIADO_PLAN
	foreign key (ID_PLAN) references [3FG].PLANES (ID_PLAN);
GO
alter table [3FG].HISTORIAL_CAMBIOS_PLAN add constraint FK_AFILIADO_PLAN
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
alter table [3FG].BONOS add constraint FK_BONO_AFILIADO
	foreign key (ID_USUARIO) references [3FG].AFILIADOS (ID_USUARIO);
GO
alter table [3FG].BONOS add constraint FK_BONO_PLAN
	foreign key (ID_PLAN) references [3FG].PLANES (ID_PLAN);
GO
alter table [3FG].BONOS add constraint FK_BONO_COMPRAS
	foreign key (ID_COMPRA) references [3FG].COMPRAS (ID_COMPRA);
GO
alter table [3FG].TURNOS add constraint FK_TURNO_AFILIADO
	foreign key (ID_USUARIO) references [3FG].AFILIADOS (ID_USUARIO);
GO
alter table [3FG].TURNOS add constraint FK_TURNO_PROFESIONAL
	foreign key (ID_USUARIO) references [3FG].PROFESIONALES (ID_USUARIO);
GO
alter table [3FG].RECEPCIONES add constraint FK_RECEPCIONES_TURNO
	foreign key (ID_TURNO) references [3FG].TURNOS (ID_TURNO);
GO
alter table [3FG].RECEPCIONES add constraint FK_RECEPCIONES_BONO
	foreign key (ID_BONO) references [3FG].BONOS (ID_BONOS);
GO
alter table [3FG].ATENCIONES_MEDICAS add constraint FK_ATENCIONES_MEDICAS_RECEPCION
	foreign key (ID_RECEPCION) references [3FG].RECEPCIONES (ID_RECEPCIONES);
GO
alter table [3FG].CANCELACIONES add constraint FK_CANCELACIONES_TURNO
	foreign key (ID_TURNO) references [3FG].TURNOS (ID_TURNO);
GO

/* -- Migracion-- */
	
	CREATE PROCEDURE [3FG].MigrarPacientes
AS
BEGIN

	--Se migran los pacientes de la tabla Maestra
	INSERT INTO [3FG].USUARIOS(NOMBRE,APELLIDO,NUMERO_DOCUMENTO,DIRECCION,TELEFONO,MAIL,FECHA_NACIMIENTO)
	SELECT DISTINCT Paciente_Nombre,Paciente_Apellido,Paciente_Dni,Paciente_Direccion,Paciente_Telefono,Paciente_Mail,Paciente_Fecha_Nac 
	FROM gd_esquema.Maestra

END;
GO

CREATE PROCEDURE [3FG].MigrarProfesionales
AS
BEGIN

	--Se migran los profesionales de la tabla Maestra
	INSERT INTO [3FG].USUARIOS(NOMBRE,APELLIDO,NUMERO_DOCUMENTO,DIRECCION,TELEFONO,MAIL,FECHA_NACIMIENTO)
	SELECT DISTINCT Medico_Nombre,Medico_Apellido,Medico_Dni,Medico_Direccion,Medico_Telefono,Medico_Mail,Medico_Fecha_Nac 
	FROM gd_esquema.Maestra

END;
GO

CREATE PROCEDURE [3FG].MigrarPlanes
AS
BEGIN

	--Se migran los planes de la tabla Maestra
	INSERT INTO [3FG].PLANES(CODIGO_PLAN,DESCRIPCION_PLAN,PRECIO_BONO_CONSULTA,PRECIO_BONO_FARMACIA)
	SELECT DISTINCT Plan_Med_Codigo,Plan_Med_Descripcion,Plan_Med_Precio_Bono_Consulta,Plan_Med_Precio_Bono_Farmacia
	FROM gd_esquema.Maestra

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
	SELECT DISTINCT Especialidad_Codigo,Tipo_Especialidad_Codigo,Tipo_Especialidad_Descripcion
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

/*VER SI ESTO ESTA BIEN*/
CREATE PROCEDURE [3FG].MigrarBonos
AS
BEGIN

	--Se migran los bonos de la tabla Maestra
	INSERT INTO [3FG].BONOS(ID_PLAN,ID_USUARIO)
	SELECT p.ID_PLAN,u.ID_USUARIO
	FROM gd_esquema.Maestra m, PLANES p, USUARIOS u
	WHERE m.Paciente_Dni = u.NUMERO_DOCUMENTO
	AND m.Plan_Med_Codigo=p.CODIGO_PLAN
	AND m.Bono_Consulta_Numero=m.Bono_Consulta_Fecha_Impresion
	AND m.Bono_Consulta_Numero is NOT NULL
	ORDER BY Bono_Consulta_Numero asc
END;
GO

/*
CREATE PROCEDURE [3FG].CargarCompras
AS
BEGIN

	--Se cargan las compras luego de migrar los bonos
	INSERT INTO [3FG].COMPRAS(CANTIDAD_BONOS,MONTO_PAGADO)
	SELECT COUNT(Bono_Consulta_Numero),(p.PRECIO_BONO_CONSULTA*COUNT(Bono_Consulta_Numero))
	FROM gd_esquema.Maestra m, PLANES p
	WHERE m.Plan_Med_Codigo=p.CODIGO_PLAN
	AND m.Compra_Bono_Fecha=m.Bono_Consulta_Fecha_Impresion
	AND m.Compra_Bono_Fecha is NOT NULL
END;
GO
*/


/*Tablas temporales para afiliados y medicos*/

CREATE TABLE [3FG].#TMP_AFILIADOS (
	ID_AFILIADO BIGINT,
	NOMBRE VARCHAR(100),
	APELLIDO VARCHAR(100),
	NUMERO_DOCUMENTO BIGINT
)
GO

CREATE TABLE [3FG].#TMP_PROFESIONALES (
	ID_PROFESIONAL BIGINT,
	NOMBRE VARCHAR(100),
	APELLIDO VARCHAR(100),
	NUMERO_DOCUMENTO BIGINT
)
GO

CREATE PROCEDURE [3FG].Migrar_Afiliados_Profesionales_Temporales
AS
BEGIN

	--Se migran los AFILIADOS de la usuarios a la temporal
	INSERT INTO #TMP_AFILIADOS (ID_AFILIADO,NOMBRE,APELLIDO,NUMERO_DOCUMENTO)
	SELECT DISTINCT u.ID_USUARIO, m.Paciente_Nombre, m.Paciente_Apellido, m.Paciente_Dni
	FROM USUARIOS u JOIN gd_esquema.Maestra m ON (u.NUMERO_DOCUMENTO = m.Paciente_Dni)
	
	--Se migran los PROFESIONES de la usuarios a la temporal
	INSERT INTO #TMP_PROFESIONALES (ID_PROFESIONAL,NOMBRE,APELLIDO,NUMERO_DOCUMENTO)
	SELECT DISTINCT u.ID_USUARIO, m.Medico_Nombre, m.Medico_Apellido, m.Medico_Dni
	FROM USUARIOS u JOIN gd_esquema.Maestra m ON (u.NUMERO_DOCUMENTO = m.Medico_Dni) 

END;
GO

CREATE PROCEDURE [3FG].MigrarTurnos
AS
BEGIN

	--Se migran los turnos de la tabla Maestra
	INSERT INTO [3FG].TURNOS(ID_AFILIADO,ID_PROFESIONAL,FECHA_TURNO,FECHA_TURNO)
	SELECT a.ID_AFILIADO,p.ID_PROFESIONAL,m.Turno_Fecha,m.Turno_Fecha
	FROM gd_esquema.Maestra m, #TMP_AFILIADOS a, #TMP_PROFESIONALES p
	WHERE m.Paciente_Dni = a.ID_AFILIADO
	AND m.Medico_Dni = p.ID_PROFESIONAL
	AND m.Compra_Bono_Fecha is NULL
	AND m.Bono_Consulta_Fecha_Impresion is NULL
	AND m.Consulta_Sintomas is NULL
	AND m.Consulta_Enfermedades is NULL

END;
GO

/* -- Inserto los ROLES -- */

INSERT INTO [3FG].ROLES(NOMBRE_ROL) VALUES('Administrativo');
INSERT INTO [3FG].ROLES(NOMBRE_ROL) VALUES('Afiliado');
INSERT INTO [3FG].ROLES(NOMBRE_ROL) VALUES('Profesional');
INSERT INTO [3FG].ROLES(NOMBRE_ROL) VALUES('Administrador general'); /* Es quien va a tener
																	todas las funcionalidades*/



 /*CREO NUEVO ESQUEMA */
CREATE SCHEMA [3FG] AUTHORIZATION [gd];
GO
/*CREO LAS TABLAS DEL DER*/

/*CREO LA TABLA DE USUARIOS */
CREATE TABLE 3FG.USUARIOS (
	ID_USUARIO BIGINT IDENTITY(1,1) PRIMARY KEY,
	USUARIO_NOMBRE VARCHAR(250) UNIQUE,
	PASSWORD VARCHAR(250),
	CANT_INTENTOS TINYINT DEFAULT 0,
	NOMBRE VARCHAR(100),
	APELLIDO VARCHAR(100),
	TIPO_DE_DOCUMENTO VARCHAR(100),
	NUMERO_DOCUMENTO BIGINT,
	TELEFONO BIGINT,
	MAIL VARCHAR(100),
	FECHA_NACIMIENTO DATETIME,
	SEXO VARCHAR(20),
	HABILITADO TINYINT DEFAULT 1
);
GO
/*CREO LA TABLA DE ROLES */
CREATE TABLE 3FG.ROLES (
	ID_ROL BIGINT IDENTITY(1,1) PRIMARY KEY,
	ROL VARCHAR(100) UNIQUE,
	HABILITADO TINYINT DEFAULT 1
);
GO
/*CREO LA TABLA DE ROLES POR USUARIO*/
CREATE TABLE 3FG.ROLES_USUARIOS (
	ID_USUARIO BIGINT NOT NULL,
	ID_ROL BIGINT NOT NULL,
);
GO
/*CREO LA TABLA DE FUNCIONALIDADES*/
CREATE TABLE 3FG.FUNCIONALIDADES (
	ID_FUNCIONALIDAD BIGINT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE VARCHAR(100) UNIQUE,
);
GO
/*CREO LA TABLA DE FUNCIONALIDADES POR ROL*/
CREATE TABLE 3FG.FUNCIONALIDADES_ROL (
	ID_ROL BIGINT NOT NULL,
	ID_FUNCIONALIDAD BIGINT NOT NULL,
);
GO
/*CREO LA TABLA DE AFILIADOS */
CREATE TABLE 3FG.AFILIADOS (
	ID_USUARIO BIGINT PRIMARY KEY,
	ID_PLAN BIGINT,
	ESTADO_CIVIL VARCHAR(20),
	CANT_FAMILIARES BIGINT,
	RAIZ_AFILIADO BIGINT,
	NUMERO_FAMILIA BIGINT
);
GO
/*CREO LA TABLA DE PROFESIONALES */
CREATE TABLE 3FG.PROFESIONALES (
	ID_USUARIO BIGINT PRIMARY KEY,
	MATRICULA BIGINT UNIQUE,
	INICIO_DISPONIBILIDAD DATETIME,
	FIN_DISPONIBILIDAD DATETIME
);
GO
/*CREO LA TABLA DE HISTORIAL_CAMBIOS_PLAN*/
CREATE TABLE 3FG.HISTORIAL_CAMBIOS_PLAN (
	ID_CAMBIO BIGINT IDENTITY(1,1) PRIMARY KEY,
	MOTIVO_CAMBIO_PLAN VARCHAR(250),
	FECHA_MODIFICACION DATETIME
);
GO
/*CREO LA TABLA DE ESPECIALIDADES*/
CREATE TABLE 3FG.ESPECIALIDADES (
	ID_ESPECIALIDAD BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_TIPO_ESPECIALIDAD BIGINT,
	DESCRIPCION_ESPECIALIDAD VARCHAR(250),
	CODIGO_ESPECIALIDAD BIGINT
);
GO
/*CREO LA TABLA DE ESPECIALIDAD_PROFESIONAL*/
CREATE TABLE 3FG.ESPECIALIDAD_PROFESIONAL (
	ID_USUARIO BIGINT PRIMARY KEY,
	ID_ESPECIALIDAD BIGINT PRIMARY KEY
);
GO
/*CREO LA TABLA DE AGENDA*/
CREATE TABLE 3FG.AGENDA (
	ID_AGENDA BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_USUARIO BIGINT,
	DIA_ATENCION VARCHAR(20),
	INICIO_ATENCION TIME,
	FIN_ATENCION TIME
);
GO
/*CREO LA TABLA DE TIPO_ESPECIALIDAD */
CREATE TABLE 3FG.TIPO_ESPECIALIDAD (
	ID_TIPO_ESPECIALIDAD BIGINT IDENTITY(1,1) PRIMARY KEY,
	DESCRIPCION_TIPO_ESPECIALIDAD VARCHAR(250),
	CODIGO_TIPO_ESPECIALIDAD BIGINT
);
GO
/*CREO LA TABLA DE PLANES*/
CREATE TABLE 3FG.PLANES (
	ID_PLAN BIGINT IDENTITY(1,1) PRIMARY KEY,
	CODIGO_PLAN BIGINT,
	DESCRIPCION_PLAN VARCHAR(250),
	PRECIO_BONO_CONSULTA BIGINT,
	PRECIO_BONO_FARMACIA BIGINT
);
GO
/*CREO LA TABLA DE LOS BONOS*/
CREATE TABLE 3FG.BONOS (
	ID_BONO BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_PLAN BIGINT,
	ID_AFILIADO BIGINT,
	ID_COMPRA BIGINT,
	NUMERO_CONSULTA BIGINT,
	FECHA_IMPRESION DATETIME
);
GO
/*CREO LA TABLA DE COMPRAS*/
CREATE TABLE 3FG.COMPRAS (
	ID_COMPRAS BIGINT IDENTITY(1,1) PRIMARY KEY,
	CANTIDAD BIGINT,
	MONTO_PAGADO BIGINT
);
GO
/*CREO LA TABLA DE TURNOS*/
CREATE TABLE 3FG.TURNOS (
	ID_TURNO BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_AFILIADO BIGINT,
	ID_PROFESIONAL BIGINT,
	FECHA_TURNO DATETIME
);
GO
/*CREO LA TABLA DE RECEPCIONES*/
CREATE TABLE 3FG.RECEPCIONES (
	ID_RECEPCION BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_TURNO BIGINT,
	ID_BONO BIGINT,
	FECHA_RECEPCIONES DATETIME
);
GO
/*CREO LA TABLA DE ATENCIONES_MEDICAS*/
CREATE TABLE 3FG.ATENCIONES_MEDICAS (
	ID_ATENCION BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_RECEPCION BIGINT,
	FECHA_ATENCION DATETIME,
	DIAGNOSTICO VARCHAR(250),
	SINTOMAS VARCHAR(250)
);
GO
/*CREO LA TABLA DE CANCELACIONES*/
CREATE TABLE 3FG.CANCELACIONES (
	ID_CANCELACION BIGINT IDENTITY(1,1) PRIMARY KEY,
	ID_TURNO BIGINT,
	TIPO_CANCELACION VARCHAR(20),
	MOTIVO_CANCELACION VARCHAR(250)
);
GO

create database practica;

use practica;
go

create table persona
(
	cedula int not null primary key,
	nombre varchar(255) NOT NULL,
	fechaNac varchar(8) NOT NULL,
	sexo char(1) NOT NULL,
	profesion varchar(255) NOT NULL,
	telefono int null,
	foto Image null
);

use practica;
go

CREATE PROCEDURE insertarPersona
    @nombre varchar(255),   
    @fechNac varchar(8),
	@sexo char(1),
	@profesion varchar(255),
	@cedula int,
	@telefono int,
	@foto Image
	   
AS   
	INSERT INTO persona(cedula, nombre, fechaNac, sexo, profesion, telefono, foto)
	values (@cedula, @nombre, @fechNac, @sexo, @profesion, @telefono, @foto);

GO  

use practica;
go
select * from persona;

go
use practica;
go
CREATE PROCEDURE buscarPersona
 	@cedula int
	   
AS   
	select * from persona where cedula = @cedula;

GO  

use practica;
go
CREATE PROCEDURE insertarPersona
    @nombre varchar(255),   
    @fechNac varchar(8),
	@sexo char(1),
	@profesion varchar(255),
	@cedula int,
	@telefono int,
	@foto Image
	   
AS   
	INSERT INTO persona(cedula, nombre, fechaNac, sexo, profesion, telefono, foto)
	values (@cedula, @nombre, @fechNac, @sexo, @profesion, @telefono, @foto);

GO

use practica;
go
create PROCEDURE filtrarPersonas
    @filtro varchar(255)
	   
AS   
	select * from persona where nombre like '%%'+@filtro+'%%' or nombre LIKE ''+@filtro+'%%'
	or  nombre like '%%'+@filtro+'' or nombre like ''+@filtro+'';

GO
 
use practica;
go
create procedure actualizarPersona
	@nombre varchar(255),   
    @fechNac varchar(8),
	@sexo char(1),
	@profesion varchar(255),
	@cedula int,
	@telefono int,
	@foto Image 
	AS   
	UPDATE persona
	set nombre=@nombre, fechaNac=@fechNac, sexo=@sexo, profesion=@profesion, telefono=@telefono, foto=@foto
	where cedula = @cedula;
GO

use practica;
go
create procedure existePersona
	@cedula int,
	@contador int output
	AS   
	select @contador = COUNT(*) from persona
	where cedula = @cedula;
	return;
GO

use practica;
go
create procedure eliminarPersona
	@cedula int
	AS   
	delete from persona
	where cedula = @cedula;
GO
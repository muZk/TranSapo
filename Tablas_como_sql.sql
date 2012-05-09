CREATE TABLE cuenta
(
Email varchar(60) PRIMARY KEY, 
Facebook text,
Twitter text,
Nombre text NOT NULL,
Password varchar(10) NOT NULL,
);
GO

Create table paradero
(
Codigo varchar(10) Primary Key
);
GO

Create table recorridos_paradero(

Paradero varchar(10) NOT NULL,
Recorrido varchar(8) NOT NULL,
Constraint pk_recorridos_paradero Primary key (Paradero,Recorrido),
Constraint fk_recorridos_paradero_paradero FOREIGN KEY(Paradero) REFERENCES paradero(Codigo)
);
GO

Create table calles_paradero(
Paradero varchar(10) NOT NULL,
Calle varchar(50) NOT NULL,
Constraint pk_calles_paradero Primary key (Paradero,Calle),
Constraint fk_calles_paradero_paradero FOREIGN KEY(Paradero) REFERENCES paradero(Codigo)
);
GO

Create table comentario(
fecha datetime NOT NULL,
cuenta varchar(60) NOT NULL,
contenido text not null,
constraint pk_comentario Primary key (fecha, cuenta),
constraint fk_comentario_cuenta Foreign key (cuenta) References cuenta(email)
);
GO

Create table informacion(
estado text not null,
fecha datetime not null,
paradero varchar(10) not null,
recorrido varchar(8) not null,
cuenta varchar(60) not null,
constraint pk_informacion primary key (fecha, paradero,recorrido, cuenta),
constraint fk_informacion_recorridos_paraderos foreign key (paradero, recorrido) references recorridos_paradero(paradero, recorrido),
constraint fk_informacion_cuenta foreign key (cuenta) references cuenta(email)
);
GO

create table comentario_informacion(
fecha_comentario datetime not null,
cuenta_comentario varchar(60) not null,
fecha_informacion datetime not null,
paradero varchar(10) not null,
recorrido varchar(8) not null,
cuenta_informacion varchar(60) not null,
calificacion int,
constraint pk_comentario_informacion Primary key (fecha_comentario, cuenta_comentario,fecha_informacion, paradero, recorrido, cuenta_informacion),
constraint fk_comentario_informacion_comentario foreign key (fecha_comentario, cuenta_comentario) references comentario(fecha, cuenta),
constraint fk_comentario_informacion_informacion foreign key (fecha_informacion, paradero, recorrido, cuenta_informacion) references informacion(fecha, paradero, recorrido, cuenta)
);
Go

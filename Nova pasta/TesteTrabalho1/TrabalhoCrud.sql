create database teste;

use teste; 

create table funcionarios
(
	id_funcionarios int not null auto_increment primary key,
    nome varchar(255), 
    idade int,
    CPF varchar(255),
    funcao varchar(255),
    email varchar(255), 
    login varchar(255),
    senha varchar(255)
);

create table clientes 
(
	id_clientes int not null auto_increment primary key,
    nome varchar(255), 
    CPF varchar(255),
	dt_registro varchar(255)
);

create table produtos 
(
	id_produtos int not null auto_increment primary key,
    nome varchar(255), 
    NCM int, 
    unidade varchar(255), 
    quantidade int, 
    valor float
);	

INSERT INTO clientes (nome, CPF, dt_registro) 
VALUES ('João Silva', '123.456.789-00', '2024-11-14');

INSERT INTO clientes (nome, CPF, dt_registro) 
VALUES ('Maria Oliveira', '987.654.321-00', '2023-08-01');

INSERT INTO clientes (nome, CPF, dt_registro) 
VALUES ('Carlos Santos', '456.123.789-00', '2022-05-20');


select * from funcionarios;
select * from produtos;
select * from clientes;
create database prototipobancoOceanid;
use prototipobancoOceanid;

create table tbEndereco(
	idEnd int primary key auto_increment,
	cepEnd varchar(10) not null,
	numeroEnd int unsigned not null,
	logradouro varchar(250) not null,
	complemento varchar(100),
	bairro varchar(100) not null,
    estado varchar(100) not null,
	cidade varchar(100) not null
);


create table tbAdm(
    idAdm int primary key auto_increment,
	nomeAdm varchar(70) not null,
    senhaAdm varchar(30) not null unique,
    emailAdm enum ('adm1@gmail.com','adm2@gmail.com','adm3@gmail.com','adm4@gmail.com','adm5@gmail.com') unique not null
);


create table tbCliente (
	idCliente int primary key auto_increment,
    cpf varchar(11) unique,
    nomeCompleto varchar(200) not null,
    senhaCliente varchar(30) not null unique,
    emailCliente varchar(50) not null unique,
    dataNasc date not null,
    idEnd int,
    foreign key (idEnd) references tbEndereco(idEnd)
);

create table tblogin(
  idLogin  int primary key auto_increment,
  idCliente int, 
  foreign key (idCliente) references tbCliente(idCliente),
  idAdm int, 
  foreign key (idAdm) references tbAdm(idAdm)
);
  
  create table tbCategoria(
	idCategoria int primary key auto_increment,
	nomeCategoria varchar(50) not null
);

create table tbProduto (
	idProd int primary key auto_increment,
	codBar varchar(15),
	nomeProd varchar(200) not null,
	precoProd decimal(10,2) not null,
	qtdProd int unsigned not null,
	marcaProd varchar(50) not null,
	descricaoProd varchar(200) not null,
	idCategoria int not null,
	foreign key (idCategoria) references tbCategoria(idCategoria)
);

create table tbPromocao(
	idPromocao int primary key auto_increment,
	promoAtiv boolean not null default true,
	idProd int not null,
    foreign key (idProd) references tbProduto(idProd) on delete cascade,
    idCategoria int not null,
	foreign key (idCategoria) references tbCategoria(idCategoria) on delete cascade
);




create table tbClienteFavoritos(
    idClienteFav int primary key auto_increment,
    idCliente int not null,
    foreign key (idCliente) references tbCliente(idCliente) on delete cascade,
    idProd int not null,
    foreign key (idProd) references tbProduto(idProd) on delete cascade,
    ativo boolean not null default true,
    constraint unique_client_product unique (idCliente, idProd)
);




/*Esta é uma restrição de unicidade composta que garante que cada combinação de idCliente e idProd seja única na tabela tbClienteFavoritos.*/

create table tbPedido(
    idPed int primary key auto_increment,
    idEnd int not null,
	foreign key (idEnd) references tbEndereco(idEnd),
    idPag int not null,
	foreign key (idPag) references tbPagamento(idPag),
    idCliente int not null,
    foreign key (idCliente) references tbCliente(idCliente),
    dataPed datetime not null,
    totalPed decimal(10,2) not null
);

select * from tbCliente;

create table tbItemPedido (
    idProdutoPedido int primary key auto_increment,
    idPedido int not null,
	foreign key(idPedido) references tbPedido(idPed),
    idProd int not null,
    foreign key (idProd) references tbProduto(idProd),
    quantidade int not null,
    precoUnitario DECIMAL(10, 2)  not null
);

create table tbPagamento(
	idPag int primary key auto_increment,
	statusPag enum('Pago', 'Pendente', 'Não Realizado') not null default 'Pendente',
	metodoPag varchar(50) not null
);




-- **************************** INSERTSS *********************************
-- tbEndereco
INSERT INTO tbEndereco (cepEnd, numeroEnd, logradouro, complemento, bairro, estado, cidade) VALUES
('01001-000', 123, 'Rua das Flores', 'Apto 101', 'Centro', 'SP', 'São Paulo'),
('20040-020', 456, 'Av. Atlântica', null, 'Copacabana', 'RJ', 'Rio de Janeiro'),
('30130-010', 789, 'Praça Sete', 'Loja 5', 'Centro', 'MG', 'Belo Horizonte'),
('40020-000', 321, 'Rua Chile', '', 'Comércio', 'BA', 'Salvador'),
('60055-000', 654, 'Av. Beira Mar', null, 'Meireles', 'CE', 'Fortaleza');

-- tblogin
INSERT INTO tblogin (idCliente, idAdm) VALUES
(1, 6),
(2, 7),
(3, 8),
(4, 9),
(5, 10);

select * from tbAdm;

-- tbCategoria
INSERT INTO tbCategoria (nomeCategoria) VALUES
('Maquiagem'),
('Skincare'),
('Cabelos'),
('Perfumes'),
('Acessórios');

-- tbProduto
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('789456123001', 'Base Líquida Matte', 39.90, 50, 'Oceanid', 'Base de longa duração com acabamento matte.', 1),
('789456123002', 'Sérum Facial Hidratante', 59.90, 30, 'Oceanid', 'Sérum com ácido hialurônico para hidratação intensa.', 2),
('789456123003', 'Shampoo Reparador', 29.90, 40, 'Oceanid', 'Shampoo para cabelos danificados e ressecados.', 3),
('789456123004', 'Perfume Floral Femme', 89.90, 25, 'Oceanid', 'Fragrância floral suave e duradoura.', 4),
('789456123005', 'Kit Pincéis Maquiagem', 49.90, 20, 'Oceanid', 'Conjunto com 10 pincéis profissionais.', 5);

-- tbPromocao
INSERT INTO tbPromocao (promoAtiv, idProd, idCategoria) VALUES
(true, 1, 1),
(true, 2, 2),
(false, 3, 3),
(true, 4, 4),
(false, 5, 5);

-- tbClienteFavoritos
INSERT INTO tbClienteFavoritos (idCliente, idProd, ativo) VALUES
(1, 1, true),
(2, 2, true),
(3, 3, true),
(4, 4, true),
(5, 5, true);

-- tbPagamento
INSERT INTO tbPagamento (statusPag, metodoPag) VALUES
('Pago', 'Cartão de Crédito'),
('Pendente', 'Boleto Bancário'),
('Não Realizado', 'Pix'),
('Pago', 'Cartão de Débito'),
('Pendente', 'Transferência Bancária');

-- tbPedido
INSERT INTO tbPedido (idEnd, idPag, idCliente, dataPed, totalPed) VALUES
(1, 1, 1, NOW(), 39.90),
(2, 2, 2, NOW(), 59.90),
(3, 3, 3, NOW(), 29.90),
(4, 4, 4, NOW(), 89.90),
(5, 5, 5, NOW(), 49.90);

-- tbItemPedido
INSERT INTO tbItemPedido (idPedido, idProd, quantidade, precoUnitario) VALUES
(1, 1, 1, 39.90),
(2, 2, 1, 59.90),
(3, 3, 1, 29.90),
(4, 4, 1, 89.90),
(5, 5, 1, 49.90);









/*------------------------------JOINS--------------------------------------*/

-- JOINS -- Consulta detalhada de compras por cliente

SELECT
    tbCliente.nomeCompleto as cliente, -- Nome do cliente 
    tbPedido.idPed, -- ID do pedido
    tbProduto.idProd, -- Código do produto
    tbProduto.nomeProd, -- Nome do produto
    tbProduto.qtdProd, -- Quantidade do produto no pedido
    tbItemPedido.precoUnitario, -- Preço unitário do produto no Itempedido
    tbPedido.dataPed, -- Data do pedido
    tbPedido.totalPed -- Total do pedido
    
FROM tbCliente
INNER JOIN tbPedido  ON tbPedido.idCliente = tbPedido.idCliente
INNER JOIN tbItemPedido  ON tbItemPedido.idPedido = tbItemPedido.idPedido
INNER JOIN tbProduto  ON tbProduto.idProd = tbProduto.idProd;


-- Total gasto por cada cliente
SELECT 
    tbcliente.nomecompleto AS cliente,
    SUM(tbpedido.totalped) AS 'total gasto'
FROM tbcliente
JOIN tbpedido ON tbcliente.idCliente = tbpedido.idCliente
GROUP BY tbcliente.idCliente, tbcliente.nomecompleto
ORDER BY SUM(tbpedido.totalped) DESC;




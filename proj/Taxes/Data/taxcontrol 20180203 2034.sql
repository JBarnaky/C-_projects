--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 03.02.2018 20:34:47
-- Версия сервера: 12.00.5207
--



USE taxcontrol
GO

IF DB_NAME() <> N'taxcontrol' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[taxPay]
--
PRINT (N'Создать таблицу [dbo].[taxPay]')
GO
CREATE TABLE dbo.taxPay (
  id_tp int IDENTITY,
  tpDate varchar(50) NULL,
  tpSum money NULL,
  tpTax money NULL,
  id_tax int NULL,
  id_org int NULL,
  CONSTRAINT PK_taxPay_id_tp PRIMARY KEY CLUSTERED (id_tp)
)
ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать процедуру [dbo].[taxPay_INS]
--
GO
PRINT (N'Создать процедуру [dbo].[taxPay_INS]')
GO
CREATE PROCEDURE dbo.taxPay_INS
  @tpDate AS DATE, 
  @tpSum AS MONEY, 
  @tpTax AS MONEY, 
  @id_tax AS INT, 
  @id_org AS INT
AS 
  INSERT INTO taxPay (tpDate, tpSum, tpTax, id_tax, id_org)
  VALUES (@tpDate, @tpSum, @tpTax, @id_tax, @id_org);
GO

--
-- Создать процедуру [dbo].[taxPay_DEL]
--
GO
PRINT (N'Создать процедуру [dbo].[taxPay_DEL]')
GO
CREATE PROCEDURE dbo.taxPay_DEL
  @id_tp AS INT
AS 
  DELETE FROM taxPay WHERE id_tp = @id_tp;
GO

--
-- Создать процедуру [dbo].[gr_SEL]
--
GO
PRINT (N'Создать процедуру [dbo].[gr_SEL]')
GO
CREATE PROCEDURE dbo.gr_SEL
  @id_org AS INT,
  @pYear AS INT
AS 
  SELECT DATEPART(MONTH, p.tpDate) AS pMonth, SUM(p.tpTax) AS sTax
  FROM taxPay p
  WHERE p.id_org = @id_org AND
    DATEPART(YEAR, p.tpDate) = @pYear
  GROUP BY DATEPART(MONTH, p.tpDate)
  ORDER BY pMonth
  
  
GO

--
-- Создать таблицу [dbo].[taxes]
--
PRINT (N'Создать таблицу [dbo].[taxes]')
GO
CREATE TABLE dbo.taxes (
  id_tax int IDENTITY,
  taxPerc numeric(5, 2) NULL,
  taxName varchar(20) NULL,
  CONSTRAINT PK_taxes_id_tax PRIMARY KEY CLUSTERED (id_tax)
)
ON [PRIMARY]
GO

--
-- Создать процедуру [dbo].[taxes_SEL]
--
GO
PRINT (N'Создать процедуру [dbo].[taxes_SEL]')
GO
CREATE PROCEDURE dbo.taxes_SEL
AS
  SELECT
    t.id_tax
   ,t.taxPerc
   ,t.taxName
  FROM taxes t
GO

--
-- Создать процедуру [dbo].[taxes_INS]
--
GO
PRINT (N'Создать процедуру [dbo].[taxes_INS]')
GO
CREATE PROCEDURE dbo.taxes_INS
  @taxPerc AS NUMERIC(5,2), 
  @taxName AS VARCHAR(20),
  @id_tax AS INT OUTPUT
AS
  INSERT INTO taxes (taxPerc, taxName)
  VALUES (@taxPerc, @taxName); 

  SET @id_tax = SCOPE_IDENTITY();
GO

--
-- Создать таблицу [dbo].[organization]
--
PRINT (N'Создать таблицу [dbo].[organization]')
GO
CREATE TABLE dbo.organization (
  id_org int IDENTITY,
  orgName varchar(70) NULL,
  orgPhone varchar(20) NULL,
  orgAdr varchar(150) NULL,
  orgRCnt varchar(30) NULL,
  orgUNP numeric(9) NULL,
  id_kn int NULL,
  id_in int NULL,
  CONSTRAINT PK_organization_id_org PRIMARY KEY CLUSTERED (id_org)
)
ON [PRIMARY]
GO

--
-- Создать процедуру [dbo].[taxPay_SEL]
--
GO
PRINT (N'Создать процедуру [dbo].[taxPay_SEL]')
GO
CREATE PROCEDURE dbo.taxPay_SEL
  @pMonth AS INT,
  @pYear AS INT
AS 
  SELECT
  taxPay.id_tp
 ,taxPay.tpDate
 ,taxPay.tpSum
 ,taxPay.tpTax
 ,taxPay.id_tax
 ,taxes.taxName
 ,taxes.taxPerc
 ,taxPay.id_org
 ,organization.orgName
 ,CAST(taxPay.tpSum * taxes.taxPerc / 100 AS MONEY) AS forTax
 ,CAST(taxPay.tpTax - (taxPay.tpSum * taxes.taxPerc / 100) AS MONEY) AS saldo
FROM dbo.taxPay
LEFT JOIN dbo.organization
  ON taxPay.id_org = organization.id_org
LEFT JOIN dbo.taxes
  ON taxPay.id_tax = taxes.id_tax
WHERE
  DATEPART(YEAR, tpDate) = @pYear AND 
  DATEPART(MONTH, tpDate) = @pMonth;
GO

--
-- Создать процедуру [dbo].[org_INS]
--
GO
PRINT (N'Создать процедуру [dbo].[org_INS]')
GO
CREATE PROCEDURE dbo.org_INS
  @orgName AS VARCHAR(70), 
  @orgPhone AS VARCHAR(20), 
  @orgAdr AS VARCHAR(150), 
  @orgRCnt AS VARCHAR(30), 
  @orgUNP AS NUMERIC(9), 
  @id_kn AS INT, 
  @id_in AS INT,
  @id_org AS INT OUTPUT
AS 
  INSERT INTO organization (orgName, orgPhone, orgAdr, orgRCnt, orgUNP, id_kn, id_in)
  VALUES (@orgName, @orgPhone, @orgAdr, @orgRCnt, @orgUNP, @id_kn, @id_in);

  SET @id_org = SCOPE_IDENTITY();
GO

--
-- Создать процедуру [dbo].[org_DEL]
--
GO
PRINT (N'Создать процедуру [dbo].[org_DEL]')
GO
CREATE PROCEDURE dbo.org_DEL
  @id_org AS INT
AS 
  DELETE FROM organization WHERE id_org = @id_org;
GO

--
-- Создать таблицу [dbo].[kind]
--
PRINT (N'Создать таблицу [dbo].[kind]')
GO
CREATE TABLE dbo.kind (
  id_kn int IDENTITY,
  knName varchar(50) NULL,
  CONSTRAINT PK_kind_id_kn PRIMARY KEY CLUSTERED (id_kn)
)
ON [PRIMARY]
GO

--
-- Создать процедуру [dbo].[kind_INS]
--
GO
PRINT (N'Создать процедуру [dbo].[kind_INS]')
GO
CREATE PROCEDURE dbo.kind_INS
  @knName AS VARCHAR(50),
  @id_kn AS INT OUTPUT
AS
  INSERT INTO kind (knName)
  VALUES (@knName); 

  SET @id_kn = SCOPE_IDENTITY();
GO

--
-- Создать таблицу [dbo].[inspection]
--
PRINT (N'Создать таблицу [dbo].[inspection]')
GO
CREATE TABLE dbo.inspection (
  id_in int IDENTITY,
  inName varchar(50) NULL,
  inPhone varchar(20) NULL,
  inAdr varchar(150) NULL,
  CONSTRAINT PK_inspection_id_in PRIMARY KEY CLUSTERED (id_in)
)
ON [PRIMARY]
GO

--
-- Создать процедуру [dbo].[org_SEL]
--
GO
PRINT (N'Создать процедуру [dbo].[org_SEL]')
GO
CREATE PROCEDURE dbo.org_SEL
AS
SELECT
  organization.id_org
 ,organization.orgName
 ,organization.orgPhone
 ,organization.orgAdr
 ,organization.orgRCnt
 ,organization.orgUNP
 ,organization.id_kn
 ,kind.knName
 ,organization.id_in
 ,inspection.inName
FROM dbo.organization
LEFT OUTER JOIN dbo.kind
  ON organization.id_kn = kind.id_kn
LEFT OUTER JOIN dbo.inspection
  ON organization.id_in = inspection.id_in

GO

--
-- Создать процедуру [dbo].[inspetion_SEL]
--
GO
PRINT (N'Создать процедуру [dbo].[inspetion_SEL]')
GO
CREATE PROCEDURE dbo.inspetion_SEL
AS
  SELECT
    i.id_in
   ,i.inName
   ,i.inPhone
   ,i.inAdr
  FROM inspection i
GO

--
-- Создать процедуру [dbo].[inspetion_INS]
--
GO
PRINT (N'Создать процедуру [dbo].[inspetion_INS]')
GO
CREATE PROCEDURE dbo.inspetion_INS
  @inName AS VARCHAR(50), 
  @inPhone AS VARCHAR(20), 
  @inAdr AS VARCHAR(150),
  @id_in AS INT OUTPUT
AS 
  INSERT INTO inspection (inName, inPhone, inAdr)
  VALUES (@inName, @inPhone, @inAdr);

  SET @id_in = SCOPE_IDENTITY();
GO
-- 
-- Вывод данных для таблицы inspection
--
SET IDENTITY_INSERT dbo.inspection ON
GO
INSERT dbo.inspection(id_in, inName, inPhone, inAdr) VALUES (1, N'Центральная', N'142-75-89', N'Можайка 15')
INSERT dbo.inspection(id_in, inName, inPhone, inAdr) VALUES (2, N'Восточная', N'125-89-80', N'п-т Победы 18')
INSERT dbo.inspection(id_in, inName, inPhone, inAdr) VALUES (3, N'Железнодорожная', N'172-90-96', N'Станционная 45')
GO
SET IDENTITY_INSERT dbo.inspection OFF
GO
-- 
-- Вывод данных для таблицы kind
--
SET IDENTITY_INSERT dbo.kind ON
GO
INSERT dbo.kind(id_kn, knName) VALUES (1, N'производственная')
INSERT dbo.kind(id_kn, knName) VALUES (3, N'финансово-экономическая')
INSERT dbo.kind(id_kn, knName) VALUES (4, N'Образовательная ')
GO
SET IDENTITY_INSERT dbo.kind OFF
GO
-- 
-- Вывод данных для таблицы organization
--
SET IDENTITY_INSERT dbo.organization ON
GO
INSERT dbo.organization(id_org, orgName, orgPhone, orgAdr, orgRCnt, orgUNP, id_kn, id_in) VALUES (1, N'ОАО "Рога и копыта"', N'488-66-55', N'Пушкина 18', N'854654658546565', 546465465, 1, 1)
INSERT dbo.organization(id_org, orgName, orgPhone, orgAdr, orgRCnt, orgUNP, id_kn, id_in) VALUES (3, N'ООО Геркулес', N'745-99-66', N'Семеновский б-р, 19', N'894658468465', 654654654, 4, 1)
GO
SET IDENTITY_INSERT dbo.organization OFF
GO
-- 
-- Вывод данных для таблицы taxes
--
SET IDENTITY_INSERT dbo.taxes ON
GO
INSERT dbo.taxes(id_tax, taxPerc, taxName) VALUES (1, 20.00, N'НДС')
INSERT dbo.taxes(id_tax, taxPerc, taxName) VALUES (2, 2.00, N'Недвижимость')
INSERT dbo.taxes(id_tax, taxPerc, taxName) VALUES (3, 12.00, N'Подоходный')
INSERT dbo.taxes(id_tax, taxPerc, taxName) VALUES (4, 5.00, N'Транспортный')
GO
SET IDENTITY_INSERT dbo.taxes OFF
GO
-- 
-- Вывод данных для таблицы taxPay
--
SET IDENTITY_INSERT dbo.taxPay ON
GO
INSERT dbo.taxPay(id_tp, tpDate, tpSum, tpTax, id_tax, id_org) VALUES (1, N'2018-01-17', 500.0000, 20.0000, 3, 1)
INSERT dbo.taxPay(id_tp, tpDate, tpSum, tpTax, id_tax, id_org) VALUES (2, N'2018-01-11', 1500.0000, 100.0000, 4, 1)
INSERT dbo.taxPay(id_tp, tpDate, tpSum, tpTax, id_tax, id_org) VALUES (3, N'2018-02-13', 1900.0000, 150.0000, 1, 1)
INSERT dbo.taxPay(id_tp, tpDate, tpSum, tpTax, id_tax, id_org) VALUES (4, N'2018-02-17', 80000.0000, 800.0000, 3, 1)
GO
SET IDENTITY_INSERT dbo.taxPay OFF
GO

USE taxcontrol
GO

IF DB_NAME() <> N'taxcontrol' SET NOEXEC ON
GO

--
-- Создать внешний ключ [FK_organization_inspection_id_in] для объекта типа таблица [dbo].[organization]
--
PRINT (N'Создать внешний ключ [FK_organization_inspection_id_in] для объекта типа таблица [dbo].[organization]')
GO
ALTER TABLE dbo.organization
  ADD CONSTRAINT FK_organization_inspection_id_in FOREIGN KEY (id_in) REFERENCES dbo.inspection (id_in)
GO

--
-- Создать внешний ключ [FK_organization_kind_id_kn] для объекта типа таблица [dbo].[organization]
--
PRINT (N'Создать внешний ключ [FK_organization_kind_id_kn] для объекта типа таблица [dbo].[organization]')
GO
ALTER TABLE dbo.organization
  ADD CONSTRAINT FK_organization_kind_id_kn FOREIGN KEY (id_kn) REFERENCES dbo.kind (id_kn)
GO

--
-- Создать внешний ключ [FK_taxPay_organization_id_org] для объекта типа таблица [dbo].[taxPay]
--
PRINT (N'Создать внешний ключ [FK_taxPay_organization_id_org] для объекта типа таблица [dbo].[taxPay]')
GO
ALTER TABLE dbo.taxPay
  ADD CONSTRAINT FK_taxPay_organization_id_org FOREIGN KEY (id_org) REFERENCES dbo.organization (id_org)
GO

--
-- Создать внешний ключ [FK_taxPay_taxes_id_tax] для объекта типа таблица [dbo].[taxPay]
--
PRINT (N'Создать внешний ключ [FK_taxPay_taxes_id_tax] для объекта типа таблица [dbo].[taxPay]')
GO
ALTER TABLE dbo.taxPay
  ADD CONSTRAINT FK_taxPay_taxes_id_tax FOREIGN KEY (id_tax) REFERENCES dbo.taxes (id_tax)
GO
SET NOEXEC OFF
GO
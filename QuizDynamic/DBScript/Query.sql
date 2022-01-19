
--lag en database først

SELECT QuestionId,QuestionDescription FROM QuestionTable
SELECT * FROM OptionTable
SELECT * FROM AnswerTable

CREATE TABLE QuestionTable(QuestionId INT NOT NULL,QuestionDescription NVARCHAR(500) NOT NULL)
INSERT INTO QuestionTable VALUES(1,'Who is the Prime minister of India')
INSERT INTO QuestionTable VALUES(2,'Capital City of Maharashtra')
INSERT INTO QuestionTable VALUES(3,'Maximum run in ODI by a batsman')

CREATE TABLE OptionTable(QuestionId INT NOT NULL,Options NVARCHAR(500) NOT NULL)
INSERT INTO OptionTable VALUES(1,'LK Advani')
INSERT INTO OptionTable VALUES(1,'Manmohan singh')
INSERT INTO OptionTable VALUES(1,'Sachin Tendulker')
INSERT INTO OptionTable VALUES(1,'N. Modi')
INSERT INTO OptionTable VALUES(2,'Delhi')
INSERT INTO OptionTable VALUES(2,'Mumbai')
INSERT INTO OptionTable VALUES(2,'Kolkata')
INSERT INTO OptionTable VALUES(3,'Ricky Pointing')
INSERT INTO OptionTable VALUES(3,'Sachin Tendulker')

CREATE TABLE AnswerTable(QuestionId INT NOT NULL,Answer NVARCHAR(500) NOT NULL)
INSERT INTO AnswerTable VALUES(1,'N. Modi')
INSERT INTO AnswerTable VALUES(2,'Mumbai')
INSERT INTO AnswerTable VALUES(3,'Sachin Tendulker')

--dette er en manuell måte å lage spørsmål på. Det kan også lages en egen webform for dette gjøres, slik at man ikke trenger å gå inn i databasen hver gang en quiz skal opprettes.
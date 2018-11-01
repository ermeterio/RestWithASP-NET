CREATE TABLE `persons`
(
 `Id` int(10) UNSIGNED null default null,
`FirstName` VARCHAR(50) NULL DEFAULT NULL,
`LastName` VARCHAR(50) NULL DEFAULT NULL,
`Address` VARCHAR(50) NULL DEFAULT NULL,
`Gender` VARCHAR(50) NULL DEFAULT NULL
)
ENGINE=InnoDB
;

alter table persons 
change Id Id int(10) AUTO_INCREMENT primary key;
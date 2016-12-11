SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `crossover_sem_journals` DEFAULT CHARACTER SET utf8 ;
USE `crossover_sem_journals` ;

-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`Journal`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`Journal` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`Journal` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(256) NULL DEFAULT NULL,
  `Price` DOUBLE NULL DEFAULT 0,
  `Created` DATETIME NULL DEFAULT NULL,
  `Updated` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`Paper`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`Paper` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`Paper` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(255) NULL DEFAULT NULL,
  `JournalId` INT NULL,
  `OriginalFile` BLOB NULL,
  `Created` DATETIME NULL DEFAULT NULL,
  `Updated` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`Roles`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`Roles` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`Roles` (
  `Id` VARCHAR(128) NOT NULL,
  `Name` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`Users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`Users` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`Users` (
  `Id` VARCHAR(128) NOT NULL,
  `Email` VARCHAR(256) NULL DEFAULT NULL,
  `UserName` VARCHAR(256) NOT NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` LONGTEXT NULL DEFAULT NULL,
  `SecurityStamp` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumber` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEndDateUtc` DATETIME NULL DEFAULT NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT(11) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`UserClaims`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`UserClaims` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`UserClaims` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(128) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id` (`Id` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`UserLogins`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`UserLogins` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`UserLogins` (
  `LoginProvider` VARCHAR(128) NOT NULL,
  `ProviderKey` VARCHAR(128) NOT NULL,
  `UserId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`, `ProviderKey`, `UserId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`UserRoles`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`UserRoles` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`UserRoles` (
  `UserId` VARCHAR(128) NOT NULL,
  `RoleId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`Subscription`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`Subscription` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`Subscription` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(128) NULL,
  `JournalId` INT NULL,
  `Created` DATETIME NULL,
  `Updated` DATETIME NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `crossover_sem_journals`.`PaperPage`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `crossover_sem_journals`.`PaperPage` ;

CREATE TABLE IF NOT EXISTS `crossover_sem_journals`.`PaperPage` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `PageNumber` INT NULL,
  `Image` BLOB NULL,
  `PaperId` INT NULL,
  `Created` DATETIME NULL,
  `Updated` DATETIME NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `crossover_sem_journals`.`Journal`
-- -----------------------------------------------------
START TRANSACTION;
USE `crossover_sem_journals`;
INSERT INTO `crossover_sem_journals`.`Journal` (`Id`, `Name`, `Price`, `Created`, `Updated`) VALUES (2, 'Brain', NULL, NULL, NULL);
INSERT INTO `crossover_sem_journals`.`Journal` (`Id`, `Name`, `Price`, `Created`, `Updated`) VALUES (3, 'Pediatrics', NULL, NULL, NULL);
INSERT INTO `crossover_sem_journals`.`Journal` (`Id`, `Name`, `Price`, `Created`, `Updated`) VALUES (1, 'Orthopedics', NULL, NULL, NULL);

COMMIT;


-- -----------------------------------------------------
-- Data for table `crossover_sem_journals`.`Paper`
-- -----------------------------------------------------
START TRANSACTION;
USE `crossover_sem_journals`;
INSERT INTO `crossover_sem_journals`.`Paper` (`Id`, `Name`, `JournalId`, `OriginalFile`, `Created`, `Updated`) VALUES (4, 'Paper 1.1', 1, NULL, NULL, NULL);
INSERT INTO `crossover_sem_journals`.`Paper` (`Id`, `Name`, `JournalId`, `OriginalFile`, `Created`, `Updated`) VALUES (5, 'Paper 1.2', 1, NULL, NULL, NULL);
INSERT INTO `crossover_sem_journals`.`Paper` (`Id`, `Name`, `JournalId`, `OriginalFile`, `Created`, `Updated`) VALUES (6, 'Paper 2.1', 2, NULL, '', NULL);

COMMIT;


-- -----------------------------------------------------
-- Data for table `crossover_sem_journals`.`Roles`
-- -----------------------------------------------------
START TRANSACTION;
USE `crossover_sem_journals`;
INSERT INTO `crossover_sem_journals`.`Roles` (`Id`, `Name`) VALUES ('13b70ae4-4041-4f3e-8a9d-d5d5297991d8', 'Admin');

COMMIT;


-- -----------------------------------------------------
-- Data for table `crossover_sem_journals`.`Users`
-- -----------------------------------------------------
START TRANSACTION;
USE `crossover_sem_journals`;
INSERT INTO `crossover_sem_journals`.`Users` (`Id`, `Email`, `UserName`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEndDateUtc`, `LockoutEnabled`, `AccessFailedCount`) VALUES ('775912f4-978c-4a9c-9204-a5ea4bcdf5e3', 'admin@email.com', 'admin@email.com', 1, 'AD+c+kxUK6J6eQ1WNfYfMIyyw9aqVWp8D2nX+RNEe1DaTSdZyo903pmIKs3M56ne3Q==', NULL, '', 0, 0, NULL, 0, 0);

COMMIT;


-- -----------------------------------------------------
-- Data for table `crossover_sem_journals`.`UserRoles`
-- -----------------------------------------------------
START TRANSACTION;
USE `crossover_sem_journals`;
INSERT INTO `crossover_sem_journals`.`UserRoles` (`UserId`, `RoleId`) VALUES ('775912f4-978c-4a9c-9204-a5ea4bcdf5e3', '13b70ae4-4041-4f3e-8a9d-d5d5297991d8');

COMMIT;


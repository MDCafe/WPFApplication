-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema MDCafe
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema MDCafe
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `MDCafe` DEFAULT CHARACTER SET utf8 ;
USE `MDCafe` ;

-- -----------------------------------------------------
-- Table `MDCafe`.`Items`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MDCafe`.`Items` (
  `code` INT NOT NULL,
  `Name` VARCHAR(45) NULL,
  `Description` VARCHAR(100) NULL,
  `UnitOfMeasure` VARCHAR(45) NULL,
  `CurrentPrice` FLOAT NULL,
  `CategoryId` INT NULL,
  PRIMARY KEY (`code`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MDCafe`.`Customers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MDCafe`.`Customers` (
  `Id` INT NOT NULL,
  `Name` VARCHAR(45) NULL,
  `Description` VARCHAR(45) NULL,
  `Address` VARCHAR(500) NULL,
  `ContactNo` INT NULL,
  `Email` VARCHAR(45) NULL,
  `IsExistingCustomer` TINYINT(1) NULL,
  `BalanceAmount` FLOAT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MDCafe`.`Sales`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MDCafe`.`Sales` (
  `id` INT NOT NULL,
  `ItemCode` INT NULL,
  `Quantity` FLOAT NULL,
  `CustomerId` INT NULL,
  `SaleDate` DATETIME NULL,
  `PriceOverride` FLOAT NULL,
  `CurrentPrice` FLOAT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MDCafe`.`Accounts`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MDCafe`.`Accounts` (
  `Date` DATETIME NOT NULL,
  `OpeningBalance` FLOAT NULL,
  `ClosingBalance` FLOAT NULL,
  PRIMARY KEY (`Date`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MDCafe`.`CustomerTransactions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MDCafe`.`CustomerTransactions` (
  `CustomerId` INT NOT NULL,
  `TopUpAmount` FLOAT NULL,
  `Date` DATETIME NULL)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

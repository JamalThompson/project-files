-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: asl
-- ------------------------------------------------------
-- Server version	5.7.19-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(40) DEFAULT NULL,
  `Address1` varchar(30) DEFAULT NULL,
  `BillCycle` int(11) DEFAULT NULL,
  `Contact_name` varchar(40) DEFAULT NULL,
  `Contact_address` varchar(30) DEFAULT NULL,
  `Address2` varchar(30) DEFAULT NULL,
  `City` varchar(35) DEFAULT NULL,
  `State` varchar(2) DEFAULT NULL,
  `Zip` varchar(10) DEFAULT NULL,
  `Phone_Number` varchar(20) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `Email` varchar(45) DEFAULT NULL,
  `AccountBalance` decimal(13,4) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
INSERT INTO `customer` VALUES (1,'jacob','121 s hampton rd',3,'Jacob','121 s hampton rd apt64','121 s hampton rd apt 64','Burleson','Tx','76028','817-298-4550',1,NULL,NULL),(2,'katelynn dennis','121 s hampton rd apt 64',2,'katelynn','121 s hampton rd apt 64','121 s hampton rd apt 64','Crowley','Tx','76036','8177308009',1,NULL,NULL);
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expense_type`
--

DROP TABLE IF EXISTS `expense_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `expense_type` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(25) DEFAULT NULL,
  `Desciption` varchar(150) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expense_type`
--

LOCK TABLES `expense_type` WRITE;
/*!40000 ALTER TABLE `expense_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `expense_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expenses`
--

DROP TABLE IF EXISTS `expenses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `expenses` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(25) DEFAULT NULL,
  `Total` decimal(13,4) DEFAULT NULL,
  `SvcId` int(11) DEFAULT NULL,
  `SvcType` varchar(15) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expenses`
--

LOCK TABLES `expenses` WRITE;
/*!40000 ALTER TABLE `expenses` DISABLE KEYS */;
INSERT INTO `expenses` VALUES (1,'flowers',50.0000,1,NULL,0),(12,'test',900.0000,NULL,'code',0),(13,'test',589.0000,NULL,'code',0),(14,'g',1.0000,NULL,'l',0),(15,'texte',10005.0000,NULL,'f',0),(16,'haksdfjgh',50.0000,NULL,'landscape',0),(17,'jhfj',600.0000,NULL,'landscape',0),(18,'jfd',600.0000,NULL,'landscape',0),(19,'ttt',555.0000,NULL,'ggg',0),(20,'f',4.0000,NULL,'h',0);
/*!40000 ALTER TABLE `expenses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoicehdr`
--

DROP TABLE IF EXISTS `invoicehdr`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `invoicehdr` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InvNum` int(11) DEFAULT NULL,
  `CustId` int(11) DEFAULT NULL,
  `Subtotal` decimal(13,4) DEFAULT NULL,
  `Total` decimal(13,4) DEFAULT NULL,
  `Paid` tinyint(4) DEFAULT NULL,
  `PymmtTerms` varchar(30) DEFAULT NULL,
  `Bill_Date` date DEFAULT NULL,
  `Due_Date` date DEFAULT NULL,
  `Paid_Date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `CustId` (`CustId`),
  CONSTRAINT `invoicehdr_ibfk_1` FOREIGN KEY (`CustId`) REFERENCES `customer` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoicehdr`
--

LOCK TABLES `invoicehdr` WRITE;
/*!40000 ALTER TABLE `invoicehdr` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoicehdr` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoiceli`
--

DROP TABLE IF EXISTS `invoiceli`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `invoiceli` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InvNum` int(11) DEFAULT NULL,
  `CustId` int(11) DEFAULT NULL,
  `Total` decimal(13,4) DEFAULT NULL,
  `SvcId` int(11) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `CustId` (`CustId`),
  KEY `SvcId` (`SvcId`),
  CONSTRAINT `invoiceli_ibfk_1` FOREIGN KEY (`CustId`) REFERENCES `customer` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoiceli`
--

LOCK TABLES `invoiceli` WRITE;
/*!40000 ALTER TABLE `invoiceli` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoiceli` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payments`
--

DROP TABLE IF EXISTS `payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payments` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InvId` int(11) DEFAULT NULL,
  `Amount` decimal(13,4) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `InvId` (`InvId`),
  CONSTRAINT `payments_ibfk_1` FOREIGN KEY (`InvId`) REFERENCES `invoicehdr` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payments`
--

LOCK TABLES `payments` WRITE;
/*!40000 ALTER TABLE `payments` DISABLE KEYS */;
/*!40000 ALTER TABLE `payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `service`
--

DROP TABLE IF EXISTS `service`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `service` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CustId` int(11) DEFAULT NULL,
  `SvcType` varchar(15) DEFAULT NULL,
  `Address1` varchar(30) DEFAULT NULL,
  `Address2` varchar(30) DEFAULT NULL,
  `City` varchar(35) DEFAULT NULL,
  `State` varchar(2) DEFAULT NULL,
  `Zip` varchar(10) DEFAULT NULL,
  `SvcDescription` varchar(65) DEFAULT NULL,
  `Start_Date` date DEFAULT NULL,
  `Start_Time` time DEFAULT NULL,
  `End_Date` date DEFAULT NULL,
  `End_Time` time DEFAULT NULL,
  `BillSubTotal` decimal(13,4) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `SvcAddressId` int(11) DEFAULT NULL,
  `Sched_Date` date DEFAULT NULL,
  `comp_date` date DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `CustId` (`CustId`),
  KEY `fk_SvcAddressId` (`SvcAddressId`),
  CONSTRAINT `fk_SvcAddressId` FOREIGN KEY (`SvcAddressId`) REFERENCES `serviceaddress` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `service`
--

LOCK TABLES `service` WRITE;
/*!40000 ALTER TABLE `service` DISABLE KEYS */;
INSERT INTO `service` VALUES (1,6,NULL,'358 Paris Lane',NULL,'Carrollton','TX','75007',NULL,NULL,NULL,NULL,NULL,35.0000,1,NULL,'2018-03-13',NULL),(2,4,NULL,'358 Paris Lane',NULL,'Carrollton','TX','75007',NULL,NULL,NULL,NULL,NULL,35.0000,1,NULL,'2018-03-13',NULL),(3,12,NULL,'1325 Victory Lane',NULL,'Richardson','TX','70004',NULL,NULL,NULL,NULL,NULL,40.0000,1,NULL,'2018-03-07',NULL),(4,4,NULL,'789 Louis Lane',NULL,'Hurst','TX','75009',NULL,NULL,NULL,NULL,NULL,18.0000,1,NULL,'2018-03-11','0001-01-01'),(5,5,NULL,'1257 Holms Lane',NULL,'Carrollton','TX','75008',NULL,NULL,NULL,NULL,NULL,62.0000,1,NULL,'2018-03-13','2018-03-14');
/*!40000 ALTER TABLE `service` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `serviceaddress`
--

DROP TABLE IF EXISTS `serviceaddress`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `serviceaddress` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CustId` int(11) DEFAULT NULL,
  `Address1` varchar(45) DEFAULT NULL,
  `Address2` varchar(45) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `State` varchar(2) DEFAULT NULL,
  `Zip` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `CustId` (`CustId`),
  CONSTRAINT `serviceaddress_ibfk_1` FOREIGN KEY (`CustId`) REFERENCES `customer` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `serviceaddress`
--

LOCK TABLES `serviceaddress` WRITE;
/*!40000 ALTER TABLE `serviceaddress` DISABLE KEYS */;
/*!40000 ALTER TABLE `serviceaddress` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `svctype`
--

DROP TABLE IF EXISTS `svctype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `svctype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SvcType` varchar(15) DEFAULT NULL,
  `Rate` decimal(13,4) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `svctype`
--

LOCK TABLES `svctype` WRITE;
/*!40000 ALTER TABLE `svctype` DISABLE KEYS */;
/*!40000 ALTER TABLE `svctype` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-03-14 19:45:32

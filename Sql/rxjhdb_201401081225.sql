/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50150
Source Host           : localhost:3306
Source Database       : rxjhdb

Target Server Type    : MYSQL
Target Server Version : 50150
File Encoding         : 65001

Date: 2014-01-08 12:25:37
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for tb_ability
-- ----------------------------
DROP TABLE IF EXISTS `tb_ability`;
CREATE TABLE `tb_ability` (
  `PlayerId` int(11) NOT NULL DEFAULT '0',
  `AbilityId` int(11) NOT NULL DEFAULT '0',
  `AbilityLevel` int(11) NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_ability
-- ----------------------------
INSERT INTO `tb_ability` VALUES ('1', '10', '8');
INSERT INTO `tb_ability` VALUES ('2', '10', '0');
INSERT INTO `tb_ability` VALUES ('2', '11', '0');
INSERT INTO `tb_ability` VALUES ('2', '12', '0');
INSERT INTO `tb_ability` VALUES ('2', '13', '0');
INSERT INTO `tb_ability` VALUES ('2', '14', '0');
INSERT INTO `tb_ability` VALUES ('3', '20', '0');
INSERT INTO `tb_ability` VALUES ('3', '21', '0');
INSERT INTO `tb_ability` VALUES ('3', '22', '0');
INSERT INTO `tb_ability` VALUES ('3', '23', '0');
INSERT INTO `tb_ability` VALUES ('3', '24', '0');

-- ----------------------------
-- Table structure for tb_account
-- ----------------------------
DROP TABLE IF EXISTS `tb_account`;
CREATE TABLE `tb_account` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  `passwd` varchar(32) NOT NULL,
  `isgm` tinyint(1) NOT NULL DEFAULT '0',
  `activated` tinyint(1) NOT NULL DEFAULT '0',
  `membership` tinyint(1) NOT NULL DEFAULT '0',
  `last_ip` varchar(20) NOT NULL,
  `cash` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_account
-- ----------------------------
INSERT INTO `tb_account` VALUES ('1', 'admin', '60590d1d7d770b6b2687e6427985cf0e', '0', '0', '0', '192.168.1.111', '0');
INSERT INTO `tb_account` VALUES ('2', 'test', '6c4fdb8465cf7279086d0d633638a174', '0', '0', '0', '127.0.0.1', '0');
INSERT INTO `tb_account` VALUES ('3', 'master', 'admin', '0', '0', '0', '192.168.1.111', '0');
INSERT INTO `tb_account` VALUES ('4', 'ghost', 'admin', '0', '0', '0', '192.168.1.111', '0');

-- ----------------------------
-- Table structure for tb_inventory
-- ----------------------------
DROP TABLE IF EXISTS `tb_inventory`;
CREATE TABLE `tb_inventory` (
  `UniqueID` bigint(20) NOT NULL,
  `PlayerID` int(11) NOT NULL DEFAULT '0',
  `ItemID` bigint(20) NOT NULL,
  `Amount` int(11) NOT NULL DEFAULT '0',
  `Magic0` int(11) NOT NULL DEFAULT '0',
  `Magic1` int(11) NOT NULL DEFAULT '0',
  `Magic2` int(11) NOT NULL DEFAULT '0',
  `Magic3` int(11) NOT NULL DEFAULT '0',
  `Magic4` int(11) NOT NULL DEFAULT '0',
  `BonusMagic1` int(11) NOT NULL DEFAULT '0',
  `BonusMagic2` int(11) NOT NULL DEFAULT '0',
  `BonusMagic3` int(11) NOT NULL DEFAULT '0',
  `BonusMagic4` int(11) NOT NULL DEFAULT '0',
  `BonusMagic5` int(11) NOT NULL DEFAULT '0',
  `Equiped` int(1) NOT NULL DEFAULT '0',
  `Slot` int(11) NOT NULL DEFAULT '0',
  `InventoryType` int(11) NOT NULL DEFAULT '0',
  `LimitTime` int(11) NOT NULL DEFAULT '0',
  `Upgrade` int(11) NOT NULL DEFAULT '0',
  `Quality` int(11) NOT NULL DEFAULT '0',
  `Lock` int(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`UniqueID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_inventory
-- ----------------------------
INSERT INTO `tb_inventory` VALUES ('1000009', '1', '100200001', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '3', '0', '0', '0', '0', '0');
INSERT INTO `tb_inventory` VALUES ('1000001', '1', '1000000101', '5', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `tb_inventory` VALUES ('1000003', '1', '1008000619', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '30', '0', '0', '0', '0', '0');
INSERT INTO `tb_inventory` VALUES ('1000004', '1', '1000000104', '99', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '0', '0', '0', '0', '0');
INSERT INTO `tb_inventory` VALUES ('1000007', '1', '800000006', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '35', '0', '0', '0', '0', '0');
INSERT INTO `tb_inventory` VALUES ('1000008', '1', '100200001', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '5', '0', '0', '0', '0', '0');

-- ----------------------------
-- Table structure for tb_player
-- ----------------------------
DROP TABLE IF EXISTS `tb_player`;
CREATE TABLE `tb_player` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `account` int(11) NOT NULL DEFAULT '0',
  `server_id` int(11) NOT NULL DEFAULT '0',
  `name` varchar(15) NOT NULL,
  `level` int(11) NOT NULL DEFAULT '1',
  `job` int(2) NOT NULL DEFAULT '0',
  `title` int(11) NOT NULL DEFAULT '0',
  `map` int(11) NOT NULL DEFAULT '101',
  `x` float NOT NULL,
  `y` float NOT NULL,
  `z` float NOT NULL,
  `gender` int(11) NOT NULL DEFAULT '1',
  `forces` int(11) NOT NULL DEFAULT '0',
  `hair` int(11) NOT NULL DEFAULT '0',
  `color` int(11) NOT NULL DEFAULT '0',
  `face` int(11) NOT NULL,
  `voice` int(11) NOT NULL,
  `hp` int(11) NOT NULL,
  `mp` int(11) NOT NULL,
  `sp` int(11) NOT NULL,
  `exp` bigint(20) NOT NULL DEFAULT '0',
  `skillpoint` int(11) NOT NULL DEFAULT '0',
  `abilitypoint` int(11) NOT NULL DEFAULT '0',
  `money` bigint(20) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_player
-- ----------------------------
INSERT INTO `tb_player` VALUES ('1', '1', '1', 'Admin', '10', '1', '0', '101', '61.8131', '1550.84', '15', '1', '0', '1', '0', '0', '1', '253', '101', '0', '2110', '918', '10', '344');
INSERT INTO `tb_player` VALUES ('2', '3', '1', 'MASTER', '1', '1', '0', '101', '399.719', '1765.28', '15', '1', '0', '1', '0', '0', '1', '145', '120', '0', '100', '0', '0', '100');
INSERT INTO `tb_player` VALUES ('3', '4', '1', 'GHOST', '1', '2', '0', '101', '300', '1865', '15', '1', '0', '1', '0', '0', '1', '157', '118', '0', '0', '0', '0', '100');

-- ----------------------------
-- Table structure for tb_shop
-- ----------------------------
DROP TABLE IF EXISTS `tb_shop`;
CREATE TABLE `tb_shop` (
  `NpcID` int(11) NOT NULL DEFAULT '0',
  `ItemSlot` int(11) NOT NULL DEFAULT '0',
  `ItemID` bigint(20) NOT NULL,
  `Amount` int(11) NOT NULL DEFAULT '0',
  `Money` bigint(20) NOT NULL DEFAULT '0',
  `Magic0` int(11) NOT NULL DEFAULT '0',
  `Magic1` int(11) NOT NULL DEFAULT '0',
  `Magic2` int(11) NOT NULL DEFAULT '0',
  `Magic3` int(11) NOT NULL DEFAULT '0',
  `Magic4` int(11) NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tb_shop
-- ----------------------------

-- ----------------------------
-- Table structure for tb_skill
-- ----------------------------
DROP TABLE IF EXISTS `tb_skill`;
CREATE TABLE `tb_skill` (
  `PlayerId` int(11) NOT NULL DEFAULT '0',
  `SkillId` int(11) NOT NULL DEFAULT '0',
  `SkillLevel` int(11) NOT NULL DEFAULT '0',
  `SkillType` int(11) NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tb_skill
-- ----------------------------

-- ----------------------------
-- Table structure for tb_worldchannel
-- ----------------------------
DROP TABLE IF EXISTS `tb_worldchannel`;
CREATE TABLE `tb_worldchannel` (
  `wid` int(11) NOT NULL DEFAULT '0',
  `id` int(11) NOT NULL DEFAULT '0',
  `name` varchar(64) NOT NULL,
  `port` int(11) NOT NULL DEFAULT '0',
  `max_user` int(11) NOT NULL DEFAULT '0',
  `type` int(11) NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_worldchannel
-- ----------------------------
INSERT INTO `tb_worldchannel` VALUES ('1', '1', 'channel 1', '16101', '100', '3');

-- ----------------------------
-- Table structure for tb_worldserver
-- ----------------------------
DROP TABLE IF EXISTS `tb_worldserver`;
CREATE TABLE `tb_worldserver` (
  `id` int(11) NOT NULL DEFAULT '0',
  `name` varchar(64) NOT NULL,
  `ip` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_worldserver
-- ----------------------------
INSERT INTO `tb_worldserver` VALUES ('1', 'yulang', '127.0.0.1');

-- ----------------------------
-- Procedure structure for AUTH_ACCOUNT_BYNAME_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `AUTH_ACCOUNT_BYNAME_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AUTH_ACCOUNT_BYNAME_GET`(IN in_name VARCHAR(32))
BEGIN
		SELECT *
		FROM tb_account
		WHERE `name` = in_name;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for AUTH_ACCOUNT_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `AUTH_ACCOUNT_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AUTH_ACCOUNT_GET`(IN in_name VARCHAR(32),
 IN in_passwd VARCHAR(32))
BEGIN
	SELECT *
	FROM tb_account
	WHERE name = in_name AND passwd = in_passwd;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for AUTH_ACCOUNT_LASTIP_UPDATE
-- ----------------------------
DROP PROCEDURE IF EXISTS `AUTH_ACCOUNT_LASTIP_UPDATE`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AUTH_ACCOUNT_LASTIP_UPDATE`(IN in_uid INT,
 IN in_ipaddress VARCHAR(20),
 OUT out_success INT)
BEGIN
		DECLARE old_ipaddress VARCHAR(20);
		SELECT last_ip INTO old_ipaddress FROM tb_account WHERE id = in_uid;
		SET out_success = 0;

		IF old_ipaddress != in_ipaddress THEN
				UPDATE tb_account SET last_ip = in_ipaddress WHERE id = in_uid;
				SET out_success = 1;
		END IF;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_INVENTORY_ADD
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_INVENTORY_ADD`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_INVENTORY_ADD`(IN in_UID BIGINT, IN in_PID INT,  IN in_ID BIGINT, IN in_Amount INT,IN in_Magic0 INT,IN in_Magic1 INT,IN in_Magic2 INT,IN in_Magic3 INT,IN in_Magic4 INT,IN in_BonusMagic1 INT,IN in_BonusMagic2 INT,IN in_BonusMagic3 INT,IN in_BonusMagic4 INT,IN in_BonusMagic5 INT,IN in_Equiped INT,IN in_Slot INT,IN in_InventoryType INT,IN in_LimitTime INT,IN in_Upgrade INT,IN in_Quality INT,IN in_Lock INT)
BEGIN
	INSERT INTO tb_inventory(
		UniqueID,
		PlayerID,
		ItemID,
		Amount,
		Magic0,
		Magic1,
		Magic2,
		Magic3,
		Magic4,
		BonusMagic1,
		BonusMagic2,
		BonusMagic3,
		BonusMagic4,
		BonusMagic5,
		Equiped,
		Slot,
		InventoryType,
		LimitTime,
		`Upgrade`,
		Quality,
		`Lock`
	) VALUES (
		in_UID,
		in_PID,
		in_ID,
		in_Amount,
		in_Magic0,
		in_Magic1,
		in_Magic2,
		in_Magic3,
		in_Magic4,
		in_BonusMagic1,
		in_BonusMagic2,
		in_BonusMagic3,
		in_BonusMagic4,
		in_BonusMagic5,
		in_Equiped,
		in_Slot,
		in_InventoryType,
		in_LimitTime,
		in_Upgrade,
		in_Quality,
		in_Lock
	);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_INVENTORY_CLEAR
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_INVENTORY_CLEAR`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_INVENTORY_CLEAR`(IN in_PID INT)
BEGIN
	DELETE FROM tb_inventory WHERE PlayerID = in_PID; 
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_INVENTORY_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_INVENTORY_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_INVENTORY_GET`(IN in_PID INT, IN in_Type INT)
BEGIN
	SELECT * FROM tb_inventory WHERE PlayerID=in_PID AND InventoryType=in_Type;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_INVENTORY_UPDATE
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_INVENTORY_UPDATE`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_INVENTORY_UPDATE`(IN in_UID BIGINT, IN in_PID INT,  IN in_ID BIGINT, IN in_Amount INT,IN in_Magic0 INT,IN in_Magic1 INT,IN in_Magic2 INT,IN in_Magic3 INT,IN in_Magic4 INT,IN in_BonusMagic1 INT,IN in_BonusMagic2 INT,IN in_BonusMagic3 INT,IN in_BonusMagic4 INT,IN in_BonusMagic5 INT,IN in_Equiped INT,IN in_Slot INT,IN in_InventoryType INT,IN in_LimitTime INT,IN in_Upgrade INT,IN in_Quality INT,IN in_Lock INT)
BEGIN
	UPDATE tb_inventory 
	SET
		Amount = in_Amount,
		Magic0 = in_Magic0,
		Magic1 = in_Magic1,
		Magic2 = in_Magic2,
		Magic3 = in_Magic3,
		Magic4 = in_Magic4,
		BonusMagic1 = in_BonusMagic1,
		BonusMagic2 = in_BonusMagic2,
		BonusMagic3 = in_BonusMagic3,
		BonusMagic4 = in_BonusMagic4,
		BonusMagic5 = in_BonusMagic5,
		Equiped = in_Equiped,
		Slot = in_Slot,
		InventoryType = in_InventoryType,
		LimitTime = in_LimitTime,
		`Upgrade` = in_Upgrade,
		Quality = in_Quality,
		`Lock` = in_Lock
	WHERE
		UniqueID = in_UID AND 
		PlayerID = in_PID AND
		ItemID = in_ID;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_NPC_SHOP_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_NPC_SHOP_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_NPC_SHOP_GET`(IN in_NpcId INT)
BEGIN
	SELECT * FROM tb_shop WHERE NpcID = in_NpcId;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_ABILITY_ADD
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_ABILITY_ADD`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_ABILITY_ADD`(IN in_PlayerId INT, IN in_AbilityId INT, IN in_AbilityLevel INT)
BEGIN
	INSERT INTO tb_ability (PlayerId, AbilityId, AbilityLevel) VALUES (in_PlayerId, in_AbilityId, in_AbilityLevel);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_ABILITY_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_ABILITY_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_ABILITY_GET`(IN in_PlayerId INT)
BEGIN
	SELECT * FROM tb_ability WHERE PlayerId = in_PlayerId;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_ABILITY_UPDATE
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_ABILITY_UPDATE`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_ABILITY_UPDATE`(IN in_PlayerId INT, IN in_AbilityId INT, IN in_AbilityLevel INT)
BEGIN
	UPDATE tb_ability SET AbilityLevel = in_AbilityLevel WHERE PlayerId = in_PlayerId AND AbilityId = in_AbilityId;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_CREATE
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_CREATE`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_CREATE`(IN in_AccountID INT,
		IN in_ServerID INT,
		IN in_Name VARCHAR(15),
		IN in_Level INT,
		IN in_Job INT,
		IN in_Title INT,
		IN in_Map INT,
		IN in_X FLOAT,
		IN in_Y FLOAT,
		IN in_Z FLOAT,
		IN in_Gender INT,
		IN in_Forces INT,
		IN in_Hair INT,
		IN in_Color INT,
		IN in_Face INT,
		IN in_Voice INT,
		IN in_Hp INT,
		IN in_Mp INT,
		IN in_Sp INT,
		IN in_Exp BIGINT,
		IN in_SkillPoint INT,		IN in_AbilityPoint INT,		OUT out_Id INT)
BEGIN
		INSERT INTO tb_player (account, server_id, name, level, job, title, map, x, y, z, gender, forces, hair, color, face, voice, hp, mp, sp, exp, skillpoint, abilitypoint)
		VALUES (in_AccountID, in_ServerID, in_Name, in_Level, in_Job, in_Title, in_Map, in_X, in_Y, in_Z, in_Gender, in_Forces, in_Hair, in_Color, in_Face, in_Voice, in_Hp, in_Mp, in_Sp, in_Exp, in_SkillPoint, in_AbilityPoint);
		
		SET out_Id = LAST_INSERT_ID();
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_GET_ALL
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_GET_ALL`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_GET_ALL`(IN in_AccID INT, IN in_SrvID INT)
BEGIN
		SELECT * 
		FROM tb_player
		WHERE account = in_AccID AND server_id = in_SrvID;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_MONEY_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_MONEY_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_MONEY_GET`(IN in_PID INT,
	OUT out_Money BIGINT)
BEGIN
	SELECT money INTO out_Money FROM tb_player WHERE id = in_PID;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_MONEY_UPDATE
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_MONEY_UPDATE`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_MONEY_UPDATE`(IN in_PID INT, IN in_AMOUNT BIGINT)
BEGIN
	UPDATE tb_player 
	SET 
		money = in_AMOUNT 
	WHERE 
		id = in_PID;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_NAME_CHECK
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_NAME_CHECK`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_NAME_CHECK`(IN in_name VARCHAR(32), OUT out_result INT)
BEGIN

	DECLARE _exists INT;

	SELECT COUNT(*) INTO _exists
	FROM tb_player
	WHERE name = in_name;

	IF _exists > 0 THEN
		SET out_result = 0;
	ELSE
		SET out_result = 1;
	END IF;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_SAVE
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_SAVE`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_SAVE`(IN in_Pid INT,
	IN in_Level INT,
	IN in_Job INT,
	IN in_Title INT,
	IN in_Map INT,
	IN in_X FLOAT,
	IN in_Y FLOAT,
	IN in_Z FLOAT,
	IN in_Forces INT,
	IN in_Hair INT,
	IN in_Color INT,
	IN in_Face INT,
	IN in_Voice INT,
	IN in_Hp INT,
	IN in_Mp INT,
	IN in_Sp INT,
	IN in_Exp BIGINT,	IN in_SkillPoint INT,	IN in_AbilityPoint INT)
BEGIN
	UPDATE tb_player SET
		`level` = in_Level,
		job = in_Job,
		title = in_Title,
		map = in_Map,
		x = in_X,
		y = in_Y,
		z = in_Z,
		forces = in_Forces,
		hair = in_Hair,
		color = in_Color,
		face = in_Face,
		voice = in_Voice,
		hp = in_Hp,
		mp = in_Mp,
		sp = in_Sp,
		exp = in_Exp,
		skillpoint = in_SkillPoint,
		abilitypoint = in_AbilityPoint
	WHERE id = in_Pid;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_SKILL_ADD
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_SKILL_ADD`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_SKILL_ADD`(IN in_PlayerId INT, IN in_SkillId INT,IN in_SkillLevel INT,IN in_SkillType INT)
BEGIN
	INSERT INTO tb_skill (PlayerId, SkillId, SkillLevel, SkillType) VALUES (in_PlayerId, in_SkillId, in_SkillLevel, in_SkillType);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_SKILL_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_SKILL_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_SKILL_GET`(IN in_PlayerId INT,IN in_SkillType INT)
BEGIN
	SELECT * FROM tb_skill WHERE PlayerId = in_PlayerId AND SkillType = in_SkillType;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GAME_PLAYER_SKILL_UPDATE
-- ----------------------------
DROP PROCEDURE IF EXISTS `GAME_PLAYER_SKILL_UPDATE`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GAME_PLAYER_SKILL_UPDATE`(IN in_PlayerId INT, IN in_SkillId INT, IN in_SkillLevel INT)
BEGIN
	UPDATE tb_skill SET SkillLevel = in_SkillLevel WHERE PlayerId = in_PlayerId AND SkillId = in_SkillId;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for SRV_CHANNEL_INFO_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `SRV_CHANNEL_INFO_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SRV_CHANNEL_INFO_GET`(IN worldId INT)
BEGIN
    SELECT *
    FROM tb_worldchannel
    WHERE wid = worldId;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for SRV_SERVER_INFO_GET
-- ----------------------------
DROP PROCEDURE IF EXISTS `SRV_SERVER_INFO_GET`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SRV_SERVER_INFO_GET`(IN svId INT)
BEGIN
    SELECT *
    FROM tb_worldserver
    WHERE id = svId;
END
;;
DELIMITER ;

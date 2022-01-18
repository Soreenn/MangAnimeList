-- --------------------------------------------------------
-- Hôte:                         127.0.0.1
-- Version du serveur:           8.0.26 - MySQL Community Server - GPL
-- SE du serveur:                Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Listage de la structure de la base pour manganimelist
DROP DATABASE IF EXISTS `manganimelist`;
CREATE DATABASE IF NOT EXISTS `manganimelist` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `manganimelist`;

-- Listage de la structure de la table manganimelist. animes
DROP TABLE IF EXISTS `animes`;
CREATE TABLE IF NOT EXISTS `animes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `state` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'In progress',
  `season` int DEFAULT NULL,
  `episode` int DEFAULT NULL,
  `note` int DEFAULT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table manganimelist.animes : ~1 rows (environ)
/*!40000 ALTER TABLE `animes` DISABLE KEYS */;
INSERT INTO `animes` (`id`, `name`, `state`, `season`, `episode`, `note`, `user_id`) VALUES
	(1, 'Oni Chichi: Refresh♥', 'In progress', NULL, NULL, NULL, 1),
	(2, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(3, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(4, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(5, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(6, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(7, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(8, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(9, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(10, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(11, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(12, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(13, 'Huyao Xiao Hongniang: Wangquan Pian', 'In progress', NULL, NULL, NULL, 1),
	(14, 'Baka na Imouto wo Rikou ni Suru no wa Ore no xx dake na Ken ni Tsuite', 'In progress', NULL, NULL, NULL, 1);
/*!40000 ALTER TABLE `animes` ENABLE KEYS */;

-- Listage de la structure de la table manganimelist. mangas
DROP TABLE IF EXISTS `mangas`;
CREATE TABLE IF NOT EXISTS `mangas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `state` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'In progress',
  `volume` int DEFAULT NULL,
  `chapter` int DEFAULT NULL,
  `note` int DEFAULT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table manganimelist.mangas : ~0 rows (environ)
/*!40000 ALTER TABLE `mangas` DISABLE KEYS */;
INSERT INTO `mangas` (`id`, `name`, `state`, `volume`, `chapter`, `note`, `user_id`) VALUES
	(4, 'Retsujou no Meikyuu', 'In progress', NULL, NULL, NULL, 1);
/*!40000 ALTER TABLE `mangas` ENABLE KEYS */;

-- Listage de la structure de la table manganimelist. media
DROP TABLE IF EXISTS `media`;
CREATE TABLE IF NOT EXISTS `media` (
  `id` int NOT NULL,
  `title_romaji` varchar(255) NOT NULL,
  `title_native` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `tags` varchar(255) DEFAULT NULL,
  `status` varchar(50) NOT NULL,
  `release_year` int NOT NULL,
  `cover` varchar(400) NOT NULL,
  `type` varchar(50) DEFAULT NULL,
  `episodes` int DEFAULT NULL,
  `average_score` int NOT NULL,
  `volumes` int DEFAULT NULL,
  `chapters` int DEFAULT NULL,
  `bannerImage` varchar(400) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table manganimelist.media : ~0 rows (environ)
/*!40000 ALTER TABLE `media` DISABLE KEYS */;
/*!40000 ALTER TABLE `media` ENABLE KEYS */;

-- Listage de la structure de la table manganimelist. users
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `password` varchar(30) NOT NULL,
  `userType` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table manganimelist.users : ~0 rows (environ)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`id`, `username`, `password`, `userType`) VALUES
	(1, 'admin', '1234', 2),
	(3, 'Luke', '12', 1);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

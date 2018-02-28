-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Mar 01, 2018 at 12:19 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `good_games`
--
CREATE DATABASE IF NOT EXISTS `good_games` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `good_games`;

-- --------------------------------------------------------

--
-- Table structure for table `games`
--

CREATE TABLE `games` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `genre` varchar(255) NOT NULL,
  `system` varchar(255) NOT NULL,
  `release_year` int(4) NOT NULL,
  `rating` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

--
-- Dumping data for table `games`
--

INSERT INTO `games` (`id`, `name`, `genre`, `system`, `release_year`, `rating`) VALUES
(1, 'Super Mario 64', 'Platformer', 'Nintendo 64', 1996, 97),
(3, 'Kingdom Hearts', 'Role-Playing Game', 'PS2', 2002, 85),
(4, 'Putt Putt Saves the Zoo', 'Family', 'PC', 1995, 100);

-- --------------------------------------------------------

--
-- Table structure for table `retailers`
--

CREATE TABLE `retailers` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `website` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `retailers`
--

INSERT INTO `retailers` (`id`, `name`, `website`) VALUES
(2, 'GameStop', 'www.gamestop.com'),
(3, 'Amazon', 'www.amazon.com'),
(4, 'GameCrazy', 'www.gamecrazy.com'),
(5, 'EB Games', 'www.ebgames.ca'),
(6, 'Walmart', 'www.walmart.com/cp/video-games/2636');

-- --------------------------------------------------------

--
-- Table structure for table `retailers_games`
--

CREATE TABLE `retailers_games` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `retailer_id` int(11) NOT NULL,
  `game_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `retailers_games`
--

INSERT INTO `retailers_games` (`id`, `retailer_id`, `game_id`) VALUES
(5, 3, 0),
(6, 4, 0),
(7, 5, 0),
(8, 5, 3),
(9, 4, 4),
(10, 2, 1),
(11, 3, 4);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `games`
--
ALTER TABLE `games`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `retailers`
--
ALTER TABLE `retailers`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `retailers_games`
--
ALTER TABLE `retailers_games`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `games`
--
ALTER TABLE `games`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `retailers`
--
ALTER TABLE `retailers`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `retailers_games`
--
ALTER TABLE `retailers_games`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

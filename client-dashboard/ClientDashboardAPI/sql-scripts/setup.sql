-- SQL setup scripts go here
CREATE DATABASE IF NOT EXISTS ClientDashboardDB;
USE ClientDashboardDB;

CREATE TABLE IF NOT EXISTS Client (
	id INT AUTO_INCREMENT PRIMARY KEY,
	username VARCHAR(255) UNIQUE NOT NULL,
	email VARCHAR(255) UNIQUE NOT NULL,
	archived DATETIME
	);

CREATE TABLE IF NOT EXISTS ClientPhone (
	id INT AUTO_INCREMENT PRIMARY KEY,
	clientid INT NOT NULL,
	phonenumber VARCHAR(20) NOT NULL
	);

INSERT IGNORE INTO Client (username, email, archived) VALUES
('vinny', 'vinny@test.com', NULL),
('joel', 'joel@test.com', NULL),
('jose', 'jose@test.com', '2025-01-30 00:00:00');

INSERT IGNORE INTO ClientPhone (clientid, phonenumber) VALUES
(1, '1111111'),
(1, '2222222'),
(2, '3333333'),
(3, '4444444');
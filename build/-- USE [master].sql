-- USE [master]
-- GO
-- create database [Pitcher8] ON
-- (FileName='C:\Users\Jordan\Pitcher8.mdf'),
-- (FileName='C:\Users\Jordan\Pitcher8_log.ldf')
-- FOR ATTACH;

-- CREATE DATABASE [Database] On PRIMARY
-- (NAME = N'Pitcher8',FILENAME = N'C:\Users\Jordan\Pitcher8.mdf')
-- LOG ON
-- (NAME = N'Pitcher8_log', FILENAME = N'C:\Users\Jordan\Pitcher8_log.ldf')
-- GO

sp_detach_db 'C:\Users\Jordan\Pitcher7.mdf'
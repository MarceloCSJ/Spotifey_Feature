CREATE DATABASE ArtistConnect;
USE ArtistConnect;

CREATE TABLE Artistas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Bio TEXT,
    GeneroMusical VARCHAR(50)
);

CREATE TABLE Musicas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Titulo VARCHAR(100) NOT NULL,
    ArtistaResponsavel VARCHAR(100),
    Duracao VARCHAR(10)
);

INSERT INTO Artistas (Nome, Bio, GeneroMusical) VALUES
('Mateus Auroura','Sou um artista independente, tenho 27 anos e amo fazer música...','Indie'),
('Julia Martins','Sou cantora e compositora independente. Escrevo músicas desde a adolescência e recentemente comecei...','Gospel'),
('Jorge Santos','Artista Indie de 23 anos, novo single "Espera" tenta reproduzir...','Sertanejo'),
('Thiago Mendes','Thiago "Thiagão" Mendes é um artista emergente de 25 anos que ama curtição e música boa...','Indie'),
('Mariana Fernandes','Mari Fernandes é o novo nome para a música gospel...','Gospel');

INSERT INTO Musicas (Titulo, ArtistaResponsavel, Duracao) VALUES
('Sol no horizonte','Mateus Auroura','03:53'),
('Deus no Controle','Julia Martins','02:59'),
('Espera','Jorge Santos','04:34'),
('Vibe Final','Thiagão','05:21'),
('Águas Tranquilas','Mari Fernandes','03:16');
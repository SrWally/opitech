
-- 1. Tabla Departamentos
CREATE TABLE Departamentos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL
);

-- 2. Tabla Ciudades
CREATE TABLE Ciudades (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    DepartamentoId INTEGER NOT NULL,
    FOREIGN KEY (DepartamentoId) REFERENCES Departamentos(Id)
);

-- 3. Tabla TiposSiniestro
CREATE TABLE TiposSiniestro (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Descripcion TEXT
);

-- 4. Tabla Siniestros
CREATE TABLE Siniestros (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FechaHora DATETIME NOT NULL,
    CiudadId INTEGER NOT NULL,
    TipoSiniestroId INTEGER NOT NULL,
    VehiculosInvolucrados INTEGER NOT NULL CHECK (VehiculosInvolucrados >= 1),
    NumeroVictimas INTEGER NOT NULL CHECK (NumeroVictimas >= 0),
    Descripcion TEXT,
    FechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CiudadId) REFERENCES Ciudades(Id),
    FOREIGN KEY (TipoSiniestroId) REFERENCES TiposSiniestro(Id)
);

-- Índices para mejor performance
CREATE INDEX IX_Siniestros_FechaHora ON Siniestros(FechaHora);
CREATE INDEX IX_Siniestros_CiudadId ON Siniestros(CiudadId);
CREATE INDEX IX_Siniestros_TipoSiniestroId ON Siniestros(TipoSiniestroId);
CREATE INDEX IX_Ciudades_DepartamentoId ON Ciudades(DepartamentoId);
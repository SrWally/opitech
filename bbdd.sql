
-- 1. Departamentos (NO especificar Id si es autoincremental)
INSERT INTO Departamentos (Nombre) VALUES 
('Cundinamarca'),
('Bogota'),
('Bolivar'),
('Santander'),
('Nariño');

-- 2. Ciudades
INSERT INTO Ciudades (Nombre, DepartamentoId) VALUES
('Cajica', 1),
('Chia', 1),
('Bogota', 2),
('Cartagena', 3),
('Turbaco', 3),
('Bucaramanga', 4),
('Pasto', 5),
('Barichara', 4),
('Zipaquira', 1);

-- 3. TiposSiniestro
INSERT INTO TiposSiniestro (Nombre, Descripcion) VALUES
('Colisión', 'Choque entre vehículos'),
('Atropello', 'Persona atropellada por vehículo'),
('Vuelco', 'Vehículo volcado'),
('Incendio', 'Vehículo incendiado'),
('Caída', 'Caída de ocupante del vehículo'),
('Otro', 'Otro tipo de siniestro');

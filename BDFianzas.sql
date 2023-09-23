-- Crear la base de datos
CREATE DATABASE IF NOT EXISTS BD_Fianzas;

-- Seleccionar la base de datos
USE BD_Fianzas;

-- Tabla de Clientes
CREATE TABLE IF NOT EXISTS clientes (
    id_user INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    dni VARCHAR(8) NOT NULL,
    telefono VARCHAR(9) NOT NULL,
    correo_electronico VARCHAR(100) NOT NULL,
    contrasena VARCHAR(32) NOT NULL
);

-- Tabla de Ofertas Vehiculares
CREATE TABLE IF NOT EXISTS ofertas_vehiculares (
    id INT AUTO_INCREMENT PRIMARY KEY,
    marca VARCHAR(100) NOT NULL,
    modelo VARCHAR(100) NOT NULL,
    precio DECIMAL(10, 2) NOT NULL,
    van DECIMAL(10, 2) NOT NULL,
    tir DECIMAL(5, 2) NOT NULL
);

-- Tabla de Préstamos
CREATE TABLE IF NOT EXISTS prestamos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cliente_id INT NOT NULL,
    oferta_vehicular_id INT NOT NULL,
    moneda ENUM('Soles', 'Dólares') NOT NULL,
    tasa_interes DECIMAL(5, 2) NOT NULL,
    plazo INT NOT NULL,
    periodo_gracia INT DEFAULT 0,
    FOREIGN KEY (oferta_vehicular_id) REFERENCES ofertas_vehiculares(id),
    FOREIGN KEY (cliente_id) REFERENCES clientes(id_user)
);

-- Tabla de Configuraciones
CREATE TABLE IF NOT EXISTS configuraciones (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cliente_id INT NOT NULL,
    moneda ENUM('Soles', 'Dólares') NOT NULL,
    tipo_tasa ENUM('Nominal', 'Efectiva') NOT NULL,
    plazo_gracia INT DEFAULT 0,
    FOREIGN KEY (cliente_id) REFERENCES clientes(id_user)
);

-- Tabla de Pagos
CREATE TABLE IF NOT EXISTS pagos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    prestamo_id INT NOT NULL,
    numero_cuota INT NOT NULL,
    monto_cuota DECIMAL(10, 2) NOT NULL,
    fecha_pago DATE NOT NULL,
    estado ENUM('Pendiente', 'Pagado') NOT NULL DEFAULT 'Pendiente',
    FOREIGN KEY (prestamo_id) REFERENCES prestamos(id)
);
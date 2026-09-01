-- ===============================================
-- SCRIPT DE SEEDING - GESTIÓN GIMNASIO
-- Inserta todos los datos iniciales necesarios
-- ===============================================

-- Limpiar datos existentes (opcional, comentar si no se desea)
-- DELETE FROM dbo.Roles;
-- DELETE FROM dbo.Usuarios;

-- ===============================================
-- 1. INSERTAR ROLES (TipoRol Enum)
-- ===============================================
SET IDENTITY_INSERT dbo.Roles ON;

INSERT INTO dbo.Roles (Id, Tipo) VALUES 
	(1, 1),  -- Admin
	(2, 2);  -- PersonalTrainer

SET IDENTITY_INSERT dbo.Roles OFF;

PRINT 'Roles insertados correctamente';

-- ===============================================
-- 2. INSERTAR USUARIOS DE PRUEBA
-- ===============================================
SET IDENTITY_INSERT dbo.Usuarios ON;

INSERT INTO dbo.Usuarios (Id, Nombre, Apellido, Email, Contrasena, Activo, IdRol) VALUES
	(1, 'Admin', 'Sistema', 'admin@gimnasio.com', 'admin123', 1, 1),
	(2, 'Juan', 'Medina', 'juan@gimnasio.com', 'juan123', 1, 2),
	(3, 'María', 'García', 'maria@gimnasio.com', 'maria123', 1, 2);

SET IDENTITY_INSERT dbo.Usuarios OFF;

PRINT 'Usuarios insertados correctamente';

-- ===============================================
-- NOTAS SOBRE ENUMS DISPONIBLES:
-- ===============================================
-- TipoRol:
--   1 = Admin
--   2 = PersonalTrainer

-- EstadoCliente:
--   1 = Activo
--   2 = Inactivo
--   3 = Baja

-- EstadoPago:
--   1 = AlDia
--   2 = Vencido
--   3 = EnTolerancia

-- MetodoPago:
--   1 = Efectivo
--   2 = Transferencia

-- PlanPase:
--   1 = Libre
--   2 = TresDias

-- PlanTipo:
--   1 = Mensual
--   2 = Semanal

-- PlanValidez:
--   30 = TreintaDias
--   3 = TresDias

-- TipoNotificacion:
--   1 = Email
--   2 = WhatsApp

PRINT '';
PRINT '========================================';
PRINT 'SEEDING COMPLETADO EXITOSAMENTE';
PRINT '========================================';
PRINT 'Datos insertados:';
PRINT '  - 2 Roles (Admin, PersonalTrainer)';
PRINT '  - 3 Usuarios de prueba';
PRINT '';
PRINT 'Credenciales de prueba:';
PRINT '  - Admin: admin@gimnasio.com / admin123';
PRINT '  - PT 1: juan@gimnasio.com / juan123';
PRINT '  - PT 2: maria@gimnasio.com / maria123';
PRINT '========================================';

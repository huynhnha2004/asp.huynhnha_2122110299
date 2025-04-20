INSERT INTO Users (FullName, Email, Phone, Password, Role, Avatar, CreatedBy, UpdatedBy, DeletedBy, CreatedAt, UpdatedAt, DeletedAt)
VALUES 
('Nguyen Van A', 'phat@gmail.com', '0123456789', 'pass123', 'Admin', 'avatar1.jpg', 1, 1, NULL, GETDATE(), NULL, NULL),
('Tran Thi B', 'tranthib@example.com', '0987654321', 'password456', 'User', 'avatar2.jpg', 1, NULL, NULL, GETDATE(), NULL, NULL),
('Le Minh C', 'leminhc@example.com', '0912345678', 'password789', 'User', 'avatar3.jpg', 1, 2, NULL, GETDATE(), NULL, NULL),
('Pham Thi D', 'phamthid@example.com', '0901234567', 'password101', 'Admin', 'avatar4.jpg', 2, NULL, NULL, GETDATE(), NULL, NULL);

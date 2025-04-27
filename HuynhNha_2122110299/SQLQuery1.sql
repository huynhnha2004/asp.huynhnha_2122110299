INSERT INTO Users (FirstName, LastName, FullName, Email, Phone, Password, Role, Avatar, CreatedBy, UpdatedBy, DeletedBy, CreatedAt, UpdatedAt, DeletedAt)
VALUES 
('huynh', 'nha', 'Huynh NHa ', 'nha@gmail.com', '0123456789', 'pass123', 'Admin', 'avatar1.jpg', 1, 1, NULL, GETDATE(), NULL, NULL),
('Thi', 'B', 'Tran Thi B', 'lisa02@mail.com', '0987654321', 'pass456', 'admin ', 'avatar2.jpg', 1, NULL, NULL, GETDATE(), NULL, NULL),
('Minh', 'C', 'Le Minh C', 'leminhc@example.com', '0912345678', 'password789', 'User', 'avatar3.jpg', 1, 2, NULL, GETDATE(), NULL, NULL),
('Thi', 'D', 'Pham Thi D', 'phamthid@example.com', '0901234567', 'password101', 'Admin', 'avatar4.jpg', 2, NULL, NULL, GETDATE(), NULL, NULL);
  select*from users
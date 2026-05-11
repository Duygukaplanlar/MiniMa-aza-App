USE MiniMagazaDb;
GO
USE MiniMagazaDb;
GO

-- Yiyecek ürünleri
INSERT INTO Products (Name, Price, Stock, Category, Description)
VALUES 
('Çikolatalı Gofret', 15.00, 100, 'Yiyecek', 'Taze ve çıtır gofret'),
('Sütlü Kahve', 85.00, 50, 'Yiyecek', 'Yumuşak içimli kahve');

-- Giyim ürünleri
INSERT INTO Products (Name, Price, Stock, Category, Description)
VALUES 
('Pamuklu Tişört', 350.00, 30, 'Giyim', '100% Pamuklu rahat kesim'),
('Mavi Kot Pantolon', 900.00, 20, 'Giyim', 'Slim fit kot pantolon');
GO
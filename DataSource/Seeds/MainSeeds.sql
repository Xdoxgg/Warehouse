INSERT INTO tbl_item_type (type_name, description)
VALUES ('Электроника', 'Устройства и гаджеты'),
       ('Одежда', 'Предметы гардероба'),
       ('Книги', 'Печатные издания');

-- Заполнение записей
INSERT INTO tbl_record (date_entrance)
VALUES ('2025-06-01'),
       ('2025-06-02'),
       ('2025-06-03');

-- Заполнение товаров
INSERT INTO tbl_item (cost, description, to_date, FK_type_id, FK_record_id)
VALUES (1500.00, 'Ноутбук ASUS', NULL, 1, 1),
       (99.99, 'Футболка черная', '2025-12-31', 2, 2),
       (29.99, 'Книга "Программирование в SQL"', '2025-07-10', 3, 3);
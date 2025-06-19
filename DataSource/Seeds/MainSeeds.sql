INSERT INTO tbl_item_type (type_name, description)
VALUES ('Электроника', 'Устройства и гаджеты'),
       ('Одежда', 'Предметы гардероба'),
       ('Книги', 'Печатные издания');

-- Заполнение записей
INSERT INTO tbl_record (date_entrance)
VALUES ('2025-06-01'),
       ('2025-06-02'),
       ('2025-06-03');

INSERT INTO tbl_creator (name)
VALUES ('company1'),
       ('company2');

-- Заполнение товаров
INSERT INTO tbl_item (name, cost, description, to_date, FK_type_id, FK_record_id, is_send, is_reverted, FK_creator_id)
VALUES ('name1', 1500.00, 'Ноутбук ASUS', NULL, 1, 1, 0, 0,1),
       ('name2', 99.99, 'Футболка черная', '2025-12-31', 2, 2, 0, 0,2),
       ('name3', 29.99, 'Книга "Программирование в SQL"', '2025-07-10', 3, 3, 0, 0,1);


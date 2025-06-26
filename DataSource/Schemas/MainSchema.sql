DROP TABLE IF EXISTS tbl_item;
DROP TABLE IF EXISTS tbl_creator;
DROP TABLE IF EXISTS tbl_record;
DROP TABLE IF EXISTS tbl_item_type;
DROP TABLE IF EXISTS tbl_users;



CREATE TABLE tbl_users
(
    PK_user_id    INT PRIMARY KEY IDENTITY (1,1),
    user_name     VARCHAR(255) NOT NULL,
    user_password VARCHAR(255) NOT NULL,
    type          BIT          NOT NULL
);


CREATE TABLE tbl_item_type
(
    PK_type_id  INT PRIMARY KEY IDENTITY (1,1),
    type_name   NVARCHAR(255) NOT NULL,
    description NVARCHAR(255) NOT NULL--?
);



CREATE TABLE tbl_record
(
    PK_record_id  INT PRIMARY KEY IDENTITY (1,1),
    date_entrance DATE NOT NULL UNIQUE
);


CREATE TABLE tbl_creator
(
    PK_creator_id INT PRIMARY KEY IDENTITY (1,1),
    name          NVARCHAR(255) NOT NULL UNIQUE,
);

CREATE TABLE tbl_item
(
    PK_item_id    INT PRIMARY KEY IDENTITY (1,1),
    name          NVARCHAR(255) NOT NULL,
    cost          FLOAT         NOT NULL,
    description   NVARCHAR(255) NOT NULL,
    to_date       DATE,
    FK_type_id    INT           FOREIGN KEY REFERENCES tbl_item_type (PK_type_id) ON DELETE SET NULL,
    FK_record_id  INT           FOREIGN KEY REFERENCES tbl_record (PK_record_id) ON DELETE SET NULL,
    is_send       BIT           NOT NULL,
    is_reverted   BIT           NOT NULL,
    FK_creator_id INT           FOREIGN KEY REFERENCES tbl_creator (PK_creator_id) ON DELETE SET NULL,
);

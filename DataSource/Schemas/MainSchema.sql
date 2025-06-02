DROP TABLE IF EXISTS tbl_users;

CREATE TABLE tbl_users
(
    PK_user_id INT PRIMARY KEY IDENTITY(1,1),
    user_name VARCHAR(255) NOT NULL,
    user_password VARCHAR(255) NOT NULL
);


CREATE TABLE tbl_items(
    PK_item_id INT PRIMARY KEY IDENTITY(1,1),
    cost FLOAT NOT NULL,
    item_count INT NOT NULL,
    description NVARCHAR(255) NOT NULL,
    to_date DATE,
    date_entrance DATE NOT NULL
    
    
);

CREATE TABLE tbl_record (
      
    
    
);
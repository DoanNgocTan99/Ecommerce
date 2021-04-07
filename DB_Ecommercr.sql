CREATE DATABASE DBEcommerce
USE EcommerceDB

CREATE TABLE Category(
	category_id BIGINT PRIMARY KEY IDENTITY,
	cate_name NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	cate_description NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	isActive BIT NOT NULL DEFAULT 0,
	categorycol NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP
)

CREATE TABLE role(
	role_id BIGINT PRIMARY KEY IDENTITY,
	role_name NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	description NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP
)

CREATE TABLE shop(
	shop_id BIGINT PRIMARY KEY IDENTITY,
	shop_name NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	shop_description NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	shop_address NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	shop_phone NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP 
)

CREATE TABLE Userr(
	user_id BIGINT PRIMARY KEY IDENTITY,
	name NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	username NVARCHAR(250) NOT NULL DEFAULT 'defaut' UNIQUE,
	password NVARCHAR(250) NOT NULL DEFAULT'123',
	email NVARCHAR(250) NOT NULL DEFAULT'defaut',
	phone NVARCHAR(250) NOT NULL DEFAULT'defaut',
	gender BIT  DEFAULT 0,
	dob DATETIME DEFAULT CURRENT_TIMESTAMP,
	address NVARCHAR(250) NOT NULL DEFAULT'defaut',
	isActive BIT  DEFAULT 0,
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP ,
	role_id_user BIGINT NOT NULL,
	FOREIGN KEY (role_id_user) REFERENCES dbo.role(role_id)
	ON UPDATE CASCADE
	ON DELETE CASCADE,
	shop_id_user BIGINT NOT NULL,
	FOREIGN KEY (shop_id_user) REFERENCES dbo.shop(shop_id)
	ON UPDATE CASCADE
	ON DELETE CASCADE
)
CREATE TABLE Product(
	prod_id BIGINT PRIMARY KEY IDENTITY,
	prod_name NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	prod_description NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	brand NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	material NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	origin NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	prod_price DECIMAL(2,0) NOT NULL,
	del_price DECIMAL(2,0) NOT NULL,
	warrantyDate DATETIME DEFAULT  CURRENT_TIMESTAMP ,
	Stock INT DEFAULT 1 UNIQUE,
	Discount INT DEFAULT 1,
	Views INT DEFAULT 1,
	rate INT DEFAULT 1,
	isActive BIT DEFAULT 0,
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP ,
	cate_id_prod BIGINT NOT NULL,
	FOREIGN KEY(cate_id_prod) REFERENCES dbo.Category(category_id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	shop_id_prod BIGINT NOT NULL,
	FOREIGN KEY (shop_id_prod) REFERENCES dbo.shop(shop_id) 
	ON UPDATE CASCADE
	ON DELETE CASCADE
)
CREATE TABLE transactionn(
	transaction_id BIGINT PRIMARY KEY IDENTITY,
	address NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	amount NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	status NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP ,
	user_id_transaction BIGINT NOT NULL,
	FOREIGN KEY (user_id_transaction) REFERENCES dbo.Userr(user_id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	prod_id_transaction BIGINT NOT NULL,
	FOREIGN KEY (prod_id_transaction) REFERENCES dbo.Product(prod_id),
	shop_id_transaction BIGINT NOT NULL,
	FOREIGN KEY (shop_id_transaction) REFERENCES dbo.shop(shop_id)
)
CREATE TABLE payment(
	payment_id BIGINT PRIMARY KEY IDENTITY,
	payment_name NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	payment_type NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	allow BIT NOT NULL DEFAULT 0,
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP 

)
CREATE TABLE image (
	image_id BIGINT PRIMARY KEY IDENTITY,
	path VARCHAR(250) NOT NULL ,
	prod_id_img BIGINT ,
	cate_id_img BIGINT ,
	user_id_img BIGINT,
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP ,
	FOREIGN KEY (prod_id_img) REFERENCES dbo.Product(prod_id),
	FOREIGN KEY (cate_id_img ) REFERENCES dbo.Category(category_id),
	FOREIGN KEY (user_id_img) REFERENCES dbo.Userr(user_id)
)
CREATE TABLE product_advertising(
	product_advertising_id BIGINT  PRIMARY KEY IDENTITY,
	prod_id_adver BIGINT NOT NULL,
	shop_id_adver BIGINT NOT NULL,
	img_id_adver BIGINT NOT NULL,
	content TEXT  ,
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP ,
	FOREIGN KEY (prod_id_adver) REFERENCES dbo.Product(prod_id),
	FOREIGN KEY (shop_id_adver ) REFERENCES dbo.shop(shop_id),
	FOREIGN KEY (img_id_adver) REFERENCES dbo.image(image_id)
)
CREATE TABLE Orderr(
	order_id BIGINT PRIMARY KEY IDENTITY,
	total DECIMAL(2,0) ,
	isDelivery BIT DEFAULT 0,
	ordercol NVARCHAR(250) DEFAULT 'defaut' NOT NULL,
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP ,
	shop_id_order BIGINT NOT NULL,
	user_id_order BIGINT NOT NULL,
	FOREIGN KEY (shop_id_order ) REFERENCES dbo.shop(shop_id),
	FOREIGN KEY (user_id_order) REFERENCES dbo.Userr(user_id)
) 
CREATE TABLE order_detail(
	order_detail_id BIGINT PRIMARY KEY IDENTITY,

	order_id_detail BIGINT NOT NULL,
	prod_id_detail BIGINT NOT NULL,
	shop_id_detail BIGINT NOT NULL,
	payment_id_detail BIGINT NOT NULL,
	user_id_detail BIGINT NOT NULL,

	messagee NVARCHAR(250)  NOT NULL DEFAULT 'defaut',
	createdBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedBy NVARCHAR(250) NOT NULL DEFAULT 'defaut',
	modifiedDate DATETIME  DEFAULT CURRENT_TIMESTAMP,
	createdDate DATETIME  DEFAULT CURRENT_TIMESTAMP ,

    FOREIGN KEY (order_id_detail) REFERENCES dbo.Orderr(order_id),
	FOREIGN KEY (shop_id_detail ) REFERENCES dbo.shop(shop_id),
	FOREIGN KEY (prod_id_detail) REFERENCES dbo.Product(prod_id),
	FOREIGN KEY (payment_id_detail) REFERENCES dbo.payment(payment_id),
	FOREIGN KEY (user_id_detail ) REFERENCES dbo.Userr(user_id)
	
)
create table users
(
	id bigint unsigned not null auto_increment,
    primary key (id),
    
	is_admin bit not null,
    username blob not null,
    password blob not null,
    email blob not null,
    firstname varchar (100) not null,
    lastname varchar (100) not null,
    dob date not null,
    country varchar (100) not null,
    phone varchar (20) not null,
    created timestamp not null default current_timestamp ()
)
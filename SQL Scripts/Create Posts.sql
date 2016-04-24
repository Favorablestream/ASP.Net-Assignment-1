create table posts
(
	id bigint unsigned not null auto_increment,
    primary key (id),
    author_id bigint unsigned not null,
    foreign key (author_id)
		references users (id)
        on delete cascade,
    
	is_available bit not null,
	title varchar (100) not null,
    description varchar (100) not null,
    text varchar (20000) not null,
    posted timestamp not null default current_timestamp ()
)
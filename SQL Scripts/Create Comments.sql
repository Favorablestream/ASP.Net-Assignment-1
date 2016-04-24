create table comments
(
	id bigint unsigned not null auto_increment,
    primary key (id),
    post_id bigint unsigned not null,
    foreign key (post_id)
		references posts (id)
        on delete cascade,
    author_id bigint unsigned not null,
    foreign key (author_id)
		references users (id)
        on delete cascade,
        
	text varchar (20000) not null,
    posted timestamp not null default current_timestamp ()
)

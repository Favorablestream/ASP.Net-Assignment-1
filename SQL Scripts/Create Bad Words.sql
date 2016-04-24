create table badwords 
(
	id bigint unsigned not null auto_increment,
    primary key (id),
        
	word varchar (50) not null,
    added timestamp not null default current_timestamp ()
)
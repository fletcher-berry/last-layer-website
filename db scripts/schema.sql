create schema cubing_algs_new;

create table general_source(
	id int primary key auto_increment,
    description varchar(255)
);

create table source(
	id int primary key auto_increment,
    description varchar(255) not null,
    url varchar(1023),
    general_source_id int not null,
    foreign key (general_source_id) references general_source(id)
);

create table alg_case(
	id int primary key auto_increment,
    type varchar(15) not null,
    case_number int not null,
    super_case_id int,
    foreign key (super_case_id) references alg_case(id)
);

create table algorithm(
	id int primary key auto_increment,
    case_id int not null,
    source_id int,
    alg varchar(255),
    modified tinyint(1)
);
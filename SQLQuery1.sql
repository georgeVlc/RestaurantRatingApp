insert into Restaurant
values ('The italian', 0, './the_italian_image.png', 'italian', 'description for the italian restaurant'),
		('The Mexican', 0, './the_mexican_image.png', 'Mexican', 'description for the mexican restaurant'),
		('The greek', 0, './the_greek_image.png', 'greek', 'description for the greek restaurant'),
		('The tai', 0, './the_tai_image.png', 'tai', 'description for the tai restaurant'),
		('The chinise', 0, './the_chinise_image.png', 'chinise', 'description for the chinise restaurant');

insert into Users
values ('user1', 'admin'),
		('user2', 'logedIn'),
		('user3', 'guest');

insert into Reviews
values ('user2', 'The italian', 8),
		('user2', 'The tai', 7);
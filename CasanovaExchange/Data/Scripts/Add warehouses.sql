use commodityexchangedb;
delete from Warehouse;
insert into Warehouse
values
   ('Addis Ababa', 'AA'),
   ('Adama', 'AD'),
   ('Assossa', 'AS'),
   ('Bure', 'BR'),
   ('Bure', 'BU'),
   ('Dansha', 'DS'),
   ('Gonder', 'GN'),
   ('Hawassa', 'HW'),
   ('Humera', 'HM'),
   ('Jimma', 'JM'),
   ('Kombolcha', 'KM'),
   ('Metemma', 'MT'),
   ('Nazareth', 'NZ'),
   ('Nekemte', 'NK'),
   ('Sarris Coffee', 'SC'),
   ('Saris Grain', 'SG'),
   ('Hawassa', 'HW'),
   ('Wolayta Sodo Grain', 'WG');

select *
from Warehouse
import unittest
from models.warehouses import Warehouses, WAREHOUSES

class TestWarehouses(unittest.TestCase):
    def setUp(self):
        self.Warehouse = Warehouses("/", True)
        WAREHOUSES.append({"id": 1, "code": "YQZZNL56", "name": "Heemskerk cargo hub", "address": "Karlijndreef 281", "zip": "4002 AS", "city": "Heemskerk", "province": "Friesland", "country": "NL", "contact": {"name": "Fem Keijzer", "phone": "(078) 0013363", "email": "blamore@example.net"}, "created_at": "1983-04-13 04:59:55", "updated_at": "2007-02-08 20:11:00"})
        WAREHOUSES.append({"id": 2, "code": "GIOMNL90", "name": "Petten longterm hub", "address": "Owenweg 731", "zip": "4615 RB", "city": "Petten", "province": "Noord-Holland", "country": "NL", "contact": {"name": "Maud Adryaens", "phone": "+31836 752702", "email": "nickteunissen@example.com"}, "created_at": "2008-02-22 19:55:39", "updated_at": "2009-08-28 23:15:50"})
        WAREHOUSES.append({"id": 3, "code": "VCKINLLK", "name": "Naaldwijk distribution hub", "address": "Izesteeg 807", "zip": "1636 KI", "city": "Naaldwijk", "province": "Utrecht", "country": "NL", "contact": {"name": "Frederique van Wallaert", "phone": "(009) 4870289", "email": "jelle66@example.net"}, "created_at": "2001-05-11 10:43:52", "updated_at": "2017-12-19 14:32:38"})

    def test_get_warehouses(self):
        self.assertEqual(self.Warehouse.get_warehouses(), WAREHOUSES)

    def test_get_warehouse(self):
        self.assertEqual(self.Warehouse.get_warehouse(1), WAREHOUSES[0])
    
    def test_add_warehouse(self):
        warehouse = {"id": 4, "code": "IPJMNLSY", "name": "Bosch en Duin storage location", "address": "Fabianweg 71", "zip": "5701 IA", "city": "Bosch en Duin", "province": "Flevoland", "country": "NL", "contact": {"name": "Oscar Hemma van Allemani\u00eb-Hoes", "phone": "058 2995479", "email": "suze00@example.org"}, "created_at": "2007-10-19 09:43:20", "updated_at": "2019-11-02 07:30:52"}
        self.Warehouse.add_warehouse(warehouse)
        self.assertEqual(self.Warehouse.get_warehouse(4), warehouse)

    def test_update_warehouse(self):
        warehouse = {"id": 1, "code": "YQZZNL56", "name": "Heemskerk cargo hub", "address": "Karlijndreef 281", "zip": "4002 AM", "city": "Heemskerk", "province": "Friesland", "country": "NL", "contact": {"name": "Fem Keijzer", "phone": "(078) 0013363", "email": "blamore@example.net"}, "created_at": "1983-04-13 04:59:55", "updated_at": "2007-02-08 20:11:00"}
        self.Warehouse.update_warehouse(1, warehouse)
        self.assertEqual(self.Warehouse.get_warehouse(1), warehouse)
    
    def test_remove_warehouse(self):
        self.Warehouse.remove_warehouse(1)
        self.assertEqual(self.Warehouse.get_warehouse(1), None)


if __name__ == '__main__':
    unittest.main()
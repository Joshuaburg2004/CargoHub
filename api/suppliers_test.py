import unittest
from models.suppliers import Suppliers, SUPPLIERS


class TestSuppliers(unittest.TestCase):
    def setUp(self):
        self.Supplier = Suppliers("/", True)
        SUPPLIERS.append({"id": 1, "code": "SUP0001", "name": "Lee, Parks and Johnson", "address": "5989 Sullivan Drives", "address_extra": "Apt. 996", "city": "Port Anitaburgh", "zip_code": "91688", "province": "Illinois", "country": "Czech Republic", "contact_name": "Toni Barnett", "phonenumber": "363.541.7282x36825", "reference": "LPaJ-SUP0001", "created_at": "1971-10-20 18:06:17", "updated_at": "1985-06-08 00:13:46"}) 
        SUPPLIERS.append({"id": 2, "code": "SUP0002", "name": "Holden-Quinn", "address": "576 Christopher Roads", "address_extra": "Suite 072", "city": "Amberbury", "zip_code": "16105", "province": "Illinois", "country": "Saint Martin", "contact_name": "Kathleen Vincent", "phonenumber": "001-733-291-8848x3542", "reference": "H-SUP0002", "created_at": "1995-12-18 03:05:46", "updated_at": "2019-11-10 22:11:12"})
        SUPPLIERS.append({"id": 3, "code": "SUP0003", "name": "White and Sons", "address": "1761 Shepard Valley", "address_extra": "Suite 853", "city": "Aguilarton", "zip_code": "63918", "province": "Wyoming", "country": "Ghana", "contact_name": "Jason Hudson", "phonenumber": "001-910-585-6962x8307", "reference": "WaS-SUP0003", "created_at": "2010-06-14 02:32:58", "updated_at": "2019-06-16 19:29:49"})

    def test_get_suppliers(self):
        self.assertEqual(self.Supplier.get_suppliers(), SUPPLIERS)

    def test_get_supplier(self):
        self.assertEqual(self.Supplier.get_supplier(1), SUPPLIERS[0])
    
    def test_add_supplier(self):
        supplier = {"id": 4, "code": "SUP0004", "name": "Smith, Johnson and Johnson", "address": "5989 Sullivan Drives", "address_extra": "Apt. 996", "city": "Port Anitaburgh", "zip_code": "91688", "province": "Illinois", "country": "Czech Republic", "contact_name": "Toni Barnett", "phonenumber": "363.541.7282x36825", "reference": "LPaJ-SUP0001", "created_at": "1971-10-20 18:06:17", "updated_at": "1985-06-08 00:13:46"}
        self.Supplier.add_supplier(supplier)
        self.assertEqual(self.Supplier.get_supplier(4), supplier)

    def test_update_supplier(self):
        supplier = {"id": 1, "code": "SUP0001", "name": "Lee, Parks and Johnson", "address": "5989 Sullivan Drives", "address_extra": "Apt. 996", "city": "Port Anitaburgh", "zip_code": "91688", "province": "Illinois", "country": "Czech Republic", "contact_name": "Toni Barnett", "phonenumber": "363.541.7282x36825", "reference": "LPaJ-SUP0001", "created_at": "1971-10-20 18:06:17", "updated_at": "1985-06-08 00:13:46"}
        self.Supplier.update_supplier(1, supplier)
        self.assertEqual(self.Supplier.get_supplier(1), supplier)
    
    def test_remove_supplier(self):
        self.Supplier.remove_supplier(1)
        self.assertEqual(self.Supplier.get_supplier(1), None)


if __name__ == '__main__':
    unittest.main()
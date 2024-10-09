import unittest
from models.items import Items, ITEMS

class TestItems(unittest.TestCase):
    def setUp(self):
        self.Item = Items("./data/")
        ITEMS.append({
            "uid": "P000001",
            "code": "sjQ23408K",
            "description": "Face-to-face clear-thinking complexity",
            "short_description": "must",
            "upc_code": "6523540947122",
            "model_number": "63-OFFTq0T",
            "commodity_code": "oTo304",
            "item_line": 11,
            "item_group": 73,
            "item_type": 14,
            "unit_purchase_quantity": 47,
            "unit_order_quantity": 13,
            "pack_order_quantity": 11,
            "supplier_id": 34,
            "supplier_code": "SUP423",
            "supplier_part_number": "E-86805-uTM",
            "created_at": "2015-02-19 16:08:24",
            "updated_at": "2015-09-26 06:37:56"
        })

        
    def test_get_items(self):
        self.assertEqual(self.Item.get_items()[0], ITEMS[0])
    

    def test_get_by_uid(self):
        self.assertEqual(self.Item.get_item("P000001"), ITEMS[0])


    def test_get_items_for_item_line(self):
        self.assertEqual(self.Item.get_items_for_item_line(11)[0], ITEMS[0])


    def test_get_items_for_item_group(self):
        self.assertEqual(self.Item.get_items_for_item_group(73)[0], ITEMS[0])


    def test_get_items_for_item_type(self):
        self.assertEqual(self.Item.get_items_for_item_type(14)[0], ITEMS[0])


    def test_get_items_for_supplier(self):
        self.assertEqual(self.Item.get_items_for_supplier(34)[0], ITEMS[0])


    def test_add_item(self):
        #TODO: make sure an item is created and compared to a mocked item??
        mock_item = {
            "uid": "P999999",
            "code": "abc123ABC",
            "description": "This is a description",
            "short_description": "mand",
            "upc_code": "0123456789123",
            "model_number": "99-ABCDe7G",
            "commodity_code": "aBc123",
            "item_line": 1,
            "item_group": 1,
            "item_type": 1,
            "unit_purchase_quantity": 1,
            "unit_order_quantity": 1,
            "pack_order_quantity": 1,
            "supplier_id": 1,
            "supplier_code": "SUP1",
            "supplier_part_number": "E-86805-uTM",
            "created_at": "",
            "updated_at": ""
        }
        self.Item.add_item(mock_item)
        # this should return something
        return True


if __name__ == "__main__":
    unittest.main()
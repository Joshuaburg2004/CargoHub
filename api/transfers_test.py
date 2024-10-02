import unittest
from models.transfers import Transfers, TRANSFERS

class TestTransfers(unittest.TestCase):
    def setUp(self):
        self.Transfer = Transfers("/", True)
        TRANSFERS.append({
        "id": 1,
        "reference": "TR00001",
        "transfer_from": None,
        "transfer_to": 9229,
        "transfer_status": "Completed",
        "created_at": "2000-03-11T13:11:14Z",
        "updated_at": "2000-03-12T16:11:14Z",
        "items": [
            {
                "item_id": "P007435",
                "amount": 23
            }
        ]
    }) 
        TRANSFERS.append({
        "id": 2,
        "reference": "TR00002",
        "transfer_from": 9229,
        "transfer_to": 9284,
        "transfer_status": "Completed",
        "created_at": "2017-09-19T00:33:14Z",
        "updated_at": "2017-09-20T01:33:14Z",
        "items": [
            {
                "item_id": "P007435",
                "amount": 23
            }
        ]
    })
        TRANSFERS.append({
        "id": 3,
        "reference": "TR00003",
        "transfer_from": None,
        "transfer_to": 9199,
        "transfer_status": "Completed",
        "created_at": "2000-03-11T13:11:14Z",
        "updated_at": "2000-03-12T14:11:14Z",
        "items": [
            {
                "item_id": "P009557",
                "amount": 1
            }
        ]
    })

    def test_get_transfers(self):
        self.assertEqual(self.Transfer.get_transfers(), TRANSFERS)

    def test_get_transfer(self):
        self.assertEqual(self.Transfer.get_transfer(1), TRANSFERS[0])
    
    def test_add_transfer(self):
        transfer = {"id": 4,
        "reference": "TR00004",
        "transfer_from": 5555,
        "transfer_to": 9229,
        "transfer_status": "Completed",
        "created_at": "2000-03-11T13:11:14Z",
        "updated_at": "2000-03-12T16:11:14Z",
        "items": [
            {
                "item_id": "P007445",
                "amount": 27
            }
        ]}
        self.Transfer.add_transfer(transfer)
        self.assertEqual(self.Transfer.get_transfer(4), transfer)

    def test_update_transfer(self):
        transfer = {"id": 1,
        "reference": "TR00001",
        "transfer_from": 1234,
        "transfer_to": 9229,
        "transfer_status": "Completed",
        "created_at": "2000-03-11T13:11:14Z",
        "updated_at": "2000-03-12T16:11:14Z",
        "items": [
            {
                "item_id": "P007435",
                "amount": 23
            }
        ]}
        self.Transfer.update_transfer(1, transfer)
        self.assertEqual(self.Transfer.get_transfer(1), transfer)
    
    def test_remove_transfer(self):
        self.Transfer.remove_transfer(1)
        self.assertEqual(self.Transfer.get_transfer(1), None)

    def test_get_items_in_transfer(self):
        self.assertEqual(self.Transfer.get_items_in_transfer(1), [{"item_id": "P007435", "amount": 23}])
        self.assertEqual(self.Transfer.get_items_in_transfer(2), [{"item_id": "P007435", "amount": 23}])
        self.assertEqual(self.Transfer.get_items_in_transfer(3), [{"item_id": "P009557", "amount": 1}])


if __name__ == '__main__':
    unittest.main()
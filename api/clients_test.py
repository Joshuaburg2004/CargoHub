import unittest
from fastapi.testclient import TestClient
from models.clients import Clients, CLIENTS
from providers.data_provider import DEBUG


class TestClients(unittest.TestCase):
    def setUp(self):
        DEBUG = True
        self.client = Clients("NAN", is_debug=DEBUG)
        CLIENTS.clear()
        CLIENTS.append({"id": 1, "name": "test1", "address": "1296 Daniel Road Apt. 349", "city": "Pierceview", "zip_code": "28301", "province": "Colorado", "country": "United States", "contact_name": "Bryan Clark", "contact_phone": "242.732.3483x2573", "contact_email": "robertcharles@example.net"})
        CLIENTS.append({"id": 2, "name": "test2", "address": "1296 Daniel Road Apt. 349", "city": "Pierceview", "zip_code": "28301", "province": "Colorado", "country": "United States", "contact_name": "Bryan Clark", "contact_phone": "242.732.3483x2573", "contact_email": "robertcharles@example.net"})
        CLIENTS.append({"id": 3, "name": "test3", "address": "1296 Daniel Road Apt. 349", "city": "Pierceview", "zip_code": "28301", "province": "Colorado", "country": "United States", "contact_name": "Bryan Clark", "contact_phone": "242.732.3483x2573", "contact_email": "robertcharles@example.net"})

    
    def testAdd(self):
        if (CLIENTS.__len__() != 0):
            CLIENTS.clear()
        toAdd = {"id": 1, "name": "Raymond Corp", "address": "1296 Daniel Road Apt. 349", "city": "Pierceview", "zip_code": "28301", "province": "Colorado", "country": "United States", "contact_name": "Bryan Clark", "contact_phone": "242.732.3483x2573", "contact_email": "robertcharles@example.net"}
        self.client.add_client(toAdd)
        self.assertEqual(CLIENTS.__len__(), 1)
    
    def TestGetOne(self):
        self.assertEqual(self.client.get_client(1), CLIENTS[0])

    def TestGetAll(self):
        self.assertEqual(self.client.get_clients(), CLIENTS)
    
    def TestUpdate(self):
        updated = {"id": 1, "name": "test9001", "address": "1296 Daniel Road Apt. 349", "city": "Pierceview", "zip_code": "28301", "province": "Colorado", "country": "United States", "contact_name": "Bryan Clark", "contact_phone": "242.732.3483x2573", "contact_email": "robertcharles@example.net"},
        self.client.update_client()
    
    def test_delete(self):
        self.client.remove_client(1)
        self.assertEqual(2, CLIENTS.__len__())


        


if __name__ == "__main__":
    unittest.main()

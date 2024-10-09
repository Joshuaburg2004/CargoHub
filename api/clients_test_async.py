import unittest
from fastapi.testclient import TestClient
from providers.data_provider import DEBUG;

class test_clients_async9(unittest.TestCase):
    def setUp(self):
        self.client = TestClient(app)

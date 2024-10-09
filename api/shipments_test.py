import unittest
from providers.data_provider import Shipments
from providers import data_provider
null = None

SHIPMENTS = []


class TestShipmentsApi(unittest.TestCase):
    def setUp(self):
        data_provider.DEBUG = True
        data_provider._shipments = [{
            "id": 1,
            "order_id": 1,
            "source_id": 33,
            "order_date": "2000-03-09",
            "request_date": "2000-03-11",
            "shipment_date": "2000-03-13",
            "shipment_type": "I",
            "shipment_status": "Pending",
            "notes": "Zee vertrouwen klas rots heet lachen oneven begrijpen.",
            "carrier_code": "DPD",
            "carrier_description": "Dynamic Parcel Distribution",
            "service_code": "Fastest",
            "payment_type": "Manual",
            "transfer_mode": "Ground",
            "total_package_count": 31,
            "total_package_weight": 594.42,
            "created_at": "2000-03-10T11:11:14Z",
            "updated_at": "2000-03-11T13:11:14Z",
            "items": [
                {
                    "item_id": "P007435",
                    "amount": 23
                },
                {
                    "item_id": "P009557",
                    "amount": 1
                },
                {
                    "item_id": "P009553",
                    "amount": 50
                },
                {
                    "item_id": "P010015",
                    "amount": 16
                },
                {
                    "item_id": "P002084",
                    "amount": 33
                },
                {
                    "item_id": "P009663",
                    "amount": 18
                },
                {
                    "item_id": "P010125",
                    "amount": 18
                },
                {
                    "item_id": "P005768",
                    "amount": 26
                },
                {
                    "item_id": "P004051",
                    "amount": 1
                },
                {
                    "item_id": "P005026",
                    "amount": 29
                },
                {
                    "item_id": "P000726",
                    "amount": 22
                },
                {
                    "item_id": "P008107",
                    "amount": 47
                },
                {
                    "item_id": "P001598",
                    "amount": 32
                },
                {
                    "item_id": "P002855",
                    "amount": 20
                },
                {
                    "item_id": "P010404",
                    "amount": 30
                },
                {
                    "item_id": "P010446",
                    "amount": 6
                },
                {
                    "item_id": "P001517",
                    "amount": 9
                },
                {
                    "item_id": "P009265",
                    "amount": 2
                },
                {
                    "item_id": "P001108",
                    "amount": 20
                },
                {
                    "item_id": "P009110",
                    "amount": 18
                },
                {
                    "item_id": "P009686",
                    "amount": 13
                }
            ]
        }]
        self.Shipment = Shipments("/", True)
        SHIPMENTS.append(
            {
                "id": 1,
                "order_id": 1,
                "source_id": 33,
                "order_date": "2000-03-09",
                "request_date": "2000-03-11",
                "shipment_date": "2000-03-13",
                "shipment_type": "I",
                "shipment_status": "Pending",
                "notes": "Zee vertrouwen klas rots heet lachen oneven begrijpen.",
                "carrier_code": "DPD",
                "carrier_description": "Dynamic Parcel Distribution",
                "service_code": "Fastest",
                "payment_type": "Manual",
                "transfer_mode": "Ground",
                "total_package_count": 31,
                "total_package_weight": 594.42,
                "created_at": "2000-03-10T11:11:14Z",
                "updated_at": "2000-03-11T13:11:14Z",
                "items":
                [
                    {
                        "item_id": "P007435",
                        "amount": 23
                    },
                    {
                        "item_id": "P009557",
                        "amount": 1
                    }
                ]
            }
        )
        SHIPMENTS.append(
            {
                "id": 2,
                "order_id": 2,
                "source_id": 9,
                "order_date": "1983-11-28",
                "request_date": "1983-11-30",
                "shipment_date": "1983-12-02",
                "shipment_type": "I",
                "shipment_status": "Transit",
                "notes": "Wit duur fijn vlieg.",
                "carrier_code": "PostNL",
                "carrier_description": "Royal Dutch Post and Parcel Service",
                "service_code": "TwoDay",
                "payment_type": "Automatic",
                "transfer_mode": "Ground",
                "total_package_count": 56,
                "total_package_weight": 42.25,
                "created_at": "1983-11-29T11:12:17Z",
                "updated_at": "1983-11-30T13:12:17Z",
                "items": [
                    {
                        "item_id": "P003790",
                        "amount": 10
                    },
                    {
                        "item_id": "P007369",
                        "amount": 15
                    },
                    {
                        "item_id": "P007311",
                        "amount": 21
                    },
                    {
                        "item_id": "P004140",
                        "amount": 8
                    },
                    {
                        "item_id": "P004413",
                        "amount": 46
                    },
                    {
                        "item_id": "P004717",
                        "amount": 38
                    },
                    {
                        "item_id": "P001919",
                        "amount": 13
                    },
                    {
                        "item_id": "P010075",
                        "amount": 5
                    },
                    {
                        "item_id": "P006603",
                        "amount": 48
                    },
                    {
                        "item_id": "P004504",
                        "amount": 30
                    },
                    {
                        "item_id": "P009594",
                        "amount": 35
                    },
                    {
                        "item_id": "P008851",
                        "amount": 25
                    },
                    {
                        "item_id": "P002129",
                        "amount": 46
                    },
                    {
                        "item_id": "P002320",
                        "amount": 4
                    },
                    {
                        "item_id": "P008341",
                        "amount": 23
                    }
                ]
            }
        )

    def test_get_shipment_id(self):
        self.assertEqual(self.Shipment.get_shipment(1), SHIPMENTS[0])

    def test_get_items_shipment_id(self):
        pass

    def test_add_shipment(self):
        newshipment = {
            "id": 3,
            "order_id": 3,
            "source_id": 52,
            "order_date": "1973-01-28",
            "request_date": "1973-01-30",
            "shipment_date": "1973-02-01",
            "shipment_type": "I",
            "shipment_status": "Pending",
            "notes": "Hoog genot springen afspraak mond bus.",
            "carrier_code": "DHL",
            "carrier_description": "DHL Express",
            "service_code": "NextDay",
            "payment_type": "Automatic",
            "transfer_mode": "Ground",
            "total_package_count": 29,
            "total_package_weight": 463.0,
            "created_at": "1973-01-28T20:09:11Z",
            "updated_at": "1973-01-29T22:09:11Z",
            "items": [
                {
                    "item_id": "P010669",
                    "amount": 16
                }
            ]
        }
        self.Shipment.add_shipment(newshipment)
        self.assertEqual(self.Shipment.get_shipment(3), newshipment)

    def test_add_items_shipment(self):
        pass

    def test_add_order_shipment(self):
        pass

    def test_update_shipment(self):
        newshipment = {
            "id": 1,
            "order_id": 1,
            "source_id": 33,
            "order_date": "2000-03-09",
            "request_date": "2000-03-11",
            "shipment_date": "2000-03-13",
            "shipment_type": "I",
            "shipment_status": "Pending",
            "notes": "Zee vertrouwen klas rots heet lachen oneven begrijpen.",
            "carrier_code": "DPD",
            "carrier_description": "Dynamic Parcel Distribution",
            "service_code": "Fastest",
            "payment_type": "Manual",
            "transfer_mode": "Ground",
            "total_package_count": 31,
            "total_package_weight": 594.42,
            "created_at": "2000-03-10T11:11:14Z",
            "updated_at": "2000-03-11T13:11:14Z",
            "items": [
                {
                    "item_id": "P007435",
                    "amount": 23
                },
                {
                    "item_id": "P009557",
                    "amount": 1
                }
            ]
        }
        self.Shipment.update_shipment(1, newshipment)
        self.assertEqual(self.Shipment.get_shipment(1), newshipment)

    def test_update_items_shipment(self):
        new_items = [
            {
                "item_id": "P007435",
                "amount": 23
            },
            {
                "item_id": "P009557",
                "amount": 1
            },
            {
                "item_id": "P009553",
                "amount": 50
            },
            {
                "item_id": "P010015",
                "amount": 16
            },
            {
                "item_id": "P002084",
                "amount": 33
            },
            {
                "item_id": "P009663",
                "amount": 18
            },
            {
                "item_id": "P010125",
                "amount": 18
            },
            {
                "item_id": "P005768",
                "amount": 26
            },
            {
                "item_id": "P004051",
                "amount": 1
            },
            {
                "item_id": "P005026",
                "amount": 29
            }
        ]
        self.Shipment.update_items_in_shipment(1, new_items)
        self.assertEqual(self.Shipment.get_shipment(1)["items"], new_items)

    def test_remove_shipment(self):
        pass

    def test_load_shipment(self):
        pass


if __name__ == '__main__':
    unittest.main()

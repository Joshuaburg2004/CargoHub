import unittest
from models.orders import Orders, ORDERS
null = None

class TestOrders(unittest.TestCase):
    def setUp(self):
        self.Order = Orders("/", True)
        ORDERS.append({
            "id": 1,
            "source_id": 33,
            "order_date": "2019-04-03T11:33:15Z",
            "request_date": "2019-04-07T11:33:15Z",
            "reference": "ORD00001",
            "reference_extra": "Bedreven arm straffen bureau.",
            "order_status": "Delivered",
            "notes": "Voedsel vijf vork heel.",
            "shipping_notes": "Buurman betalen plaats bewolkt.",
            "picking_notes": "Ademen fijn volgorde scherp aardappel op leren.",
            "warehouse_id": 18,
            "ship_to": null,
            "bill_to": null,
            "shipment_id": 1,
            "total_amount": 9905.13,
            "total_discount": 150.77,
            "total_tax": 372.72,
            "total_surcharge": 77.6,
            "created_at": "2019-04-03T11:33:15Z",
            "updated_at": "2019-04-05T07:33:15Z",
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
        })
        ORDERS.append({
            "id": 2,
            "source_id": 9,
            "order_date": "1999-07-05T19:31:10Z",
            "request_date": "1999-07-09T19:31:10Z",
            "reference": "ORD00002",
            "reference_extra": "Vergelijken raak geluid beetje altijd.",
            "order_status": "Delivered",
            "notes": "We hobby thee compleet wiel fijn.",
            "shipping_notes": "Nood provincie hier.",
            "picking_notes": "Borstelen dit verf suiker.",
            "warehouse_id": 20,
            "ship_to": null,
            "bill_to": null,
            "shipment_id": 2,
            "total_amount": 8484.98,
            "total_discount": 214.52,
            "total_tax": 665.09,
            "total_surcharge": 42.12,
            "created_at": "1999-07-05T19:31:10Z",
            "updated_at": "1999-07-07T15:31:10Z",
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
        })
        ORDERS.append({
            "id": 3,
            "source_id": 52,
            "order_date": "1983-09-26T19:06:08Z",
            "request_date": "1983-09-30T19:06:08Z",
            "reference": "ORD00003",
            "reference_extra": "Vergeven kamer goed enkele wiel tussen.",
            "order_status": "Delivered",
            "notes": "Zeil hoeveel onze map sex ding.",
            "shipping_notes": "Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.",
            "picking_notes": "Grof geven politie suiker bodem zuid.",
            "warehouse_id": 11,
            "ship_to": null,
            "bill_to": null,
            "shipment_id": 3,
            "total_amount": 1156.14,
            "total_discount": 420.45,
            "total_tax": 677.42,
            "total_surcharge": 86.03,
            "created_at": "1983-09-26T19:06:08Z",
            "updated_at": "1983-09-28T15:06:08Z",
            "items": [
                {
                    "item_id": "P010669",
                    "amount": 16
                }
            ]
        })

    def test_get_orders(self):
        self.assertEqual(self.Order.get_orders(), ORDERS)

    def test_get_order(self):
        self.assertEqual(self.Order.get_order(1), ORDERS[0])
    
    def test_add_order(self):
        order = {
        "id": 4,
        "source_id": 12,
        "order_date": "1988-01-31T22:12:24Z",
        "request_date": "1988-02-04T22:12:24Z",
        "reference": "ORD00004",
        "reference_extra": "Zelfde moment markeren stad markeren vreemde.",
        "order_status": "Delivered",
        "notes": "Studie vlees voelen altijd kom.",
        "shipping_notes": "Overleden munt kok.",
        "picking_notes": "Achter uitzoeken ver samen object maan.",
        "warehouse_id": 34,
        "ship_to": null,
        "bill_to": null,
        "shipment_id": 4,
        "total_amount": 4030.92,
        "total_discount": 305.23,
        "total_tax": 752.36,
        "total_surcharge": 99.41,
        "created_at": "1988-01-31T22:12:24Z",
        "updated_at": "1988-02-02T18:12:24Z",
        "items": [
            {
                "item_id": "P007004",
                "amount": 14
            },
            {
                "item_id": "P005769",
                "amount": 28
            },
            {
                "item_id": "P004263",
                "amount": 33
            },
            {
                "item_id": "P006228",
                "amount": 33
            },
            {
                "item_id": "P008542",
                "amount": 11
            },
            {
                "item_id": "P002056",
                "amount": 6
            },
            {
                "item_id": "P008694",
                "amount": 16
            },
            {
                "item_id": "P008241",
                "amount": 19
            },
            {
                "item_id": "P004464",
                "amount": 11
            },
            {
                "item_id": "P008635",
                "amount": 1
            },
            {
                "item_id": "P000667",
                "amount": 32
            },
            {
                "item_id": "P006615",
                "amount": 49
            },
            {
                "item_id": "P007815",
                "amount": 16
            },
            {
                "item_id": "P011493",
                "amount": 23
            },
            {
                "item_id": "P004458",
                "amount": 3
            },
            {
                "item_id": "P002974",
                "amount": 6
            },
            {
                "item_id": "P003655",
                "amount": 35
            },
            {
                "item_id": "P009953",
                "amount": 35
            },
            {
                "item_id": "P000917",
                "amount": 23
            },
            {
                "item_id": "P003711",
                "amount": 4
            },
            {
                "item_id": "P011448",
                "amount": 46
            },
            {
                "item_id": "P001562",
                "amount": 21
            },
            {
                "item_id": "P002435",
                "amount": 31
            }
        ]
    }
        self.Order.add_order(order)
        self.assertEqual(self.Order.get_order(4), order)

    def test_update_order(self):
        order = {
            "id": 1,
            "source_id": 33,
            "order_date": "2019-04-03T11:33:15Z",
            "request_date": "2019-04-07T11:33:15Z",
            "reference": "ORD00001",
            "reference_extra": "Bedreven arm straffen bureau.",
            "order_status": "Delivered",
            "notes": "Voedsel vijf vork heel.",
            "shipping_notes": "Buurman betalen plaats bewolkt.",
            "picking_notes": "Ademen fijn volgorde bot aardappel op leren.",
            "warehouse_id": 18,
            "ship_to": null,
            "bill_to": null,
            "shipment_id": 1,
            "total_amount": 9905.13,
            "total_discount": 150.77,
            "total_tax": 372.72,
            "total_surcharge": 77.6,
            "created_at": "2019-04-03T11:33:15Z",
            "updated_at": "2019-04-05T07:33:15Z",
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
        }
        self.Order.update_order(1, order)
        self.assertEqual(self.Order.get_order(1), order)

    def test_update_items_in_order(self):
        items = [
            {
                    "item_id": "P007435",
                    "amount": 32
                },
                {
                    "item_id": "P009557",
                    "amount": 10
                },
                {
                    "item_id": "P009553",
                    "amount": 5
                },
                {
                    "item_id": "P010015",
                    "amount": 61
                },
                {
                    "item_id": "P002084",
                    "amount": 22
                },
                {
                    "item_id": "P009663",
                    "amount": 81
                },
                {
                    "item_id": "P010125",
                    "amount": 81
                },
                {
                    "item_id": "P005768",
                    "amount": 62
                },
                {
                    "item_id": "P004051",
                    "amount": 2
                },
                {
                    "item_id": "P005026",
                    "amount": 92
                },
                {
                    "item_id": "P000726",
                    "amount": 12
                },
                {
                    "item_id": "P008107",
                    "amount": 74
                },
                {
                    "item_id": "P001598",
                    "amount": 23
                },
                {
                    "item_id": "P002855",
                    "amount": 12
                },
                {
                    "item_id": "P010404",
                    "amount": 12
                },
                {
                    "item_id": "P010446",
                    "amount": 2
                },
                {
                    "item_id": "P001517",
                    "amount": 8
                },
                {
                    "item_id": "P009265",
                    "amount": 1
                },
                {
                    "item_id": "P001108",
                    "amount": 19
                },
                {
                    "item_id": "P009110",
                    "amount": 17
                },
                {
                    "item_id": "P009686",
                    "amount": 11
                }
        ]
        self.Order.update_items_in_order(1, items)
        self.assertEqual(self.Order.get_order(1)["items"], items)

    def test_update_orders_in_shipment(self):
        orders = [1, 2]
        self.Order.update_orders_in_shipment(1, orders)
        self.assertEqual(self.Order.get_order(1)["shipment_id"], 1)
        self.assertEqual(self.Order.get_order(2)["shipment_id"], 1)
        self.assertEqual(self.Order.get_order(3)["shipment_id"], -1)
        self.assertEqual(self.Order.get_order(1)["order_status"], "Packed")
        self.assertEqual(self.Order.get_order(2)["order_status"], "Packed")
        self.assertEqual(self.Order.get_order(3)["order_status"], "Scheduled")
    
    def test_get_orders_in_shipment(self):
        pass

    def test_get_orders_for_client(self):
        pass
    
    def test_remove_order(self):
        self.Order.remove_order(1)
        self.assertEqual(self.Order.get_order(1), None)


if __name__ == '__main__':
    unittest.main()
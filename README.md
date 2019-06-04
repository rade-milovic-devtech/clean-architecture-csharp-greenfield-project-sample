# Online Theather

This is a sample software implementation which demonstrates how to
incorporate clean architecture in a greenfield project using .NET
technologies and C# programming language.

## Requirements:

The online theather system contains the following core business
entities:

* Customer
  * name
  * email
  * status (regular or advanced)
  * status expiration date
  * money spent
  * purchased movies

Once created, customer has regular state and it does not have
expiration date. When the customer is promoted to advanced status
expiration date must be set. Promoting a customer to the advanced
status gives 25% discount on all movies for 1 year. In order to
promote a customer they need to meet the following criteria:

1. buy at least 2 movies during the last 30 days
2. spend at least 100$ during the last year

---

* Movie
  * name
  * licensing model (two days or life-long)
  * price

With two days licensing model customers can only watch the movie
during the next 48 hours and with the life-long licensing model
they can watch it any time they want.

---

* Purchased movie
  * movie
  * customer
  * price
  * purchased date
  * expiration date (optional)

After a movie is purchased its price is added to the customer's
money spent amount.

### Features to implement:

* Purchasing a movie
* Create a customer
* Update a customer
* Promoting a customer to advanced status
* Retrieve all customers
* Retrieve a detailed information about a customer
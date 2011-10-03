Feature: Adding a Customer
	In order to make money
	As a business
	I need customers

Scenario: Adding a customer that does not already exists
	Given a new customer
	When I save the customer
	Then the new customer should be viewable
	And should be able to rebuild the customer

Scenario: Adding a customer that already exists
	Given a customer that has already registered
	When the same customer is trying to be added
	Then the customer should not be added twice

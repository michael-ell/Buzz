Feature: Adding a Customer
	In order to make money
	As a business
	I need customers

Scenario: Adding a customer that does not already exits
	Given a new customer
	When I save the customer
	Then the new customer should be viewable
	And should be able to rebuild the customer

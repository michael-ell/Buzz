Feature: Addition
	In order to ensure email reaches out customer
	As a owner
	I want to allow the customers email to be changed

Scenario: Changing a customer email address
	Given a customer
	When I change the email address for the customer
	Then the new customer should have the new email address

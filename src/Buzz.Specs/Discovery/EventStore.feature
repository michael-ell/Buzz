Feature: Saving Events
	In to understand NCQRS and event stores
	As a newbie
	I want to be to be able to store events

Scenario: Saving a single event
	Given a new customer
	When I save the customer
	Then new customer event should be stored
